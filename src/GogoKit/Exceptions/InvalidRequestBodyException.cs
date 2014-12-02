﻿using GogoKit.Http;
using GogoKit.Models;

namespace GogoKit.Exceptions
{
    public class InvalidRequestBodyException : ApiErrorException
    {
        public InvalidRequestBodyException(IApiResponse<ApiError> response)
            : base(response)
        {
        }
    }
}