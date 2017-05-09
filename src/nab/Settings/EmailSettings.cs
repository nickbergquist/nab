namespace nab.Settings
{
    /// <summary>
    /// E-mail settings - deserialize from appsettings.json during startup 
    /// </summary>
    public class EmailSettings
    {
        public string ToName { get; set; }
        public string ToAddress { get; set; }

        public string LocalDomain { get; set; }

        public string MailServerAddress { get; set; }
        public string MailServerPort { get; set; }

        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}
