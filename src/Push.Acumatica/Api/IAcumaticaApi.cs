using Push.Acumatica.Http;

namespace Push.Acumatica.Api
{
    public interface IAcumaticaApi
    {
        void InjectHttpContext(AcumaticaHttpContext context);
    }
}
