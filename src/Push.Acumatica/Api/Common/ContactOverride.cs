namespace Push.Acumatica.Api.Common
{
    public class ContactOverride
    {
        public StringValue Attention { get; set; }
        public StringValue BusinessName { get; set; }
        public StringValue Email { get; set; }
        public StringValue Phone1 { get; set; }

        public ContactOverride Copy()
        {
            var output = new ContactOverride();
            output.Attention = this.Attention.Copy();
            output.BusinessName = this.BusinessName.Copy();
            output.Email = this.Email.Copy();
            output.Phone1 = this.Phone1.Copy();
            return output;
        }
    }
}
