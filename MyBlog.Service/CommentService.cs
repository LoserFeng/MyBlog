using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
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

        public async Task<Comment> QueryByGUIDAsync(string GUID)
        {
            return await _iCommentRepository.QueryByGUIDAsync(GUID);
        }
    }
}
