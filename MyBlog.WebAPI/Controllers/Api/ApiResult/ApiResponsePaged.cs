using MyBlog.Model.ViewModels;
using MyBlog.WebAPI.Extensions;
using X.PagedList;

namespace MyBlog.WebAPI.Controllers.Api.ApiResult;

public class ApiResponsePaged<T> : ApiResponse<List<T>> where T : class {
    public ApiResponsePaged() {
    }

    public ApiResponsePaged(IPagedList<T> pagedList) {
        Data = pagedList.ToList();
        Pagination = pagedList.ToPaginationMetadata();
    }

    public PaginationMetadata? Pagination { get; set; }
}