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
        string sqlQueryString;
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
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = stand.accdb";
            try
            {
                database = new OleDbConnection(connectionString);
                database.Open();
                string queryString;
                if (combobox.Text == "Sem filtro")
                    queryString = "SELECT * FROM Veiculos;";


                else
                    queryString = "SELECT * FROM Veiculos WHERE tipo_veiculo = '" + combobox.Text + "';";


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
            string queryString;

            //os dois vazios
            if (string.IsNullOrWhiteSpace(textBox_min.Text) == true && string.IsNullOrWhiteSpace(textBox_max.Text) == true)
            {
                queryString = "SELECT * FROM Veiculos ;";      
            }
            //min preenchido e max vazio
            if (string.IsNullOrWhiteSpace(textBox_min.Text) == false && string.IsNullOrWhiteSpace(textBox_max.Text) == true)
            {
                queryString = "SELECT * FROM Veiculos WHERE preco >= " + int.Parse(textBox_min.Text) +";";
                loadDataGrid(queryString);
            }


            //os dois preenchidos
            if (string.IsNullOrWhiteSpace(textBox_min.Text) == false && string.IsNullOrWhiteSpace(textBox_max.Text) == false)
            {
                queryString = "SELECT * FROM Veiculos WHERE preco >=" + int.Parse(textBox_min.Text) + " AND preco <= " + int.Parse(textBox_max.Text) +";";
                loadDataGrid(queryString);
            }
            //max preenchido e min vazio
            if (string.IsNullOrWhiteSpace(textBox_min.Text) == true && string.IsNullOrWhiteSpace(textBox_max.Text) == false)
            {
                queryString = "SELECT * FROM Veiculos WHERE preco <= " + int.Parse(textBox_max.Text) + ";";
                loadDataGrid(queryString);
            }






        }
        private void Form1_Load(object sender, EventArgs e)
        {


            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = stand.accdb";
            try
            {
                database = new OleDbConnection(connectionString);
                database.Open();
                string queryString = "SELECT DISTINCT tipo_veiculo FROM Veiculos;";

                OleDbConnection connection = new OleDbConnection(connectionString);
                // Create data adapter object 
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(queryString, connection);
                // Create a dataset object and fill with data using data adapter's Fill method 
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Veiculos");

                // Attach dataset's DefaultView to the combobox 
                combobox.DataSource = dataSet.Tables["Veiculos"].DefaultView;
                combobox.DisplayMember = "tipo_veiculo";
                // Attach dataset's DefaultView to the combobox 
                // combobox.DataSource = dataSet.Tables["customers"].DefaultView;
                // combobox.DisplayMember = "tipo_veiculo";




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void checkBox_ativa_filtro_CheckedChanged(object sender, EventArgs e)
        {
            combobox.Enabled = true;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
