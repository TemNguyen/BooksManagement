using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QLSach.DTO
{
    class CreateDB : CreateDatabaseIfNotExists<CSDL>
    {
        protected override void Seed(CSDL context)
        {

            context.Authors.AddRange(new Author[]
            {
                new Author {Author_ID = 1, Author_Name = "Nguyen Van A"},
                new Author {Author_ID = 2, Author_Name = "Nguyen Thi B"}
            });

            context.Books.AddRange(new Book[]
            {
                new Book {ID = "1", Name = "Truyen Viet Nam", ReleaseDate = DateTime.Now, IsEbook = true, Author_ID = 1},
                new Book {ID = "2", Name = "Truyen Trung Quoc", ReleaseDate = DateTime.Now, IsEbook = false, Author_ID = 2},
                new Book {ID = "3", Name = "Truyen Han Quoc", ReleaseDate = DateTime.Now, IsEbook = true, Author_ID = 1}
            });

        }
    }
}
