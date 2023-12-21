using System.Collections;
using System.Configuration;
using Push.Foundation.Utilities.Helpers;


namespace Push.Acumatica.Config
{
    public class AcumaticaHttpConfig
    {
        private const int DefaultMaxAttempts = 3;
        private const int DefaultThrottlingDelay = 250;
        private const int DefaultPageSize = 50;

        private const int DefaultTimeout = 180000;
        private const string DefaultVersionSegment = "/entity/Default/17.200.001/";
        
        private static readonly 
            Hashtable _settings =
                (Hashtable)ConfigurationManager.GetSection("acumaticaHttp") ?? new Hashtable();

        public static AcumaticaHttpConfig Settings { get; } = new AcumaticaHttpConfig();



        [ConfigurationProperty("MaxAttempts", IsRequired = true)]
        public int MaxAttempts
        {
            get => (_settings["MaxAttempts"].ToString()).ToIntegerAlt(DefaultMaxAttempts);

            set => _settings["MaxAttempts"] = value;
        }

        [ConfigurationProperty("Timeout", IsRequired = true)]
        public int Timeout
        {
            get => (_settings["Timeout"].ToString()).ToIntegerAlt(DefaultTimeout);

            set => _settings["Timeout"] = value;
        }

        [ConfigurationProperty("ThrottlingDelay", IsRequired = false)]
        public int ThrottlingDelay
        {
            get => (_settings["ThrottlingDelay"].ToString()).ToIntegerAlt(DefaultThrottlingDelay);

            set => _settings["ThrottlingDelay"] = value;
        }

        [ConfigurationProperty("VersionSegment", IsRequired = false)]
        public string VersionSegment
        {
            get => (_settings["VersionSegment"].ToString()).IsNullOrEmptyAlt(DefaultVersionSegment);

            set => _settings["VersionSegment"] = value;
        }

        [ConfigurationProperty("PageSize", IsRequired = false)]
        public int PageSize
        {
            get => (_settings["PageSize"].ToString()).ToIntegerAlt(DefaultPageSize);

            set => _settings["PageSize"] = value;
        }
    }
}

