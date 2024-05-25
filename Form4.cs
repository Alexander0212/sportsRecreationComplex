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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        DB db = new DB();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void getVisists(DateTime date)
        {
            DataTable table2 = new DataTable();
            MySqlCommand commandGetVisits = new MySqlCommand("SELECT v.description AS 'Опис', p.surname AS 'Прізвище', p.name AS `Ім'я`, v.datetime AS 'Дата' FROM `visits` v JOIN `Patients` p ON p.idpatient = v.patient_id WHERE v.doctor_id = @doctorid AND DATE(v.datetime) = @date", db.getConnection());
            commandGetVisits.Parameters.Add("@doctorid", MySqlDbType.VarChar).Value = Global.UserId;
            commandGetVisits.Parameters.Add("@date", MySqlDbType.Date).Value = date.ToString("yyyy-MM-dd");
            adapter.SelectCommand = commandGetVisits;
            adapter.Fill(table2);
            dataGridView2.DataSource = table2;
        }

        private void getSchedule(DateTime date)
        {
            DataTable table1 = new DataTable();
            MySqlCommand commandGetSchedules = new MySqlCommand("SELECT s.idschedule, s.datetime AS 'Дата', p.surname AS 'Прізвище', p.name AS `Ім'я`, s.patient_id FROM `schedule` s JOIN `Patients` p ON p.idpatient = s.patient_id WHERE s.doctor_id = @doctorid AND DATE(s.datetime) = @date AND s.status = 'scheduled'", db.getConnection());
            commandGetSchedules.Parameters.Add("@doctorid", MySqlDbType.VarChar).Value = Global.UserId;
            commandGetSchedules.Parameters.Add("@date", MySqlDbType.Date).Value = date.ToString("yyyy-MM-dd");
            adapter.SelectCommand = commandGetSchedules;
            adapter.Fill(table1);
            dataGridView1.DataSource = table1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                status.ForeColor = System.Drawing.Color.Red;
                status.Text = "Поле опису не повинно бути пустим";
            }
            db.getConnection().Open();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `visits` (`description`, `doctor_id`, `patient_id`, `datetime`, `schedule_id`) VALUES (@description, @doctor_id, @patient_id, @datetime, @schedule_id); UPDATE `schedule` SET `status` = 'closed' WHERE `idschedule` = @schedule_id", db.getConnection());
                command.Parameters.Add("@description", MySqlDbType.VarChar).Value = textBox1.Text;
                command.Parameters.Add("@doctor_id", MySqlDbType.VarChar).Value = Global.UserId;
                command.Parameters.Add("@patient_id", MySqlDbType.VarChar).Value = row.Cells["patient_id"].Value.ToString();
                command.Parameters.Add("@datetime", MySqlDbType.Date).Value = row.Cells["Дата"].Value;
                command.Parameters.Add("@schedule_id", MySqlDbType.VarChar).Value = row.Cells["idschedule"].Value.ToString();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    status.ForeColor = System.Drawing.Color.Green;
                    status.Text = "Збережено";
                }
            }
            db.getConnection().Close();

            getSchedule(dateTimePicker1.Value.Date);
            getVisists(DateTime.Today);

            textBox1.Text = "";
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = "Вітаємо, " + Global.UserName;
            getSchedule(DateTime.Today);
            getVisists(DateTime.Today);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getSchedule(dateTimePicker1.Value.Date);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            getVisists(dateTimePicker2.Value.Date);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
