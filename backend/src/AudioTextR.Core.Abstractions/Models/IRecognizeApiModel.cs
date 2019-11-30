using System;

namespace AudioTextR.Core.Abstractions.Models
{
    public interface IRecognizeApiModel
    {
        Uri BaseAddress { get; set; }
        string ServerToken { get; set; }
        string RequestUri { get; set; }
    }
}
