using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSach.DTO
{
    class BookViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsEbook { get; set; }
        public string Author_Name { get; set; }

        public static bool CmpID(BookViewModel b1, BookViewModel b2)
        {
            if (String.Compare(b1.ID, b2.ID) > 0)
                return true;
            return false;
        }
        public static bool CmpName(BookViewModel b1, BookViewModel b2)
        {
            if (String.Compare(b1.Name, b2.Name) > 0)
                return true;
            return false;
        }
        public static bool CmpReleaseDate(BookViewModel b1, BookViewModel b2)
        {
            if (b1.ReleaseDate > b2.ReleaseDate)
                return true;
            return false;
        }
        public static bool CmpIsEBook(BookViewModel b1, BookViewModel b2)
        {
            if (b1.IsEbook && b2.IsEbook == false)
                return true;
            return false;
        }
        public static bool CmpAuthorName(BookViewModel b1, BookViewModel b2)
        {
            if (String.Compare(b1.Author_Name, b2.Author_Name) > 0)
                return true;
            return false;
        }
    }
}
