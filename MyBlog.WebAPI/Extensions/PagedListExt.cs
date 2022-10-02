using System.Text.Json;
using MyBlog.Model.ViewModels;
using X.PagedList;

namespace MyBlog.WebAPI.Extensions;

public static class PagedListExt {
    public static PaginationMetadata ToPaginationMetadata(this IPagedList page) {
        return new PaginationMetadata {
            PageCount = page.PageCount,
            TotalItemCount = page.TotalItemCount,
            PageNumber = page.PageNumber,
            PageSize = page.PageSize,
            HasNextPage = page.HasNextPage,
            HasPreviousPage = page.HasPreviousPage,
            IsFirstPage = page.IsFirstPage,
            IsLastPage = page.IsLastPage,
            FirstItemOnPage = page.FirstItemOnPage,
            LastItemOnPage = page.LastItemOnPage
        };
    }

    public static string ToPaginationMetadataJson(this IPagedList page) {
        return JsonSerializer.Serialize(ToPaginationMetadata(page));
    }
}