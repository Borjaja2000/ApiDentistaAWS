using ApiDentistaAWS.Data;
using ApiDentistaAWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Repositories
{
    public class RepositoryTratamientos
    {
        DentistaContext context;
        public RepositoryTratamientos(DentistaContext context)
        {
            this.context = context;
        }

        public List<Tratamiento> GetTratamientos()
        {
            return this.context.Tratamientos.ToList();

        }

        public Tratamiento BuscarTratamiento(int idtratamiento)
        {
            return this.context.Tratamientos
                .SingleOrDefault(x => x.IdTratamiento == idtratamiento);
        }

        public Tratamiento ExisteTratamiento(String nombre, int idtratamiento)
        {
            return this.context.Tratamientos
                .SingleOrDefault(x => x.Nombre == nombre
                && x.IdTratamiento == idtratamiento);
        }
        public void InsertarTratamiento(int idtratamiento, String nombre
            , int precio, String detalles, String descripcion, String duracion, String dentista, String imagen)
        {
            Tratamiento tratamiento = new Tratamiento();
            tratamiento.IdTratamiento = idtratamiento;
            tratamiento.Nombre = nombre;
            tratamiento.Precio = precio;
            tratamiento.Detalles = detalles;
            tratamiento.Descripcion = descripcion;
            tratamiento.Duracion = duracion;
            tratamiento.Dentista = dentista;
            tratamiento.Imagen = imagen;
            this.context.Tratamientos.Add(tratamiento);
            this.context.SaveChanges();
        }

        public void ModificarTratamiento(int idtratamiento, String nombre
            , int precio, String detalles, String descripcion, String duracion, String dentista, String imagen)
        {
            Tratamiento tratamiento = new Tratamiento();
            tratamiento.IdTratamiento = idtratamiento;
            tratamiento.Nombre = nombre;
            tratamiento.Precio = precio;
            tratamiento.Detalles = detalles;
            tratamiento.Descripcion = descripcion;
            tratamiento.Duracion = duracion;
            tratamiento.Dentista = dentista;
            tratamiento.Imagen = imagen;
            this.context.SaveChanges();
        }

        public void EliminarTratamiento(int idtratamiento)
        {
            Tratamiento tratamiento = this.BuscarTratamiento(idtratamiento);
            this.context.Tratamientos.Remove(tratamiento);
            this.context.SaveChanges();
        }


    }
}

