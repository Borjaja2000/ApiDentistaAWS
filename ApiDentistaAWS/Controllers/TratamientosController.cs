
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

namespace ApiEmpleadosOAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        RepositoryTratamientos repo;

        public TratamientosController(RepositoryTratamientos repo)
        {
            this.repo = repo;
        }

        //[Authorize]
        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<Tratamiento>> GetTratamientos()
        {
            return this.repo.GetTratamientos();
        }

        [HttpGet("{id}")]
        public ActionResult<Tratamiento> BuscarTratamiento(int idtratamiento)
        {
            return this.repo.BuscarTratamiento(idtratamiento);
        }

        //LOGICAMENTE, NECESITAMOS QUE ESTE METODO TENGA SEGURIDAD
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<Tratamiento> PerfilTratamiento()
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
            Tratamiento empleado =
                JsonConvert.DeserializeObject<Tratamiento>(jsonempleado);
            return empleado;
        }
        [HttpPost]
        public void InsertarTratamiento(Tratamiento tratamiento)
        {
            this.repo.InsertarTratamiento(tratamiento.IdTratamiento
                , tratamiento.Nombre, tratamiento.Precio, tratamiento.Detalles, tratamiento.Descripcion, tratamiento.Duracion, tratamiento.Dentista, tratamiento.Imagen);
        }

        //PUT api/[controller]
        [HttpPut]
        public void ModificarTratamiento(Tratamiento tratamiento)
        {
            this.repo.ModificarTratamiento(tratamiento.IdTratamiento
                , tratamiento.Nombre, tratamiento.Precio, tratamiento.Detalles, tratamiento.Descripcion, tratamiento.Duracion, tratamiento.Dentista, tratamiento.Imagen);
        }

        //DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public void EliminarTratamiento(int idtratamiento)
        {
            this.repo.EliminarTratamiento(idtratamiento);
        }

    }
}
