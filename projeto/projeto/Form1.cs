using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Console = System.Console;
using System.Data.Odbc;
using System.Data.OleDb;


namespace projeto
{

    public partial class Form1 : Form


    {

        OleDbConnection database;
        public Form1()
        {
            InitializeComponent();
        }

        public void loadDataGrid(string sqlQueryString)
        {

            OleDbCommand SQLQuery = new OleDbCommand();
            DataTable data = null;
            dataGridView1.DataSource = null;
            SQLQuery.Connection = null;
            OleDbDataAdapter dataAdapter = null;
            dataGridView1.Columns.Clear(); // <-- clear columns

            SQLQuery.CommandText = sqlQueryString;
            SQLQuery.Connection = database;
            data = new DataTable();
            dataAdapter = new OleDbDataAdapter(SQLQuery);
            dataAdapter.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AllowUserToAddRows = false; // <-- remove the null line
            dataGridView1.ReadOnly = true;          // <-- so the user cannot type 

            // following code defines column sizes
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 75;
           

        }

        public void db_connect()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = stand1.accdb";
    try
            {
                 database = new OleDbConnection(connectionString);
                database.Open();
                string queryString = "SELECT * FROM Veiculos;";
                
                loadDataGrid(queryString);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            db_connect();
            
        }
    }
}
