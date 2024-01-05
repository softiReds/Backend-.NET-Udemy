using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto> //  AbstractValidator<DtoOrClass> -> Crea una herencia para realizar validaciones con el DtoOrClass especificado
    {
        public BeerInsertValidator() 
        {
            RuleFor(e => e.Name).NotEmpty();    //  RuleFor(e => e.Attributte)... -> Se utiliza para especificar reglas de validacion para propidades especificas de un objeto
                                                //      NotEmpty() -> Valida que la propidad no esté vacia al momento de ejecutar el validador
        }
    }
}
