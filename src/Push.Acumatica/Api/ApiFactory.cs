using System;
using Autofac;
using Push.Acumatica.Config;
using Push.Acumatica.Http;

namespace Push.Acumatica.Api
{
    public class ApiFactory
    {
        private readonly ILifetimeScope _scope;
        private readonly AcumaticaHttpContext _httpContext;
        private readonly string _name;

        public ApiFactory(ILifetimeScope scope, AcumaticaHttpContext httpContext)
        {
            _scope = scope;
            _httpContext = httpContext;
        }

        public void Initialize(AcumaticaCredentials credentials, AcumaticaHttpConfig settings)
        {
            _httpContext.Initialize(credentials, settings);
        }

        public T Make<T>() where T : IAcumaticaApi
        {
            var client = _scope.Resolve<T>();
            client.InjectHttpContext(_httpContext);
            return client;
        }

        public AcumaticaHttpContext HttpContext()
        {
            return _httpContext;
        }

        public void RunSession(Action<ApiFactory> action)
        {
            try
            {
                if (!_httpContext.IsLoggedIn)
                {
                    _httpContext.Login();
                }

                action(this);
            }
            finally
            {
                if (_httpContext.IsLoggedIn)
                {
                    _httpContext.Logout();
                }
            }
        }

    }
}
