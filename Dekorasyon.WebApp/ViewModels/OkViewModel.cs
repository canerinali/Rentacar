using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dekorasyon.WebApp.ViewModels
{
    public class OkViewModel: NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "İşlem Başarılı.";
        }
    }
}