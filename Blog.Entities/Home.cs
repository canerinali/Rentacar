using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities
{
    public class Home : MyEntityBase
    {
        [DisplayName("Anasayfa Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }
        [DisplayName("Anasayfa Açıklaması"), Required, StringLength(2000)]
        public string Description { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string HomeImage { get; set; }
    }
}