using MyBlog.Model;
using MyBlog.Model.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility._AutoMapper
{
    public class ViewModelMapper
    {

        public BlogViewModel GetBlogViewModel(BlogNews blogNews)
        {

            return new BlogViewModel
            {
                Id = blogNews.Id,
                Title = blogNews.Title,
                Content = blogNews.Content,
                ContentHtml = Markdig.Markdown.ToHtml(blogNews.Content),
                CoverPhoto = blogNews.CoverPhoto,
                CreationTime = blogNews.Time,
                GUID = blogNews.GUID,
                BrowseCount=blogNews.BrowseCount,
                Path = blogNews.Path,
                Summary = blogNews.Summary,
                Tags = blogNews.Tags,
                Url = "/" + Path.Combine("Blog", "Details", blogNews.GUID)
            };

        }
    }
}
