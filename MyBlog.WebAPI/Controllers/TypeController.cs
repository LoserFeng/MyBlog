using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Utility.ApiResult;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {


        private readonly ITypeInfoService _typeInfoService;

        public TypeController(ITypeInfoService typeInfoService)
        {
            this._typeInfoService = typeInfoService;

        }

        [HttpPost("Create")]
        public async Task<ApiResult> CreateType(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名不能为空");


            var typeInfo = new TypeInfo
            {
                Name = name
            };
            bool b=await _typeInfoService.CreateAsync(typeInfo);
            if(!b)return ApiResultHelper.Error("添加失败:"+b);
            return ApiResultHelper.Success(typeInfo);

        }


        [HttpGet("AllTypes")]
        public async Task<ApiResult> GetAllTypes()
        {
            var types = await _typeInfoService.QueryAllAsync();
            //if (types.Count() == 0) return ApiResultHelper.Error("查询不到任何类型数据");  //这里我不想要返回500错误，我认为空集合也可以
            return ApiResultHelper.Success(types);

        }



        [HttpDelete("Delete")]
        public async Task<ApiResult> DeleteType(int id)
        {

            bool b = await _typeInfoService.DeleteByIdAsync(id);

            if (!b) return ApiResultHelper.Error("没有找到该文章类型");
            return ApiResultHelper.Success(b);

            
            
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> EditType(int id,string name)
        {
            
            var type = await _typeInfoService.FindByIdAsync(id);

            if (type == null) return ApiResultHelper.Error("没有找到该文章类型");

            type.Name = name;
            bool b = await _typeInfoService.UpdateAsync(type);
            if (!b) return ApiResultHelper.Error("修改失败");

            return ApiResultHelper.Success(type);



        }





    }
}
