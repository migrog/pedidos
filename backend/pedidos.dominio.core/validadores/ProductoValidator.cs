using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.entidades;
using FluentValidation;

namespace pedidos.dominio.core.validadores
{
    public class ProductoValidator: AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Ingrese Nombre");
            RuleFor(x => x.PrecioUnitario).NotEmpty().WithMessage("Ingrese PrecioUnitario");
            RuleFor(x => x.MonedaEnum).NotEmpty().WithMessage("Ingrese MonedaEnum");
        }
    }
}
