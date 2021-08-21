using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSach.DTO
{
    public class Book
    {
        [Key]
        public string  ID { get; set; }
        public string  Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsEbook { get; set; }
        public int Author_ID { get; set; }
        [ForeignKey("Author_ID")]
        public virtual Author Author { get; set; }
    }
}
