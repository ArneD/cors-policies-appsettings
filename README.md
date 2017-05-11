# Cors Policiy Settings

## How to use

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

