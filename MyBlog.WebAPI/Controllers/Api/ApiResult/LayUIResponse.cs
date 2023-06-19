using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.ObjectiveC;

namespace MyBlog.WebAPI.Controllers.Api.ApiResult
{
    public class LayUIResponse<T> : ILayUIResponse<T>
    {
        public T? Data { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }


        public LayUIResponse(){

        }

        public LayUIResponse(T ? data)
        {
            Data = data;
        }


        public static implicit operator LayUIResponse<T>(LayUIResponse response)
        {
            return new LayUIResponse<T>
            {

                Msg =response.Msg,
                Code = response.Code, 
                Count = response.Count
            };
        }

    }


    public class LayUIResponse : ILayUIResponse, IApiErrorResponse
    {
        public string Msg { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }

        public Object Data { get; set; }
        public SerializableError ErrorData { get; set; }

        public LayUIResponse()
        {
        }

        public LayUIResponse(Object Data)
        {
            this.Data = Data;
        }


        public static LayUIResponse NoContent(string message = "NoContent")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status204NoContent,

                Msg = message
            };
        }

        public static LayUIResponse Ok(string message = "Ok")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status200OK,
                Msg = message
            };
        }

        public static LayUIResponse Ok(object data, string message = "Ok")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status200OK,
                Msg = message,
                Data = data
            };
        }

        public static LayUIResponse Ok(object data,int count, string message = "Ok")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status200OK,
                Msg = message,
                Data = data,
                Count = count
            };
        }

        public static LayUIResponse Unauthorized(string message = "Unauthorized")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status401Unauthorized,
                
                Msg = message
            };
        }

        public static LayUIResponse NotFound(string message = "NotFound")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status404NotFound,
                
                Msg = message
            };
        }

        public static LayUIResponse BadRequest(string message = "BadRequest")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status400BadRequest,
                
                Msg = message
            };
        }

        public static LayUIResponse BadRequest(ModelStateDictionary modelState, string message = "ModelState is not valid.")
        {
            return new LayUIResponse
            {
                Code = StatusCodes.Status400BadRequest,

                Msg = message,
                ErrorData = new SerializableError(modelState)
            };
        }

        public static LayUIResponse Error(HttpResponse httpResponse, string message = "Error")
        {
            httpResponse.StatusCode = StatusCodes.Status500InternalServerError;
            return new LayUIResponse
            {
                Code = httpResponse.StatusCode,
                Msg = message
            };
        }

    }

}
