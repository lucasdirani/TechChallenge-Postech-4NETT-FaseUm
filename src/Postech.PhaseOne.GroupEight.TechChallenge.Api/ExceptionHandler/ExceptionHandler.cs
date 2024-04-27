using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Api.Filters
{
    public class ExceptionHandler 
    {
        public RequestDelegate Next { get; }

        public ExceptionHandler(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                // Log the exception, handle it, or return a custom response
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
            }
        }
    }
}
