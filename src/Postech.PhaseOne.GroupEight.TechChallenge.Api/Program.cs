using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs;
using Postech.PhaseOne.GroupEight.TechChallenge.Api.Setup;
using Postech.PhaseOne.GroupEight.TechChallenge.Api.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

//builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IPipelineBehavior<,>));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddDependencyRepository();
builder.Services.AddDependencyHandler();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            var status = 500;
            var message = exceptionHandlerPathFeature.Error.Message;

            if (exceptionHandlerPathFeature?.Error is DomainException)
            {
                status = 404; // Bad Request                
            }
            else if (exceptionHandlerPathFeature?.Error is NotFoundException)
            {
                status = 401; // Not found
            } else
            {
                message = "Ocorreu um erro inesperado";
            }
            
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new DefaultOutput(false, message)); 
        }
    });
});


app.MapPost("/contacts", async (IMediator mediator, ContactInput request) =>
{
    return await mediator.Send(request);
}).WithName("SaveContacts")
.WithOpenApi();

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.Run();

