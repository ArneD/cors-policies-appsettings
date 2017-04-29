using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorsPolicySettings
{
    public static class CorsPolicySettingsExtensions
    {
        public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.AddOptions();

            var corsPolicies = configurationRoot.GetSection("CORSPolicies").Get<IEnumerable<CorsPolicySetting>>();
            
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
