
using ApiDentistaAWS.Models;
using ApiDentistaAWS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiEmpleadosAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        RepositoryClientes repo;

        public ClientesController(RepositoryClientes repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Cliente>> GetClientes()
        {
            return this.repo.GetClientes();
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> BuscarCliente(int idcliente)
        {
            return this.repo.BuscarCliente(idcliente);
        }

        //LOGICAMENTE, NECESITAMOS QUE ESTE METODO TENGA SEGURIDAD
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<Cliente> PerfilCliente()
        {
            //UNA VEZ QUE NOS HEMOS VALIDADO CON EL TOKEN
            //ESTAMOS AQUI Y, EN NUESTRO API, TAMBIEN ESTAMOS
            //VALIDADOS
            //DEBEMOS RECUPERAR EL CLAIM DE UserData 
            //DE LOS CLAIMS DEL USUARIO DE LA APP
            List<Claim> claims =
                HttpContext.User.Claims.ToList();
            //BUSCAMOS EL JSON DEL EMPLEADO, GUARDADO CON LA KEY UserData
            String jsonempleado =
                claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Cliente cliente =
                JsonConvert.DeserializeObject<Cliente>(jsonempleado);
            return cliente;
        }
        [HttpPost]
        public void InsertarCliente(Cliente cliente)
        {
            this.repo.InsertarCliente(cliente.IdCliente
                , cliente.Usuario, cliente.Password,cliente.Nombre, cliente.Apellido, cliente.Domicilio, cliente.Edad, cliente.Dni,cliente.Telefono);
        }

        //PUT api/[controller]
        [HttpPut]
        public void ModificarCliente(Cliente cliente)
        {
            this.repo.ModificarCliente(cliente.IdCliente
                , cliente.Usuario, cliente.Password, cliente.Nombre, cliente.Apellido, cliente.Domicilio, cliente.Edad, cliente.Dni, cliente.Telefono);
        }

        //DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public void EliminarCliente(int idcliente)
        {
            this.repo.EliminarCliente(idcliente);
        }
    }
}
