namespace Push.Acumatica.Api.Common
{
    public class Address
    {
        public StringValue AddressLine1 { get; set; }
        public StringValue AddressLine2 { get; set; }
        public StringValue City { get; set; }
        public StringValue State { get; set; }
        public StringValue PostalCode { get; set; }
        public StringValue Country { get; set; }

        public Address Copy()
        {
            var output = new Address();
            output.AddressLine1 = this.AddressLine1.Copy();
            output.AddressLine2 = this.AddressLine2.Copy();
            output.City = this.City.Copy();
            output.State = this.State.Copy();
            output.PostalCode = this.PostalCode.Copy();
            output.Country = this.Country.Copy();
            return output;
        }
    }
}
