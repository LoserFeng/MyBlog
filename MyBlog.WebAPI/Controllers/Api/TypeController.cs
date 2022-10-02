using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;

using MyBlog.WebAPI.Controllers.Api.ApiResult;

namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {


        private readonly ITypeInfoService _typeInfoService;

        public TypeController(ITypeInfoService typeInfoService)
        {
            _typeInfoService = typeInfoService;

        }

        [HttpPost("Create")]
        public async Task<IApiResponse> CreateType(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return ApiResponse.Error(Response,"类型名不能为空");


            var typeInfo = new TypeInfo
            {
                Name = name
            };
            bool b = await _typeInfoService.CreateAsync(typeInfo);
            if (!b) return ApiResponse.Error(Response,"添加失败:" + b);
            return ApiResponse.Ok(typeInfo);

        }


        [HttpGet("AllTypes")]
        public async Task<IApiResponse> GetAllTypes()
        {
            var types = await _typeInfoService.QueryAllAsync();
            //if (types.Count() == 0) return ApiResultHelper.Error("查询不到任何类型数据");  //这里我不想要返回500错误，我认为空集合也可以
            return ApiResponse.Ok(types);

        }



        [HttpDelete("Delete")]
        public async Task<ApiResponse> DeleteType(int id)
        {

            bool b = await _typeInfoService.DeleteByIdAsync(id);

            if (!b) return ApiResponse.Error(Response,"没有找到该文章类型");
            return ApiResponse.Ok(b);



        }
        [HttpPut("Edit")]
        public async Task<ApiResponse> EditType(int id, string name)
        {

            var type = await _typeInfoService.FindByIdAsync(id);

            if (type == null) return ApiResponse.Error(Response,"没有找到该文章类型");

            type.Name = name;
            bool b = await _typeInfoService.UpdateAsync(type);
            if (!b) return ApiResponse.Error(Response,"修改失败");

            return ApiResponse.Ok(type);



        }





    }
}
