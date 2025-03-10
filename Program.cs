using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Core;
// Remove the general namespace import and use fully qualified name instead
// using Microsoft.AspNetCore.Builder;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;
using AspNetWebApp = Microsoft.AspNetCore.Builder.WebApplication;
using GraphWebApp = Microsoft.Graph.Models.WebApplication;

namespace az_auth_demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Use fully qualified name for WebApplication
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services
                .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes: new[] { "User.Read" })
                .AddMicrosoftGraph()
                .AddInMemoryTokenCaches();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.Run();
        }
    }
}
