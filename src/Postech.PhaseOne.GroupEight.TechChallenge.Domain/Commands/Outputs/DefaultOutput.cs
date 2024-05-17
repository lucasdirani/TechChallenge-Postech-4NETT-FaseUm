using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs
{
    [ExcludeFromCodeCoverage]
    public record DefaultOutput
    {
        public DefaultOutput()
        {        
        }

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
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        /// <summary>
        /// Message that describes the result of processing.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; init; }

        /// <summary>
        /// Set of data returned from the requested processing.
        /// </summary>
        [JsonPropertyName("data")]
        public object Data { get; init; }
    }
}