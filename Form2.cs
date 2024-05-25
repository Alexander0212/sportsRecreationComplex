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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void register_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            if (name.Text == "" || surname.Text == "" || password.Text == "")
            {
                status.Text = "Поля не повинні бути порожніми!";
                return;
            }

            if(password.Text != confirmPass.Text)
            {
                status.Text = "Паролі не співпадають!";
                return;
            }


            MySqlCommand command = new MySqlCommand("INSERT INTO `Patients` (`idpatient`, `Surname`, `Name`, `Birthday`, `Gender`, `Password`) VALUES (@id, @surname, @name, @date, @gender, @password);", db.getConnection());
            Guid myuuid = Guid.NewGuid();
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = myuuid.ToString();
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name.Text;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = birthday.Value.Date;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password.Text;
            db.getConnection().Open();
            int rowsAffected = command.ExecuteNonQuery();
            db.getConnection().Close();

            if (rowsAffected > 0)
            {
                Global.UserId = myuuid.ToString();
                Global.UserName = name.Text;
                this.Hide();
                Form3 form = new Form3();
                form.Show();
                return;
            }
            else
            {
                status.Text = "Помилка реєстрації, спробуйте ще раз";
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            gender.SelectedIndex = 0;
        }
    }
}
