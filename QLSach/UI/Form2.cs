using QLSach.DTO;
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

namespace QLSach.UI
{
    public partial class Form2 : Form
    {
        //Đa luồng
        Thread thread;
        Book Book;
        //Delegate
        public delegate void GetID(string ID);
        public GetID getID;
        string ID;
        private void get(string ID)
        {
            this.ID = ID;
        }
        public Form2()
        {
            getID = new GetID(get);
            InitializeComponent();
            LoadCBBAuthor();
            rbtn_Yes.Checked = true;
        }
        void LoadCBBAuthor()
        {
            var listAuthor = BLL.BLL.Instance.GetAllAuthor();
            foreach (var i in listAuthor)
            {
                cbb_Author.Items.Add(new CBBItem
                {
                    Name = i.Author_Name,
                    Value = i.Author_ID
                });
            }
            cbb_Author.SelectedIndex = 0;
        }
        void LoadBookInformation()
        {
            tb_ID.Enabled = false;
            Book book = BLL.BLL.Instance.Get1Book(ID);
            tb_ID.Text = book.ID;
            tb_Name.Text = book.Name;
            cbb_Author.SelectedIndex = book.Author_ID;
            dateTimePicker1.Value = book.ReleaseDate;
            if (book.IsEbook)
                rbtn_Yes.Checked = true;
            else
                rbtn_No.Checked = true;
        }
        Book GetBook()
        {
            Book book = new Book();
            book.ID = tb_ID.Text;
            book.Name = tb_Name.Text;
            book.ReleaseDate = dateTimePicker1.Value;
            if (rbtn_Yes.Checked)
                book.IsEbook = true;
            else book.IsEbook = false;
            book.Author_ID = ((CBBItem)cbb_Author.SelectedItem).Value;

            return book;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (ID == "")
                return;
            LoadBookInformation();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;
            if (tb_ID.Enabled == true)
            {
                if (BLL.BLL.Instance.IsExist(tb_ID.Text))
                {
                    MessageBox.Show("ID này đã tồn tại!", "Chú ý");
                    return;
                }    
            }    
            GoToForm1();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            thread = new Thread(OutForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        bool ValidateData()
        {
            if (tb_ID.Text == "")
            {
                MessageBox.Show("Nhập ID!");
                return false;
            }
            if (tb_Name.Text == "")
            {
                MessageBox.Show("Nhập Tên Sách!");
                return false;
            }
            return true;             
        }
        void RunForm1()
        {
            Form1 f1 = new Form1();
            f1.Getbook(Book);
            Application.Run(f1);
        }
        void OutForm()
        {
            Application.Run(new Form1());
        }
        void GoToForm1()
        {
            Book = GetBook();
            this.Dispose();
            thread = new Thread(RunForm1);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
