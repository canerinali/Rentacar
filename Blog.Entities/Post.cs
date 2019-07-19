using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities
{
    [Table("Posts")]
    public class Post:MyEntityBase
    {
        [DisplayName("Not Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }
        [DisplayName("Post Metni"), Required, StringLength(2000)]
        public string Text { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string PostImageFilename { get; set; }

        public bool IsDraft { get; set; }
        public int CategoryId { get; set; }

        public virtual BlogUser Owner { get; set; }
        public virtual Category Category { get; set; }
    }
}
