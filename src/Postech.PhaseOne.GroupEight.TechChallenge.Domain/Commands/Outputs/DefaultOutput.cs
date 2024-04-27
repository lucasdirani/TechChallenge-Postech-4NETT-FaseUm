using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs
{
    [ExcludeFromCodeCoverage]
    public record DefaultOutput
    {
        public DefaultOutput(bool success, string message, object data) 
        {
            Success = success;
            Message = message; 
            Data = data;
        }

        public DefaultOutput(bool success, string message)
        {
            Success = success;
            Message = message;          
        }

        public bool Success { get; init; }
        public string Message { get; init; }
        public object Data { get; init; }
    }
}