using System;
using System.Collections.Generic;
using System.Text;

namespace AudioTextR.Core.Abstractions.Models
{
    public interface IRecognizeResponse
    {
        string Text { get; set; }
    }
}
