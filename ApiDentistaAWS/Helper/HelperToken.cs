using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Helper
{
    public class HelperToken
    {
        public String Issuer { get; set; }

        public String Audience { get; set; }

        public String SecretKey { get; set; }

        public HelperToken(IConfiguration configuration)
        {
            this.Issuer = configuration["ApiOAuth:Issuer"];
            this.Audience = configuration["ApiOAuth:Audience"];
            this.SecretKey = configuration["ApiOAuth:SecretKey"];
        }

        //Method for Token
        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = Encoding.UTF8.GetBytes(this.SecretKey);
            return new SymmetricSecurityKey(data);
        }

        //Method for configuration to Token Security
        //the methods of configuration are action
        public Action<JwtBearerOptions> GetJwtBearerOptions()
        {
            Action<JwtBearerOptions> jwtoptions =
                new Action<JwtBearerOptions>(options => {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Issuer,
                        ValidAudience = this.Audience,
                        IssuerSigningKey = this.GetKeyToken()
                    };
                });
            return jwtoptions;
        }

        //Method action for option to authentication
        public Action<AuthenticationOptions> GetAuthOptions()
        {
            Action<AuthenticationOptions> authoptions =
                new Action<AuthenticationOptions>
                (options => {
                    options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                });
            return authoptions;
        }
    }
}
