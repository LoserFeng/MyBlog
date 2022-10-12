using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Blog;

namespace Utility._AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo, WriterDTO>();
            base.CreateMap<BlogNews, BlogNewsDTO>();
           /* base.CreateMap<BlogNews, BlogNewsDTO>().ForMember( dst=>dst.WriterName,src=>src.MapFrom(Src2=>Src2->WriterInfo.Name);*/
            base.CreateMap<TagInfo, TypeInfoDTO>();

        }
    }
}
