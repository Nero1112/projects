using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace PhoneBook_Project {
    public static class DBOperator {
        private const string CONNECT_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Base.mdb";
        public static DataTable GetAllUsersFromBase() {
            DataSet temp = new DataSet();
            string selectCMD = "SELECT * FROM [Users]";
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCMD, CONNECT_STRING)) {
                DataTableMapping map = adapter.TableMappings.Add("Users", "Користувачі");
                map.ColumnMappings.Add("FName", "Ім'я");
                map.ColumnMappings.Add("LName", "Прізвище");
                map.ColumnMappings.Add("Number", "Номер");
                adapter.Fill(temp, "Users");
            }
            return temp.Tables[0];
        }
        public static void AddUserToBase(string fname, string lname, string number) {
            string addCMD = string.Format("INSERT INTO [Users] VALUES('{0}','{1}','{2}')", fname, lname, number);
            using (OleDbConnection cnn = new OleDbConnection(CONNECT_STRING)) {
                cnn.Open();
                using (OleDbCommand cmd = new OleDbCommand(addCMD, cnn)) {
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}
