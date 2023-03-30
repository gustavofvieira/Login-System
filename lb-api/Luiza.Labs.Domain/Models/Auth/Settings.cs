namespace Luiza.Labs.Domain.Models.Auth
{
    public class SettingsOptions
    {
        public const string Settings = "Settings";
        public string Secret { get; set; } = string.Empty;
    }
}
