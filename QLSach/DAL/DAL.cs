using QLSach.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSach.DAL
{
    class DAL
    {
        CSDL db = new CSDL();
        private static DAL _Instance;
        public static DAL Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DAL();
                return _Instance;
            }
            private set { }
        }
        private DAL()
        {

        }
        /// <summary>
        /// Get toàn bộ sách thõa mãn điều kiện
        /// </summary>
        /// <param name="AuthorID">ID tác giả</param>
        /// <param name="property_Name">Thuộc tính cần search</param>
        /// <param name="property_Value">Giá trị cần search</param>
        /// <returns></returns>
        public List<Book> GetBooks(int AuthorID, string property_Name, string property_Value)
        {
            List<Book> searchedBook = GetBooksBy(property_Name, property_Value);
            List<Book> books = new List<Book>();
            if (AuthorID == 0)
            {
                books = searchedBook;
            }
            else
            {
                foreach(var book in searchedBook)
                {
                    if (book.Author_ID == AuthorID)
                        books.Add(book);
                }
            }
            return books;
        }
        private List<Book> GetBooksBy(string property_Name, string value)
        {
            List<Book> books = new List<Book>();
            if (value == "")
            {
                books = db.Books.ToList();
            }
            else
            {
                switch (property_Name)
                {
                    case "ID":
                        books = db.Books.Where(p => p.ID == value).ToList();
                        break;
                    case "Name":
                        books = db.Books.Where(p => p.Name.ToUpper().Contains(value.ToUpper())).ToList();
                        break;
                    case "ReleaseDate":
                        books = db.Books.Where(p => p.ReleaseDate.ToString().Contains(value)).ToList();
                        break;
                    case "IsEbook":
                        books = db.Books.Where(p => p.IsEbook.ToString().Contains(value)).ToList();
                        break;
                }
            }
            return books;
        }
        /// <summary>
        /// Get 1 Bool theo ID
        /// </summary>
        /// <param name="ID">Book's ID</param>
        /// <returns></returns>
        public Book Get1Book(string ID)
        {
            var book = db.Books.Find(ID);
            if (book == null)
                return new Book();
            else
                return book;
        }
        /// <summary>
        /// Get tất cả Author
        /// </summary>
        /// <returns></returns>
        public List<Author> GetAllAuthor()
        {
            return db.Authors.ToList();
        }
        /// <summary>
        /// Get tất cả thuộc tính của đối tượng Book
        /// </summary>
        /// <returns></returns>
        public List<String> GetBookProperty()
        {
            BookViewModel b = new BookViewModel();
            List<string> listProperty = new List<string>();
            foreach(var p in b.GetType().GetProperties())
            {
                listProperty.Add(p.Name);
            }
            return listProperty;
        }
        /// <summary>
        /// Xóa Books
        /// </summary>
        /// <param name="IDs">List các ID cần xóa</param>
        /// <returns></returns>
        public bool DeleteBooks(List<string> IDs)
        {
            foreach(string ID in IDs)
            {
                var book = db.Books.Find(ID);
                if (book == null)
                    return false;
                db.Books.Remove(book);
            }
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Add 1 Book to Database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Edit 1 Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool EditBook(Book book)
        {
            var b = db.Books.Find(book.ID);
            if (b == null)
                return false;
            else
            {
                b.Name = book.Name;
                b.ReleaseDate = book.ReleaseDate;
                b.IsEbook = book.IsEbook;
                b.Author_ID = book.Author_ID;
            }

            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Kiểm tra Book đã tồn tại hay chưa
        /// </summary>
        /// <param name="ID">Book's ID</param>
        /// <returns></returns>
        public bool IsExist(string ID)
        {
            var b = db.Books.Find(ID);
            if (b != null)
                return true;
            return false;
        }
    }
}
