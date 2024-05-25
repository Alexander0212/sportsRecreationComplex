namespace sportsRecreationComplex
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.surname = new System.Windows.Forms.TextBox();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.password = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.confirmPass = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gender = new System.Windows.Forms.ComboBox();
            this.register = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Реєстрація пацієнта";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(128, 116);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(200, 22);
            this.name.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ім\'я";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Прізвище";
            // 
            // surname
            // 
            this.surname.Location = new System.Drawing.Point(128, 168);
            this.surname.Name = "surname";
            this.surname.Size = new System.Drawing.Size(200, 22);
            this.surname.TabIndex = 3;
            // 
            // birthday
            // 
            this.birthday.Location = new System.Drawing.Point(128, 219);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(200, 22);
            this.birthday.TabIndex = 5;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(128, 327);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(200, 22);
            this.password.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Пароль";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Підтвердження паролю";
            // 
            // confirmPass
            // 
            this.confirmPass.Location = new System.Drawing.Point(128, 371);
            this.confirmPass.Name = "confirmPass";
            this.confirmPass.Size = new System.Drawing.Size(200, 22);
            this.confirmPass.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Стать";
            // 
            // gender
            // 
            this.gender.FormattingEnabled = true;
            this.gender.Items.AddRange(new object[] {
            "Чоловік",
            "Жінка"});
            this.gender.Location = new System.Drawing.Point(128, 272);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(200, 24);
            this.gender.TabIndex = 11;
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(128, 399);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(200, 33);
            this.register.TabIndex = 12;
            this.register.Text = "Зареєструватися";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(128, 438);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(200, 45);
            this.Login.TabIndex = 13;
            this.Login.Text = "Повернутися до авторизації";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.ForeColor = System.Drawing.Color.Red;
            this.status.Location = new System.Drawing.Point(125, 72);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 16);
            this.status.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Дата народження";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 523);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.status);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.register);
            this.Controls.Add(this.gender);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.confirmPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.password);
            this.Controls.Add(this.birthday);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.surname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox surname;
        private System.Windows.Forms.DateTimePicker birthday;
        private System.Windows.Forms.MaskedTextBox password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox confirmPass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox gender;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label7;
    }
}