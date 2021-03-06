using ApiDentistaAWS.Helper;
using ApiDentistaAWS.Models;
using ApiDentistaAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Controllers
{
    //http://www.apidentista/auth
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RepositoryClientes repo;
        HelperToken helpertoken;

        public AuthController(RepositoryClientes repo
            , HelperToken helpertoken)
        {
            this.repo = repo;
            this.helpertoken = helpertoken;
        }

        //EL PUNTO DE ENTRADA PARA LA VALIDACION SE REALIZA
        //MEDIANTE POST
        //RECIBIREMOS LA CLASE LOGINMODEL
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginModel model)
        {
            Cliente cliente =
                this.repo.ExisteCliente(model.UserName
                , model.Password);
            if (cliente == null)
            {
                //DEVOLVEMOS UNA RESPUESTA HTTP
                //DE NO AUTORIZADO
                return Unauthorized();
            }
            else
            {
                //EL USUARIO O LO QUE DESEEMOS SE ALMACENA DENTRO
                //DEL TOKEN MEDIANTE Claim
                //UN CLAIM PERMITE ALMACENAR DATOS POR KEY, VALUE
                //NECESITAMOS ALMACENAR EL OBJETO EMPLEADO EN EL TOKEN
                String clientejson = JsonConvert.SerializeObject(cliente);
                Claim[] claims = new[] {
                    new Claim("UserData", clientejson)
                };

                //NECESITAMOS GENERAR UN TOKEN QUE LLEVARA
                //INFORMACION DE NUESTRO API (ISSUER)
                //EL TIEMPO DE DURACION O LAS CREDENCIALES
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.helpertoken.Issuer,
                    audience: this.helpertoken.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: 
                    new SigningCredentials(this.helpertoken.GetKeyToken()
                    , SecurityAlgorithms.HmacSha256));
                //DEVOLVEMOS UNA RESPUESTA DE ACCESO
                //QUE INCLUIRA EL TOKEN GENERADO
                return Ok(
                    new
                    {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
