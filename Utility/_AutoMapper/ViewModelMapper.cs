using Markdig;
using Markdown.ColorCode;
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
            var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseColorCode()
            .Build();


            return new BlogViewModel
            {
                Id = blogNews.Id,
                Title = blogNews.Title,
                Content = blogNews.Content,
                ContentHtml = Markdig.Markdown.ToHtml(blogNews.Content, pipeline),
                CoverPhoto = blogNews.CoverPhoto,
                CreationTime = blogNews.Time,
                GUID = blogNews.GUID,
                BrowseCount=blogNews.BrowseCount,
                Path = blogNews.Path,
                Summary = blogNews.Summary,
                Tags = blogNews.Tags,
                WriterInfo = blogNews.WriterInfo,
                Url = "/" + Path.Combine("Blog", "Details? GUID ="+blogNews.GUID),
                Comments = blogNews.Comments
                
            };

        }
    }
}
