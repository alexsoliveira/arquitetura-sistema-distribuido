using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASD.Identidade.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected IActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
                return Ok(result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Errors.ToArray() }
            }));
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach(var error in errors)
            {
                AdicionarErroProcessamento(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Errors.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Errors.Add(erro);
        }

        protected void LimparErroProcessamento()
        {
            Errors.Clear();
        }
    }
}
