using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sportsRecreationComplex
{
    public partial class Form3 : Form
    {
        private string doctorId;
        private string serviceId;
        public Form3()
        {
            InitializeComponent();
        }

        DB db = new DB();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void getSchedule()
        {
            DataTable table3 = new DataTable();
            MySqlCommand commandGetSchedules = new MySqlCommand("SELECT s.idschedule AS 'id', s.datetime AS 'Дата', p.name AS 'Назва процедури', CONCAT(d.surname, ' ', d.name) AS 'Лікар' FROM `schedule` s JOIN `Doctors` d ON d.idDoc = s.doctor_id JOIN `service` p ON p.idservice = s.service_id WHERE s.patient_id = @patientid AND s.status = 'scheduled'", db.getConnection());
            commandGetSchedules.Parameters.Add("@patientid", MySqlDbType.VarChar).Value = Global.UserId;
            adapter.SelectCommand = commandGetSchedules;
            adapter.Fill(table3);
            dataGridView3.DataSource = table3;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = "Вітаємо, " + Global.UserName;
            DataTable table = new DataTable();
            MySqlCommand commandGetServices = new MySqlCommand("SELECT name AS 'Назва', cost AS 'Ціна', description AS 'Опис' FROM `service`", db.getConnection());
            adapter.SelectCommand = commandGetServices;
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            DataTable table2 = new DataTable();
            MySqlCommand commandGetVisits = new MySqlCommand("SELECT v.description AS 'Опис', CONCAT(d.surname, ' ', d.name) AS 'Лікар', v.datetime AS 'Дата' FROM `visits` v JOIN `Doctors` d ON d.idDoc = v.doctor_id WHERE v.patient_id = @patientid", db.getConnection());
            commandGetVisits.Parameters.Add("@patientid", MySqlDbType.VarChar).Value = Global.UserId;
            adapter.SelectCommand = commandGetVisits;
            adapter.Fill(table2);
            dataGridView2.DataSource = table2;

            getSchedule();

            services.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                services.Items.Add(row["Назва"].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            MySqlCommand commandGetservices = new MySqlCommand("SELECT * FROM `service` WHERE `name` = @name", db.getConnection());
            commandGetservices.Parameters.Add("@name", MySqlDbType.VarChar).Value = services.Text;
            adapter.SelectCommand = commandGetservices;
            adapter.Fill(table);
            MySqlCommand commandGetDoctor = new MySqlCommand("SELECT * FROM `Doctors` WHERE `serviceId` = @id", db.getConnection());
            serviceId = table.Rows[0]["idservice"].ToString();
            commandGetDoctor.Parameters.Add("@id", MySqlDbType.VarChar).Value = serviceId;
            
            db.getConnection().Open();

            MySqlDataReader reader = commandGetDoctor.ExecuteReader();

            while (reader.Read())
            {
                doctor_name.Text = reader["Name"].ToString();
                doctorId = reader["IdDoc"].ToString();
            }

            reader.Close();

            db.getConnection().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serviceId == "")
            {
                status.ForeColor = System.Drawing.Color.Red;
                status.Text = "Оберіть процедуру!";
            }

            MySqlCommand command = new MySqlCommand("INSERT INTO `schedule` (`datetime`, `patient_id`, `doctor_id`, `service_id`) VALUES (@date, @patient_id, @doctor_id, @service_id);", db.getConnection());
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date.Value.Date;
            command.Parameters.Add("@patient_id", MySqlDbType.VarChar).Value = Global.UserId;
            command.Parameters.Add("@doctor_id", MySqlDbType.VarChar).Value = doctorId;
            command.Parameters.Add("@service_id", MySqlDbType.VarChar).Value = serviceId;

            db.getConnection().Open();
            int rowsAffected = command.ExecuteNonQuery();
            db.getConnection().Close();

            if (rowsAffected > 0)
            {
                getSchedule();
                status.ForeColor = System.Drawing.Color.Green;
                status.Text = "Вас записано";
                return;
            }
            else
            {
                status.ForeColor = System.Drawing.Color.Red;
                status.Text = "Винила помилка! Спробуйте ще раз";
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `schedule` WHERE idschedule=@id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = row.Cells["id"].Value.ToString();
                command.ExecuteNonQuery();
            }
            db.getConnection().Close();

            getSchedule();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
