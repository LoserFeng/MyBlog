using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebAPI.Controllers.Api.ApiResult
{
    public interface ILayUIResponse
    {
        public string Msg { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
    }


    public interface ILayUIResponse<T>:ILayUIResponse
    {
        public T? Data { get; set; }
    }



}
