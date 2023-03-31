namespace Luiza.Labs.Domain.Models.Auth
{
    public sealed class SettingsOptions
    {
        //public const string Settings = nameof(Settings);
        public string Secret { get; set; } = string.Empty;
    }
}
