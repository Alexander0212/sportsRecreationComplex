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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace sportsRecreationComplex
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        DB db = new DB();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private string action_type = "Створити";
        private string patient_action_type = "Створити";
        private string service_action_type = "Створити";

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void getDoctors()
        {
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT d.IdDoc, d.Surname AS 'Прізвище', d.Name AS `Ім'я`, d.Birthday AS 'Дата народження', d.Category AS 'Спеціалізація', d.Gender AS 'Стать', d.Number AS 'Телефон', d.password AS 'Пароль', s.name AS 'Назва послуги' FROM `Doctors` d JOIN `service` s ON d.serviceid = s.idservice", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void getPatients()
        {
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT idpatient AS id, Surname AS 'Прізвище', Name AS `Ім'я`, Birthday AS 'День народження', Gender AS 'Стать', Password AS 'Пароль' FROM `Patients`", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void getServices()
        {
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT idservice AS id, name AS 'Назва', cost AS 'Ціна', description AS 'Опис' FROM `Service`", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private void getSchedule()
        {
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT s.idschedule AS id, s.datetime AS 'Дата', CONCAT(p.surname, ' ', p.name) AS 'Пацієнт', CONCAT(d.Surname, ' ', d.Name) AS 'Лікар', ser.name AS 'Послуга' FROM `schedule` s JOIN `Patients` p ON p.idpatient = s.patient_id JOIN `Doctors` d ON d.IdDoc = s.doctor_id JOIN `service` ser ON ser.idservice = s.service_id WHERE s.status = 'scheduled'", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            getDoctors();
            getPatients();
            getServices();
            getSchedule();
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT * FROM `service`", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);

            service.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                service.Items.Add(row["idservice"].ToString() + "-" + row["name"].ToString());
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (action_type != "Створити")
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    surname.Text = row.Cells["Прізвище"].Value.ToString();
                    name.Text = row.Cells["Ім'я"].Value.ToString();
                    date.Text = row.Cells["Дата народження"].Value.ToString();
                    category.Text = row.Cells["Спеціалізація"].Value.ToString();
                    number.Text = row.Cells["Телефон"].Value.ToString();
                    gender.Text = row.Cells["Стать"].Value.ToString();
                    password.Text = row.Cells["Пароль"].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || surname.Text == "" || password.Text == "" || number.Text == "" || service.Text == "" || gender.Text == "")
            {
                status.ForeColor = System.Drawing.Color.Red;
                status.Text = "Поля не повинні бути пустими!";
                return;
            }

            string SQLcommand = "INSERT INTO `Doctors` (`Surname`, `Name`, `Birthday`, `Category`, `Gender`, `Number`, `serviceId`, `password`) VALUES (@surname, @name, @date, @category, @gender, @number, @serviceId, @password);";
            if (action_type == "Змінити")
            {
                SQLcommand = "UPDATE `Doctors` SET `Surname` = @surname, `Name` = @name, `Birthday` = @date, `Category` = @category, `Gender` = @gender, `Number` = @number, `serviceId` = @serviceId, `password` = @password WHERE `IdDoc` = @id";
            }

            MySqlCommand command = new MySqlCommand(SQLcommand, db.getConnection());
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name.Text;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = date.Value.Date;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender.Text;
            command.Parameters.Add("@number", MySqlDbType.VarChar).Value = number.Text;
            command.Parameters.Add("@category", MySqlDbType.VarChar).Value = category.Text;
            command.Parameters.Add("@serviceId", MySqlDbType.VarChar).Value = service.Text.Split('-')[0];
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password.Text;
            
            if (action_type == "Змінити")
            {
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.SelectedRows[0].Cells["IdDoc"].Value.ToString();
            }
            db.getConnection().Open();
            int rowsAffected = command.ExecuteNonQuery();
            db.getConnection().Close();

            if (rowsAffected > 0)
            {
                getDoctors();
                status.ForeColor = System.Drawing.Color.Green;
                status.Text = "Операція успішна";
            }
            else
            {
                status.ForeColor = System.Drawing.Color.Red;
                status.Text = "Виникла помилка!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `Doctors` WHERE IdDoc=@id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = row.Cells["IdDoc"].Value.ToString();
                command.ExecuteNonQuery();
            }
            db.getConnection().Close();

            getDoctors();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            action_type = comboBox1.Text;
            surname.Text = "";
            name.Text = "";
            category.Text = "";
            number.Text = "";
            gender.Text = "";
            password.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (action_type != "Створити")
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    surname.Text = row.Cells["Прізвище"].Value.ToString();
                    name.Text = row.Cells["Ім'я"].Value.ToString();
                    date.Text = row.Cells["Дата народження"].Value.ToString();
                    category.Text = row.Cells["Спеціалізація"].Value.ToString();
                    number.Text = row.Cells["Телефон"].Value.ToString();
                    gender.Text = row.Cells["Стать"].Value.ToString();
                    password.Text = row.Cells["Пароль"].Value.ToString();
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (patient_action_type != "Створити")
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    pSurname.Text = row.Cells["Прізвище"].Value.ToString();
                    pName.Text = row.Cells["Ім'я"].Value.ToString();
                    pDate.Text = row.Cells["День народження"].Value.ToString();
                    pGender.Text = row.Cells["Стать"].Value.ToString();
                    pPassword.Text = row.Cells["Пароль"].Value.ToString();
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (service_action_type != "Створити")
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    sName.Text = row.Cells["Назва"].Value.ToString();
                    sDescription.Text = row.Cells["Опис"].Value.ToString();
                    sCost.Text = row.Cells["Ціна"].Value.ToString();
                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `Patients` WHERE idpatient=@id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = row.Cells["id"].Value.ToString();
                command.ExecuteNonQuery();
            }
            db.getConnection().Close();

            getPatients();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            patient_action_type = comboBox2.Text;
            pSurname.Text = "";
            pName.Text = "";
            pGender.Text = "";
            pPassword.Text = "";
        }

        private void pSubmit_Click(object sender, EventArgs e)
        {
            if (pName.Text == "" || pSurname.Text == "" || pPassword.Text == "" || pGender.Text == "")
            {
                statusPatient.ForeColor = System.Drawing.Color.Red;
                statusPatient.Text = "Поля не повинні бути пустими!";
                return;
            }

            string SQLcommand = "INSERT INTO `Patients` (`idpatient`, `Surname`, `Name`, `Birthday`, `Gender`, `Password`) VALUES (@id, @surname, @name, @date, @gender, @password);";
            if (patient_action_type == "Змінити")
            {
                SQLcommand = "UPDATE `Patients` SET `Surname` = @surname, `Name` = @name, `Birthday` = @date, `Gender` = @gender, `password` = @password WHERE `idpatient` = @id";
            }

            MySqlCommand command = new MySqlCommand(SQLcommand, db.getConnection());
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = pSurname.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = pName.Text;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = pDate.Value.Date;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = pGender.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = pPassword.Text;
            if (patient_action_type == "Змінити")
            {
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView2.SelectedRows[0].Cells["id"].Value.ToString();
            } else
            {
                Guid myuuid = Guid.NewGuid();
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = myuuid.ToString();
            }
            db.getConnection().Open();
            int rowsAffected = command.ExecuteNonQuery();
            db.getConnection().Close();

            if (rowsAffected > 0)
            {
                getPatients();
                statusPatient.ForeColor = System.Drawing.Color.Green;
                statusPatient.Text = "Операція успішна";
            }
            else
            {
                statusPatient.ForeColor = System.Drawing.Color.Red;
                statusPatient.Text = "Виникла помилка!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `service` WHERE idservice=@id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = row.Cells["id"].Value.ToString();
                command.ExecuteNonQuery();
            }
            db.getConnection().Close();

           getServices();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sName.Text == "" || sDescription.Text == "" || sCost.Text == "")
            {
                serviceStatus.ForeColor = System.Drawing.Color.Red;
                serviceStatus.Text = "Поля не повинні бути пустими!";
                return;
            }

            string SQLcommand = "INSERT INTO `service` (`name`, `cost`, `description`) VALUES (@name, @cost, @description);";
            if (service_action_type == "Змінити")
            {
                SQLcommand = "UPDATE `service` SET `name` = @name, `cost` = @cost, `description` = @description WHERE `idservice` = @id";
            }

            MySqlCommand command = new MySqlCommand(SQLcommand, db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = sName.Text;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = sDescription.Text;
            command.Parameters.Add("@cost", MySqlDbType.Decimal).Value = Convert.ToDecimal(sCost.Text);
            if (service_action_type == "Змінити")
            {
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView3.SelectedRows[0].Cells["id"].Value.ToString();
            }
   
            db.getConnection().Open();
            int rowsAffected = command.ExecuteNonQuery();
            db.getConnection().Close();

            if (rowsAffected > 0)
            {
                getServices();
                serviceStatus.ForeColor = System.Drawing.Color.Green;
                serviceStatus.Text = "Операція успішна";
            }
            else
            {
                serviceStatus.ForeColor = System.Drawing.Color.Red;
                serviceStatus.Text = "Виникла помилка";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            service_action_type = comboBox3.Text;
            sName.Text = "";
            sDescription.Text = "";
            sCost.Text = "";
        }

        private void data(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView4.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `schedule` WHERE idschedule=@id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = row.Cells["id"].Value.ToString();
                command.ExecuteNonQuery();
            }
            db.getConnection().Close();

            getSchedule();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
