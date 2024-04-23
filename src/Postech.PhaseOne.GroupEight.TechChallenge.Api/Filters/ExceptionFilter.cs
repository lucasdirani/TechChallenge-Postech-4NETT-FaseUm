using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            

            var status = 500;
            var mensagem = "Ocorreu um erro inesperado.";
            if (context.Exception is DomainException
                    || context.Exception.InnerException is DomainException
                    )
            {
                status = 400;
                mensagem = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message;

            }
            else if (context.Exception is NotFoundException
                   || context.Exception.InnerException is NotFoundException
                   )
            {
                status = 401;
                mensagem = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message;
            }


            var response = context.HttpContext.Response;

            response.StatusCode = status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new DefaultOutput(false, mensagem));
        }
    }
}
