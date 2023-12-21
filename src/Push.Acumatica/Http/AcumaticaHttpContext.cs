using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Push.Acumatica.Config;
using Push.Foundation.Utilities.Execution;
using Push.Foundation.Utilities.Http;
using Push.Foundation.Utilities.Logging;

namespace Push.Acumatica.Http
{
    public class AcumaticaHttpContext : IDisposable
    {
        // Injected property
        private readonly IPushLogger _logger;

        // These are all set by the Initialize method
        private AcumaticaCredentials _credentials;
        private AcumaticaHttpConfig _settings;
        private Uri _baseAddressUri;
        private string BaseAddressUrl => _baseAddressUri.ToString();
        private HttpClient _httpClient;
        private FaultTolerantExecutor _executor;

        // Meta stuff
        public readonly Guid ObjectIdentifier = Guid.NewGuid();
        public bool IsLoggedIn { get; private set; } = false;

        
        public AcumaticaHttpContext(IPushLogger logger)
        {
            _logger = logger;
        }

        public void Initialize(
                AcumaticaCredentials credentials, AcumaticaHttpConfig settings = null)
        {
            _settings = settings ?? new AcumaticaHttpConfig();
            _credentials = credentials;
            _baseAddressUri = new Uri(credentials.InstanceUrl);
            
            _executor = new FaultTolerantExecutor()
            {
                MaxNumberOfAttempts = _settings.MaxAttempts,
                ThrottlingKey = credentials.InstanceUrl,
                Logger = _logger,
            };

            _httpClient
                = new HttpClient(
                    new HttpClientHandler
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer(),
                    })
                {
                    BaseAddress = _baseAddressUri,
                    DefaultRequestHeaders =
                    {
                        Accept =
                        {
                            MediaTypeWithQualityHeaderValue.Parse("text/json")
                        }
                    }
                };

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            // Spawn the WebRequest
            _httpClient.Timeout = new TimeSpan(0, 0, 0, _settings.Timeout);
        }


        public void Login()
        {
            var path = $"/entity/auth/login";
            var content = _credentials.AuthenticationJson;
            var response = Post(path, content, excludeVersion: true);
            IsLoggedIn = true;
        }

        public void Logout()
        {
            var path = $"/entity/auth/logout";
            var response = Post(path, "", excludeVersion: true);
            IsLoggedIn = false;
        }

        public string MakePath(string path, bool excludeVersion = false)
        {
            return !excludeVersion
                ? $"{BaseAddressUrl}{_settings.VersionSegment}{path}"
                : $"{BaseAddressUrl}{path}";
        }

        public ResponseEnvelope Get(
                string path,
                Dictionary<string, string> headers = null,
                bool excludeVersion = false)
        {
            // Arrange
            var address = MakePath(path, excludeVersion);
            var urlDebug = $"HTTP GET on {address} (ContextId: {this.ObjectIdentifier})";
            var errorContext = BuildErrorContext(urlDebug);
            _logger.Debug(urlDebug);

            // Act
            var response = _executor.Do(() => _httpClient.GetAsync(address).Result, errorContext);
            
            // Response
            var output = response.ToEnvelope();
            _logger.Trace(output.Body);

            // Assert
            return ProcessStatusCodes(output, urlDebug);
        }

        public ResponseEnvelope Post(
                string path, 
                string content, 
                Dictionary<string, string> headers = null,
                bool excludeVersion = false)
        {
            // Arrange
            var address = MakePath(path, excludeVersion);
            var urlDebug = $"HTTP POST on {address} (ContextId: {this.ObjectIdentifier})";
            var errorContext = BuildErrorContext(urlDebug, content);
            _logger.Debug(urlDebug);
            
            _logger.Trace(content);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            // Act
            var response = 
                _executor.Do(
                    () => _httpClient.PostAsync(address, httpContent).Result, errorContext);

            // Response
            var output = response.ToEnvelope();
            _logger.Trace(output.Body);

            // Assert
            return ProcessStatusCodes(output, errorContext);
        }

        public ResponseEnvelope Put(
                string path,
                string content,
                Dictionary<string, string> headers = null,
                bool excludeVersion = false)
        {
            var address = MakePath(path, excludeVersion);

            var urlDebug = $"HTTP PUT on {address} (ContextId: {this.ObjectIdentifier})";
            var errorContext = BuildErrorContext(urlDebug, content);

            _logger.Debug(urlDebug);
            _logger.Trace(content);

            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response =
                _executor.Do(
                    () => _httpClient.PutAsync(address, httpContent).Result, errorContext);

            var output = response.ToEnvelope();

            _logger.Trace(output.Body);

            return ProcessStatusCodes(output, errorContext);
        }

        private string BuildErrorContext(string url, string requestBody = null)
        {
            var output = $"*** Failed HTTP Request: {url}";

            if (requestBody != null)
            {
                output += Environment.NewLine + requestBody;
            }

            return output;
        }

        public ResponseEnvelope ProcessStatusCodes(ResponseEnvelope response, string errorContext)
        {
            // All other non-200 calls throw an exception
            if (response.HasBadStatusCode)
            {
                throw new Exception(
                    $"Bad Status Code - HTTP {(int)response.StatusCode} ({response.StatusCode})"
                    + Environment.NewLine + errorContext
                    + Environment.NewLine + response.Body);
            }

            return response;
        }

        /*DEPRECATED*/
        //public ResponseEnvelope ProcessStatusCodes(ResponseEnvelope response, string errorContext)
        //{
        //    // All other non-200 calls throw an exception
        //    if (response.HasBadStatusCode)
        //    {
        //        throw new Exception(
        //            $"Bad Status Code - HTTP {(int)response.StatusCode} ({response.StatusCode})"
        //            + Environment.NewLine + errorContext);
        //    }

        //    return response;
        //}

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
