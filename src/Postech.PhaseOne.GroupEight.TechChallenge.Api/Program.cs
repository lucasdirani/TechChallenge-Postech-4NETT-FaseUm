using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tech Challenge 2024 Documentation",
        Version = "v1",
        Description = "Responsible students: Ricardo Fulgencio, Breno Gomes, Lucas Pinho, Lucas Ruiz, and Tatiana Lima"
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

app.MapPut("/contacts", async (IMediator mediator, [FromBody] UpdateContactInput request) =>
{
    return await mediator.Send(request);
})
.WithName("Update Contact")
.WithMetadata(new SwaggerOperationAttribute
                        ("Modify a contact in the Agenda",
                        "Modifies a contact in the agenda according to the provided data"))
.WithMetadata(new SwaggerParameterAttribute("Data of the new contact"))
.WithMetadata(new SwaggerResponseAttribute(200, "Contact updated"))
.WithMetadata(new SwaggerResponseAttribute(400, "Invalid request"))
.WithMetadata(new SwaggerResponseAttribute(500, "Unexpected error"))
.WithOpenApi();
app.Run();

public partial class Program
{
    protected Program() { }
}