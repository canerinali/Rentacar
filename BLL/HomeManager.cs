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
    public class HomeManager : ManagerBase<Home>
    {
        public BusinessLayerResult<Home> InsertPostFoto(Home home)
        {
            BusinessLayerResult<Home> res = new BusinessLayerResult<Home>();


            if (string.IsNullOrEmpty(home.HomeImage) == false)
            {
                res.Result.HomeImage = home.HomeImage;
            }

            if (base.Insert(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
        public BusinessLayerResult<Home> UpdatePostFoto(Home home)
        {
            BusinessLayerResult<Home> res = new BusinessLayerResult<Home>();
            res.Result = Find(x => x.Id == home.Id);
            res.Result.Description = home.Description;
            res.Result.Title = home.Title;

            if (string.IsNullOrEmpty(home.HomeImage) == false)
            {
                res.Result.HomeImage = home.HomeImage;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }
    }
}
