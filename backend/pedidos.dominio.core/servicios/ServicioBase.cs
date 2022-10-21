using System;
using System.Collections.Generic;
using System.Linq;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.interfaces.servicios;
using pedidos.infra.data.repositorio;
using FluentValidation;

namespace pedidos.dominio.core.servicios
{
    public class ServicioBase<T>: IServicioBase<T> where T: EntidadBase
    {
        private RepositorioBase<T> repository = new RepositorioBase<T>();
        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Insert(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Update(obj);
            return obj;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("El id no puede ser cero.");

            repository.Delete(id);
        }

        public void Delete(IEnumerable<T> list)
        {
            if (list.Count() == 0)
                throw new ArgumentException("No puede haber una lista vacia.");

            repository.Delete(list);
        }


        public IList<T> Get() => repository.Select();

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("El id no puede ser cero.");

            return repository.Select(id);
        }

        public T Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("El id no puede ser cero.");

            return repository.Select(id);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros no encontrados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
