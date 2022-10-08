﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MyBlog.Model
{
    public class BlogNews : BaseId
    {
        //nvarchar 带中文比较好
        [SugarColumn(ColumnDataType ="nvarchar(30)")]
        public string Title { get; set; }
        [SugarColumn(ColumnDataType ="text")]
        public string Content { get; set; }
        public DateTime? Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }


        public string Introduction { get; set; }




        public int TypeId { get; set; }
        public int WriterId { get; set; }




        /// <summary>
        /// 导航查询，不映射到数据库
        /// </summary>
        [Navigate(NavigateType.OneToOne,nameof(TypeId))]
        public TagInfo TypeInfo { get; set; }


        [Navigate(NavigateType.OneToOne,nameof(WriterId))]
        public WriterInfo WriterInfo { get; set; }


        //算了，这个估计用不到，但是还是先加上好了。。。。
        [Navigate(typeof(Favorite), nameof(Favorite.BlogId), nameof(Favorite.UserId))]
        public List<UserInfo> Admirers { get; set; }


    }
}
