namespace CorsPolicySettings
{
    public class CorsPolicySetting : Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy
    {
        public string Name { get; set; }
    }
}
