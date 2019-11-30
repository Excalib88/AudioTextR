using AudioTextR.Core.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioTextR.Core.Models
{
    public class WitAiModel: IRecognizeApiModel
    {
        public WitAiModel(Uri baseAddress, string token)
        {
            BaseAddress = baseAddress;
            ServerToken = token;
        }

        public WitAiModel()
        {
        }

        public string AppId { get; set; }

        public string ServerToken { get; set; }

        public string ClientToken { get; set; }

        public Uri BaseAddress { get; set; }

        public string RequestUri { get; set; }
    }
}
