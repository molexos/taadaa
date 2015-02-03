using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using taadaa.api.Provider;

namespace taadaa.api
{
    [assembly: OwinStartup(typeof(taadaa.api.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                    Provider = new SimpleAuthorizationServerProvider()
                });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseWebApi(config);
        }
    }
}