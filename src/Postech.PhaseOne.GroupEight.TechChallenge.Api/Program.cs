using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Net;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(configuration);
builder.Services.AddMediatR();
builder.Services.AddDependencyRepository();
builder.Services.AddDependencyFactory();
builder.Services.AddDependencyHandler();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            int status = (int) HttpStatusCode.InternalServerError;
            string message = exceptionHandlerPathFeature.Error.Message;
            if (exceptionHandlerPathFeature?.Error is DomainException)
            {
                status = (int) HttpStatusCode.BadRequest;               
            }
            else if (exceptionHandlerPathFeature?.Error is NotFoundException)
            {
                status = (int) HttpStatusCode.NotFound;
            } 
            else
            {
                message = "Ocorreu um erro inesperado";
            }        
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new DefaultOutput(false, message)); 
        }
    });
});

app.MapPost("/contacts", async (IMediator mediator, [FromBody] ContactInput request) =>
{
    return await mediator.Send(request);
})
.WithName("Register Contact")
.WithOpenApi();

app.Run();