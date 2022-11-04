using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class CommentService:BaseService<Comment>, ICommentService
    {
        private readonly ICommentRepository _iCommentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            base._iBaseRepository=commentRepository;
            _iCommentRepository=commentRepository;
        }

        public async Task<bool> DeleteByGUIDAsync(string GUID)
        {
            return await _iCommentRepository.DeleteByGUIDAsync(GUID);
        }

        public async Task<List<CommentModel>> GetCommentList(int page, int limit, RefAsync<int> total)
        {

            var commentList = new List<CommentModel>();
            var comments = await _iCommentRepository.QueryAsync(page, limit, total);
            foreach(var comment in comments)
            {
                commentList.Add( new CommentModel
                {
                    Id = comment.Id,
                    SupportCount = comment.SupportCount,
                    BlogId = comment.BlogId,
                    Content = comment.Content,
                    FatherId = comment.FatherId,
                    GUID = comment.GUID,
                    TargetName = comment.TargetName,
                    Time = comment.Time,
                    UserId = comment.UserId

                });


            }
            return commentList;


        }

        public async Task<List<Comment>> GetSocietyAsync()
        {


            return await _iCommentRepository.GetCommentsByBlogId(0);
        }

        public async Task<List<CommentModel>> GetSocietyList(int page, int limit, RefAsync<int> total)
        {
            int blogId = 0;

            var commentList = new List<CommentModel>();
            var comments = await _iCommentRepository.QueryAsync(blogId,page, limit, total);
            foreach (var comment in comments)
            {
                commentList.Add(new CommentModel
                {
                    Id = comment.Id,
                    SupportCount = comment.SupportCount,
                    BlogId = comment.BlogId,
                    Content = comment.Content,
                    FatherId = comment.FatherId,
                    GUID = comment.GUID,
                    TargetName = comment.TargetName,
                    Time = comment.Time,
                    UserId = comment.UserId

                });


            }
            return commentList;
        }

        public async Task<Comment> QueryByGUIDAsync(string GUID)
        {
            return await _iCommentRepository.QueryByGUIDAsync(GUID);
        }



        public override async Task<bool>UpdateAsync(Comment comment)
        {

            var pre_Comment =await  _iCommentRepository.FindByIdAsync(comment.Id);
            if(pre_Comment == null)
            {
                Console.WriteLine("找不到对应ID的Comment");

                return false;

            }
            Comment update_Comment = pre_Comment;
            update_Comment.Content = comment.Content == null ? pre_Comment.Content : comment.Content;
            update_Comment.SupportCount = comment.SupportCount == null ? pre_Comment.SupportCount : comment.SupportCount;

            

            return await _iCommentRepository.UpdateAsync(update_Comment);

        }

    }
}
