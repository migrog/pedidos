using System.Collections.Generic;

namespace pedidos.dominio.entidades.interfaces.repositorio
{
    public interface IRepositorioBase<T> where T: EntidadBase
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);
        void Delete(IEnumerable<T> lista);

        T Select(int id);

        T Select(string id);

        IList<T> Select();
    }
}
