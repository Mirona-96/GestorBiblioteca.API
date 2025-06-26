using FluentValidation;
using GestorBibliotecaApplication.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Validators
{
    public class CreateUserValidator: AbstractValidator<CreateUsuarioInputModel>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("email invalido");
        }
    }
}
