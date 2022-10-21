using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.entidades;
using FluentValidation;

namespace pedidos.dominio.core.validadores
{
    public class ClienteValidator: AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Ingrese Nombre");
        }
    }
}
