using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs
{
    /// <summary>
    /// Default return object from endpoints.
    /// </summary>
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

        /// <summary>
        /// Indicates whether the requested processing was completed successfully.
        /// </summary>
        public bool Success { get; init; }

        /// <summary>
        /// Message that describes the result of processing.
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Set of data returned from the requested processing.
        /// </summary>
        public object Data { get; init; }
    }
}