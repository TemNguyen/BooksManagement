using QLSach.DTO;
using QLSach.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSach
{
    public partial class Form1 : Form
    {
        //Đa luồng
        Thread thread;
        Book Book;
        //Delegate
        public delegate void GetBook(Book book);
        public GetBook Getbook;
        string ID;

        private void getBook(Book book)
        {
            this.Book = book;
        }
        public Form1()
        {
            Getbook = new GetBook(getBook);
            InitializeComponent();
            LoadCBBSearch_Sort();
            LoadCBBAuthor();
            LoadBooks();
        }
        void LoadBooks()
        {
            int Author_ID = ((CBBItem)cbb_Author.SelectedItem).Value;
            string propertyName = ((CBBItem)CBBSearch.SelectedItem).Name;
            string propertyValue = tbSearch.Text;
            dataGridView1.DataSource = BLL.BLL.Instance.GetBooks(Author_ID, propertyName, propertyValue);
        }
        void LoadCBBAuthor()
        {
            var listAuthor = BLL.BLL.Instance.GetAllAuthor();
            cbb_Author.Items.Add(new CBBItem
            {
                Name = "ALL",
                Value = 0
            });
            foreach(var i in listAuthor)
            {
                cbb_Author.Items.Add(new CBBItem
                {
                    Name = i.Author_Name,
                    Value = i.Author_ID
                });
            }
            cbb_Author.SelectedIndex = 0;
        }
        void LoadCBBSearch_Sort()
        {
            var BookProp = BLL.BLL.Instance.GetBookProperty();

            foreach(var i in BookProp)
            {
                CBBSearch.Items.Add(new CBBItem
                {
                    Name = i
                });

                cbb_Sort.Items.Add(new CBBItem
                {
                    Name = i
                });
            }
            CBBSearch.Items.RemoveAt(CBBSearch.Items.Count - 1);
            CBBSearch.SelectedIndex = 0;
            cbb_Sort.SelectedIndex = 0;
        }
        private void cbb_Author_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            cbb_Author.SelectedIndex = 0;
            CBBSearch.SelectedIndex = 0;
            cbb_Sort.SelectedIndex = 0;
            tbSearch.Text = "";
            LoadBooks();
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            List<BookViewModel> bookViews = new List<BookViewModel>();
            string property = ((CBBItem)cbb_Sort.SelectedItem).Name;
            foreach(DataGridViewRow dr in dataGridView1.Rows)
            {
                bookViews.Add(new BookViewModel
                {
                    ID = dr.Cells["ID"].Value.ToString(),
                    Name = dr.Cells["Name"].Value.ToString(),
                    ReleaseDate = Convert.ToDateTime(dr.Cells["ReleaseDate"].Value),
                    IsEbook = Convert.ToBoolean(dr.Cells["IsEbook"].Value),
                    Author_Name = dr.Cells["Author_Name"].Value.ToString()
                });
            };

            dataGridView1.DataSource = BLL.BLL.Instance.SortBookBy(bookViews, property);
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            List<string> IDs = new List<string>();
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Không có hàng nào được chọn!", "Chú ý");
            else
            {
                foreach(DataGridViewRow dr in dataGridView1.SelectedRows)
                {
                    IDs.Add(dr.Cells["ID"].Value.ToString());
                }    
            }
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn xóa (những) bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch(d)
            {
                case DialogResult.Yes:
                    if (BLL.BLL.Instance.DeleteBooks(IDs))
                        MessageBox.Show("Xóa thành công!!");
                    else
                        MessageBox.Show("Có lỗi xảy ra trong quá trình xóa!");
                    break;
                case DialogResult.No:
                    break;

            }
            LoadBooks();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            GoToForm2("");
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có hàng nào được chọn!", "Chú ý");
                return;
            }        
            string ID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            GoToForm2(ID);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Book == null)
                return;
            if (BLL.BLL.Instance.IsExist(Book.ID))
                BLL.BLL.Instance.EditBook(Book);
            else
                BLL.BLL.Instance.AddBook(Book);

            LoadBooks();
        }
        void RunForm2()
        {
            Form2 f2 = new Form2();
            f2.getID(this.ID);
            Application.Run(f2);
        }
        void GoToForm2(string ID)
        {
            this.ID = ID;
            this.Dispose();
            thread = new Thread(RunForm2);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
