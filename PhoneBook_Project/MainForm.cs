using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhoneBook_Project {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            btnAddUser.Click += new EventHandler(btnAddUser_Click);
        }

        void btnAddUser_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtNewFName.Text) ||
                string.IsNullOrWhiteSpace(txtNewLName.Text) ||
                string.IsNullOrWhiteSpace(txtNewNumber.Text)) {
                    return;
            }
            DBOperator.AddUserToBase(txtNewFName.Text, txtNewLName.Text, txtNewNumber.Text);
            phoneGridView.DataSource = null;
            phoneGridView.DataSource = DBOperator.GetAllUsersFromBase();
        }

        void btnSearch_Click(object sender, EventArgs e) {
            SearchUsers();
        }

        void MainForm_Load(object sender, EventArgs e) {
            phoneGridView.DataSource = DBOperator.GetAllUsersFromBase();
        }
        void SearchUsers() {
            if (string.IsNullOrWhiteSpace(txtFName.Text) || string.IsNullOrWhiteSpace(txtLName.Text)) {
                return;
            }
            var selectedTable = ((DataTable)phoneGridView.DataSource).AsEnumerable()
                .Where(row => row.Field<string>("Ім'я") == txtFName.Text && 
                       row.Field<string>("Прізвище") == txtLName.Text);
            if (selectedTable.Count() == 0) {
                MessageBox.Show("Записів не знайденно", "Результат");
            }
            else {
                searchGridView.DataSource = null;
                searchGridView.DataSource = selectedTable.CopyToDataTable();
            }
        }
    }
}
