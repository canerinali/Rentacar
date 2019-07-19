using BLL.Abstract;
using BLL.Results;
using Blog.Entities;
using Blog.Entities.Messages;
using Blog.Entities.ValueObject;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostManager : ManagerBase<Post>
    {
        public BusinessLayerResult<Post> InsertPostFoto(Post post)
        {
            BusinessLayerResult<Post> res = new BusinessLayerResult<Post>();


            if (string.IsNullOrEmpty(post.PostImageFilename) == false)
            {
                res.Result.PostImageFilename = post.PostImageFilename;
            }

            if (base.Insert(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
        public BusinessLayerResult<Post> UpdatePostFoto(Post post)
        {
            BusinessLayerResult<Post> res = new BusinessLayerResult<Post>();
            res.Result = Find(x => x.Id == post.Id);
            res.Result.IsDraft = post.IsDraft;
            res.Result.CategoryId = post.CategoryId;
            res.Result.Text = post.Text;
            res.Result.Title = post.Title;

            if (string.IsNullOrEmpty(post.PostImageFilename) == false)
            {
                res.Result.PostImageFilename = post.PostImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
       
    }
}
