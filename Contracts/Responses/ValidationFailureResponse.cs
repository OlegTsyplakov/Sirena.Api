﻿using System.Collections.Generic;

namespace Sirena.Api.Contracts.Responses
{
    public class ValidationFailureResponse
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}
