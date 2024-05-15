using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Documentação do Tech challenge 2024", 
        Version = "v1", 
        Description = "Alunos responsáveis: Ricardo Fulgencio, Breno Gomes, Lucas Pinho, Lucas Ruiz e Tatiana Lima"
    });
    c.EnableAnnotations();
});
builder.Services.AddDbContext(configuration);
builder.Services.AddMediatR();
builder.Services.AddDependencyRepository();
builder.Services.AddDependencyFactory();
builder.Services.AddDependencyHandler();
builder.Services.AddMiniProfiler();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiniProfiler();
    app.UseSwagger();
    app.MapSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));        
}
app.UseHttpsRedirection();
app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            int statusCode = (int) HttpStatusCode.InternalServerError;
            string errorMessage = exceptionHandlerPathFeature.Error.Message;
            if (exceptionHandlerPathFeature?.Error is DomainException)
            {
                statusCode = (int) HttpStatusCode.BadRequest;               
            }
            else if (exceptionHandlerPathFeature?.Error is NotFoundException)
            {
                statusCode = (int) HttpStatusCode.NotFound;
            } 
            else
            {
                errorMessage = "Ocorreu um erro inesperado";
            }        
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new DefaultOutput(false, errorMessage)); 
        }
    });
});

app.MapPost("/contacts", async (IMediator mediator, [FromBody] AddContactInput request) =>
{
    DomainException.ThrowWhenThereAreErrorMessages(request.Validate());
    return await mediator.Send(request);
})
.WithName("Register Contact")
.WithMetadata(new SwaggerOperationAttribute(
        "Register a new contact",
        "Registers a new contact according to their first and last name, email address and phone number."
    )
)
.WithMetadata(new SwaggerParameterAttribute("New contact information"))
.WithMetadata(new SwaggerResponseAttribute(200, "Contact registered successfully"))
.WithMetadata(new SwaggerResponseAttribute(400, "The data provided for contact registration is invalid"))
.WithMetadata(new SwaggerResponseAttribute(500, "Unexpected error during contact registration"))
.WithOpenApi();

app.Run();

public partial class Program
{
    protected Program() { }
}