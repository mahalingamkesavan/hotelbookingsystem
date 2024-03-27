using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public bool Error { get; set; } = false;
        public string? Message { get; set; }
        public string? Token { get; set; }
        public T? Result { get; set; }
        public int NumResult { get; set; } = 1;
        public int? TotalResult { get; set; }

        public static BaseResponse<T> GetResult
            (
            T? result, 
            int totalResult, 
            string? token, 
            string? message, 
            int statuscode = StatusCodes.Status200OK,
            bool error = false
            )
        {
            var response = new BaseResponse<T>();
            response.StatusCode = statuscode;
            response.Result = result;
            response.Token = token;
            if (result is IEnumerable<object>)
            {
                response.NumResult = ((IEnumerable<object>)result).Count();
            }
            response.TotalResult = totalResult;
            if (message != null)
            {
                response.Message = message;
            }
            return response;
        }
        public static BaseResponse<T> GetSuccessResult(T? result, string? message = null)
        {
            return GetSuccessResult(result, 1, message);
        }
        public static BaseResponse<T> GetSuccessResult(string token, string? message = null)
        {
            return GetResult(default, 0, token, message);
        }

        public static BaseResponse<T> GetSuccessResult(T? result, int totalResult, string? message = null)
        {
            return GetResult(result, totalResult, null, message);
        }
        public static BaseResponse<T> GetError(T? result, string? message = null, int statusCodes = StatusCodes.Status400BadRequest)
        {
            return GetResult(result, 0, null, message, statusCodes, true);
        }
        public static BaseResponse<T> GetError(string? message = null, int statusCodes = StatusCodes.Status400BadRequest)
        {
            return GetResult(default, 0, null, message,statusCodes,true);
        }
    }
}
