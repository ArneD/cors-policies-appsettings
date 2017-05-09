using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorsPolicySettings
{
    public static class CorsPolicySettingsExtensions
    {
        public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.AddCorsPolicies(configurationRoot.GetSection("CORSPolicies"));
        }

        public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfigurationSection configurationSection)
        {
            var corsPolicies = configurationSection.Get<IEnumerable<CorsPolicySetting>>();

            serviceCollection.AddCors(builder =>
            {
                foreach (var corsPolicy in corsPolicies)
                {
                    builder.AddPolicy(corsPolicy.Name, corsPolicy);
                }
            });
        }
    }
}
