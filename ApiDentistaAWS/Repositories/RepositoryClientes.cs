using ApiDentistaAWS.Data;
using ApiDentistaAWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS.Repositories
{
    public class RepositoryClientes
    {
        DentistaContext context;
        public RepositoryClientes(DentistaContext context)
        {
            this.context = context;
        }

        public List<Cliente> GetClientes()
        {
            return this.context.Clientes.ToList();
        }

        public Cliente BuscarCliente(int idcliente)
        {
            return this.context.Clientes
                .SingleOrDefault(x => x.IdCliente == idcliente);
        }

        public Cliente ExisteCliente(String usuario, String pass)
        {
            return this.context.Clientes
                .SingleOrDefault(x => x.Usuario == usuario
                && x.Password == pass);
        }
        public void InsertarCliente(int idcliente, String usuario
            , String pass, String nombre, String apellido, String domicilio, int edad, String dni, String telefono)
        {
            Cliente cliente = new Cliente();
            cliente.IdCliente = idcliente;
            cliente.Usuario = usuario;
            cliente.Password = pass;
            cliente.Nombre = nombre;
            cliente.Apellido = apellido;
            cliente.Domicilio = domicilio;
            cliente.Edad = edad;
            cliente.Dni = dni;
            cliente.Telefono = telefono;
            this.context.Clientes.Add(cliente);
            this.context.SaveChanges();
        }

        public void ModificarCliente(int idcliente, String usuario
            , String pass, String nombre, String apellido, String domicilio, int edad, String dni, String telefono)
        {
            Cliente cliente = new Cliente();
            cliente.IdCliente = idcliente;
            cliente.Usuario = usuario;
            cliente.Password = pass;
            cliente.Nombre = nombre;
            cliente.Apellido = apellido;
            cliente.Domicilio = domicilio;
            cliente.Edad = edad;
            cliente.Dni = dni;
            cliente.Telefono = telefono;
            this.context.SaveChanges();
        }

        public void EliminarCliente(int idcliente)
        {
            Cliente cliente = this.BuscarCliente(idcliente);
            this.context.Clientes.Remove(cliente);
            this.context.SaveChanges();
        }
    }
}
