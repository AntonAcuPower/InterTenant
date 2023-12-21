namespace Push.Acumatica.Utility
{
    public class Paging
    {
        public static string QueryStringParams(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return $"$top={pageSize}&$skip={skip}";
        }
    }
}
