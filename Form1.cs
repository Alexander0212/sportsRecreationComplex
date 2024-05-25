using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sportsRecreationComplex
{
    public partial class Form1 : Form
    {

        private String adminLogin = "Admin";
        private String adminPassword = "root";
        private String error = "Пароль або логін - хибні";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            role.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            if (role.Text == "Адміністратор")
            {
                if(login.Text == adminLogin && password.Text == adminPassword)
                {
                    this.Hide();
                    Form5 form5 = new Form5();
                    form5.Show();
                    return;
                } else
                {
                    response.Text = error;
                    return;
                }
            } else if(role.Text == "Пацієнт")
            {
                MySqlCommand commandGetPatient = new MySqlCommand("SELECT * FROM `Patients` WHERE `Surname` = @uS AND `Password` = @uP", db.getConnection());
                commandGetPatient.Parameters.Add("@uS", MySqlDbType.VarChar).Value = login.Text;
                commandGetPatient.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password.Text;
                adapter.SelectCommand = commandGetPatient;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    Global.UserId = table.Rows[0]["idpatient"].ToString();
                    Global.UserName = table.Rows[0]["Name"].ToString();
                    this.Hide();
                    Form3 form3 = new Form3();
                    form3.Show();
                    return;
                }
                else
                {
                    response.Text = error;
                }
            } else
            {
                MySqlCommand commandGetDoctor = new MySqlCommand("SELECT * FROM `Doctors` WHERE `Surname` = @uS AND `password` = @uP", db.getConnection());
                commandGetDoctor.Parameters.Add("@uS", MySqlDbType.VarChar).Value = login.Text;
                commandGetDoctor.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password.Text;
                adapter.SelectCommand = commandGetDoctor;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    Global.UserId = table.Rows[0]["idDoc"].ToString();
                    Global.UserName = table.Rows[0]["Name"].ToString();
                    this.Hide();
                    Form4 form4 = new Form4();
                    form4.Show();
                    return;
                }
                else
                {
                    response.Text = error;
                }
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
