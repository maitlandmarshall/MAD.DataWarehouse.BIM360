namespace MAD.DataWarehouse.BIM360
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; } = "client_credentials";
        public string Scope { get; set; } = "user:read viewables:read data:read data:search account:read";
    }
}
