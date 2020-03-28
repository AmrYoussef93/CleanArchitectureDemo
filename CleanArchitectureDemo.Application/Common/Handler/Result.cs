using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CleanArchitectureDemo.Application.Common.Handler
{
    public class Result<T>
    {
        public T Date { get; set; }
        public string[] Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool HasErrors
        {
            get
            {
                return Errors?.Length > 0;
            }
        }
        public bool IsSuccessfull
        {
            get
            {
                return !HasErrors && StatusCode != HttpStatusCode.InternalServerError && StatusCode != HttpStatusCode.BadRequest && StatusCode != HttpStatusCode.NotFound;
            }
        }
        private Result(T data)
        {
            StatusCode = HttpStatusCode.OK;
            if (data != null)
                Date = data;
        }
        private Result(HttpStatusCode statusCode, object data = null, string[] errorMessages = null)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
            if (data != null)
                Date = (T)data;
        }
        public static Result<T> Ok(T data)
        {
            return new Result<T>(data);
        }
        public static Result<T> Created(T data)
        {
            return new Result<T>(HttpStatusCode.Created, data);
        }
        public static Result<T> NoContent()
        {
            return new Result<T>(HttpStatusCode.NoContent);
        }
        public static Result<T> Unauthorized(string[] errors)
        {
            return new Result<T>(HttpStatusCode.Unauthorized, default(T), errors);
        }
        public static Result<T> BadRequest(string[] errors)
        {
            return new Result<T>(HttpStatusCode.BadRequest, default(T), errors);
        }
        public static Result<T> InternalServerError(string[] errors)
        {
            return new Result<T>(HttpStatusCode.InternalServerError, default(T), errors);
        }
        public static Result<T> NotFound()
        {
            return new Result<T>(HttpStatusCode.NotFound);
        }
    }
}
