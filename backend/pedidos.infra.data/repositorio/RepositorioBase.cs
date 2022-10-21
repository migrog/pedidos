using System.Collections.Generic;
using System.Linq;
using pedidos.dominio.entidades;
using pedidos.dominio.entidades.interfaces.repositorio;
using pedidos.infra.data.context;
using Microsoft.EntityFrameworkCore;

namespace pedidos.infra.data.repositorio
{
    public class RepositorioBase<T>: IRepositorioBase<T> where T: EntidadBase
    {
        protected pedidosContext context = new pedidosContext();
        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        public void Delete(IEnumerable<T> lista)
        {
            context.Set<T>().RemoveRange(lista);
            context.SaveChanges();
        }


        public IList<T> Select()
        {
            return context.Set<T>().ToList();
        }

        public T Select(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T Select(string id)
        {
            return context.Set<T>().Find(id);
        }
    }
}
