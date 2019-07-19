using BLL.Abstract;
using BLL.Results;
using Blog.Entities;
using Blog.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AboutManager : ManagerBase<About>
    {
        public BusinessLayerResult<About> InsertAboutFoto(About about)
        {
            BusinessLayerResult<About> res = new BusinessLayerResult<About>();


            if (string.IsNullOrEmpty(about.AboutImage) == false)
            {
                res.Result.AboutImage = about.AboutImage;
            }

            if (base.Insert(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
        public BusinessLayerResult<About> UpdateAboutFoto(About about)
        {
            BusinessLayerResult<About> res = new BusinessLayerResult<About>();
            res.Result = Find(x => x.Id == about.Id);
            res.Result.Description = about.Description;
            res.Result.Title = about.Title;

            if (string.IsNullOrEmpty(about.AboutImage) == false)
            {
                res.Result.AboutImage = about.AboutImage;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
    }
}
