using System.Collections.Generic;
using FluentValidation;

namespace pedidos.dominio.entidades.interfaces.servicios
{
    public interface IServicioBase<T> where T: EntidadBase
    {
        T Post<V>(T obj) where V : AbstractValidator<T>;

        T Put<V>(T obj) where V : AbstractValidator<T>;

        void Delete(int id);

        void Delete(IEnumerable<T> lista);

        T Get(int id);

        T Get(string id);

        IList<T> Get();
    }
}
