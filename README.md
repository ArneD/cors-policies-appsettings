[![NuGet](https://img.shields.io/nuget/v/CorsPolicySettings.svg)](https://www.nuget.org/packages/CorsPolicySettings)
[![NuGet](https://img.shields.io/nuget/dt/CorsPolicySettings.svg)](https://www.nuget.org/packages/CorsPolicySettings/)

# Cors Policy Settings

## How to use ([Sample](https://github.com/ArneD/cors-policies-appsettings/tree/master/src/Samples/CorsPolicySettings.Sample))

### Default

In appsettings.json

```json
{
  "CORSPolicies": [
    {
      "Name": "Test",
      "Methods": [ "GET", "POST" ],
      "Origins": [ "http://www.example.com" ]
    },
    {
      "Name": "AllowAll",
      "Headers": ["*"],
      "Methods": ["*"],
      "Origins":  ["*"] 
    }
  ]
}
```
Note: See [CorsPolicy](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.cors.infrastructure.corspolicy#Microsoft_AspNetCore_Cors_Infrastructure_CorsPolicy) documentation to see which properties to use

Startup.cs
```csharp
public void ConfigureServices(IServiceCollection services)
{    
    //Replaces the use of services.AddCors()
    services.AddCorsPolicies(Configuration);
    
    /// ...

    // Add framework services.
    services.AddMvc();

    /// ...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{   
    if (env.IsDevelopment())
    {
        app.UseCors("AllowAll");
    }
    else
    {
        app.UseCors("Test");
    }

    /// ...

    app.UseMvc();
}
```

### Custom

In any json settings file (example: cors.json)
```json
{
  "Policies": [
    {
      "Name": "Test",
      "Methods": [ "GET", "POST" ],
      "Origins": [ "http://www.example.com" ]
    },
    {
      "Name": "AllowAll",
      "Headers": ["*"],
      "Methods": ["*"],
      "Origins":  ["*"] 
    }
  ]
}
```

Startup.cs

Add you json file to the configuration builder.
```csharp
public Startup(IHostingEnvironment env)
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddJsonFile("cors.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    Configuration = builder.Build();
}
```

Define the section where the policies should be read.
```csharp
public void ConfigureServices(IServiceCollection services)
{    
    //Replaces the use of services.AddCors()
    services.AddCorsPolicies(Configuration.GetSection("Policies"));
    
    /// ...

    // Add framework services.
    services.AddMvc();

    /// ...
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{   
    if (env.IsDevelopment())
    {
        app.UseCors("AllowAll");
    }
    else
    {
        app.UseCors("Test");
    }

    /// ...

    app.UseMvc();
}
```