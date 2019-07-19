using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dekorasyon.WebApp.ViewModels
{
    public class InfoViewModel:NotifyViewModelBase<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme";
        }
    }
}