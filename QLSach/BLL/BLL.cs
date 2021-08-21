using QLSach.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSach.BLL
{
    class BLL
    {
        public delegate bool Compare(BookViewModel b1, BookViewModel b2);
        private static BLL _Instance;
        public static BLL Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL();
                return _Instance;
            }
            private set { }
        }
        public List<BookViewModel> GetBooks(int AuthorID, string property_Name, string property_Value)
        {
            List<Book> books = DAL.DAL.Instance.GetBooks(AuthorID, property_Name, property_Value);
            List<BookViewModel> bookViews = new List<BookViewModel>();
            foreach(var b in books)
            {
                bookViews.Add(new BookViewModel
                {
                    ID = b.ID,
                    Name = b.Name,
                    ReleaseDate = b.ReleaseDate,
                    IsEbook = b.IsEbook,
                    Author_Name = b.Author.Author_Name
                });
            }
            return bookViews;
        }
        public List<Author> GetAllAuthor()
        {
            return DAL.DAL.Instance.GetAllAuthor();
        }
        public List<String> GetBookProperty()
        {
            return DAL.DAL.Instance.GetBookProperty();
        }
        public List<BookViewModel> SortBookBy(List<BookViewModel> books, string property)
        {
            List<BookViewModel> sortedBooks = books;
            Compare cmp = BookViewModel.CmpID;
            switch (property)
            {
                case "ID":
                    cmp = BookViewModel.CmpID;
                    break;
                case "Name":
                    cmp = BookViewModel.CmpName;
                    break;
                case "ReleaseDate":
                    cmp = BookViewModel.CmpReleaseDate;
                    break;
                case "IsEbook":
                    cmp = BookViewModel.CmpIsEBook;
                    break;
                case "Author_Name":
                    cmp = BookViewModel.CmpAuthorName;
                    break;
                default:
                    cmp = BookViewModel.CmpID;
                    break;
            }
            //Selection Sort
            for (int i = 0; i < sortedBooks.Count; i++)
            {
                for (int j = i + 1; j < sortedBooks.Count; j++)
                {
                    if(cmp(sortedBooks[i], sortedBooks[j]))
                    {
                        var book = sortedBooks[i];
                        sortedBooks[i] = sortedBooks[j];
                        sortedBooks[j] = book;
                    }    
                }    
            }    

            return sortedBooks;

        }
        public bool DeleteBooks(List<string> IDs)
        {
            return DAL.DAL.Instance.DeleteBooks(IDs);
        }
        public bool AddBook(Book book)
        {
            return DAL.DAL.Instance.AddBook(book);
        }
        public bool EditBook(Book book)
        {
            return DAL.DAL.Instance.EditBook(book);
        }
        public Book Get1Book(string ID)
        {
            return DAL.DAL.Instance.Get1Book(ID);
        }
        public bool IsExist(string ID)
        {
            return DAL.DAL.Instance.IsExist(ID);
        }
    }
}
