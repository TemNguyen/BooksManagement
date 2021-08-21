using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSach.DTO
{
    public class Author
    {
        [Key]
        public int Author_ID { get; set; }
        public string Author_Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}
