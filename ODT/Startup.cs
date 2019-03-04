using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace ODT
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      // Toevoegen localisation services aan de applicatie
      services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

      // Configureren van talen die we zullen ondersteunen
      services.Configure<RequestLocalizationOptions>(
        opts =>
        {
          var supportedCultures = new List<CultureInfo>
          {
            new CultureInfo("en-US"),
            new CultureInfo("fr-FR"),
            new CultureInfo("nl-BE"),
            new CultureInfo("de-DE")
          };

          opts.DefaultRequestCulture = new RequestCulture("nl-BE");
          opts.SupportedCultures = supportedCultures;
          opts.SupportedUICultures = supportedCultures;
        });

      
      services.AddResponseCaching();

      services.AddMemoryCache();

      services.AddDistributedRedisCache(options => 
        { options.Configuration = "localhost:6379"; });
      
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      
      // CacheProfiles toevoegen
      services.AddMvc(options =>
      {
        options.CacheProfiles.Add("DefaultProfile",
          new CacheProfile()
          {
            Duration = 60
          });
        options.CacheProfiles.Add("NeverProfile",
          new CacheProfile()
          {
            Location = ResponseCacheLocation.None,
            NoStore = true
          });
      });
      
      services.AddMvc()
        .AddViewLocalization(
          LanguageViewLocationExpanderFormat.Suffix,
          opts => { opts.ResourcesPath = "Resources"; })
        .AddDataAnnotationsLocalization();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      //Configuratie van localization
      var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(options.Value);

      // response caching
      app.UseResponseCaching();

      app.UseMvcWithDefaultRoute();
      // end response caching
      
      /*app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });*/
    }
  }
}