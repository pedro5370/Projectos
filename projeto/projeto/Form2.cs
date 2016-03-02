using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stand___Base_De_Dados
{
    public partial class Form_Inicio : Form
    {
        public Form_Inicio()
        {
            InitializeComponent();
        }

        private void button_Sair_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Continuar_Click(object sender, EventArgs e)
        {
            Form_Stand _f1;
            _f1 = new Form_Stand();
            _f1.Show();
            Hide();
        }

        private void Form_Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}