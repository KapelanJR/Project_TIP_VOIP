﻿
namespace ClientWindows
{
    partial class LoginForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.confirmAction_button = new System.Windows.Forms.Button();
            this.login_label = new System.Windows.Forms.Label();
            this.login_textbox = new System.Windows.Forms.TextBox();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.pass_label = new System.Windows.Forms.Label();
            this.changeMode_button = new System.Windows.Forms.Button();
            this.changeMode_label = new System.Windows.Forms.Label();
            this.actualMode_Label = new System.Windows.Forms.Label();
            this.usernameFree_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirmAction_button
            // 
            this.confirmAction_button.Location = new System.Drawing.Point(12, 125);
            this.confirmAction_button.Name = "confirmAction_button";
            this.confirmAction_button.Size = new System.Drawing.Size(200, 25);
            this.confirmAction_button.TabIndex = 0;
            this.confirmAction_button.Text = "Zaloguj się";
            this.confirmAction_button.UseVisualStyleBackColor = true;
            this.confirmAction_button.Click += new System.EventHandler(this.confirmAction_button_Click);
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Location = new System.Drawing.Point(13, 44);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(33, 13);
            this.login_label.TabIndex = 1;
            this.login_label.Text = "Login";
            // 
            // login_textbox
            // 
            this.login_textbox.Location = new System.Drawing.Point(12, 60);
            this.login_textbox.Name = "login_textbox";
            this.login_textbox.Size = new System.Drawing.Size(200, 20);
            this.login_textbox.TabIndex = 2;
            this.login_textbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.login_textbox_KeyUp);
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(12, 99);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(200, 20);
            this.password_textbox.TabIndex = 3;
            // 
            // pass_label
            // 
            this.pass_label.AutoSize = true;
            this.pass_label.Location = new System.Drawing.Point(13, 83);
            this.pass_label.Name = "pass_label";
            this.pass_label.Size = new System.Drawing.Size(36, 13);
            this.pass_label.TabIndex = 4;
            this.pass_label.Text = "Hasło";
            // 
            // changeMode_button
            // 
            this.changeMode_button.Location = new System.Drawing.Point(12, 169);
            this.changeMode_button.Name = "changeMode_button";
            this.changeMode_button.Size = new System.Drawing.Size(200, 25);
            this.changeMode_button.TabIndex = 5;
            this.changeMode_button.Text = "Zarejestruj się";
            this.changeMode_button.UseVisualStyleBackColor = true;
            this.changeMode_button.Click += new System.EventHandler(this.changeMode_button_Click);
            // 
            // changeMode_label
            // 
            this.changeMode_label.AutoSize = true;
            this.changeMode_label.Location = new System.Drawing.Point(12, 153);
            this.changeMode_label.Name = "changeMode_label";
            this.changeMode_label.Size = new System.Drawing.Size(86, 13);
            this.changeMode_label.TabIndex = 6;
            this.changeMode_label.Text = "Nie masz konta?\r";
            // 
            // actualMode_Label
            // 
            this.actualMode_Label.AutoSize = true;
            this.actualMode_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actualMode_Label.Location = new System.Drawing.Point(12, 9);
            this.actualMode_Label.Name = "actualMode_Label";
            this.actualMode_Label.Size = new System.Drawing.Size(103, 24);
            this.actualMode_Label.TabIndex = 7;
            this.actualMode_Label.Text = "Logowanie";
            // 
            // usernameFree_label
            // 
            this.usernameFree_label.AutoSize = true;
            this.usernameFree_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.usernameFree_label.ForeColor = System.Drawing.Color.Red;
            this.usernameFree_label.Location = new System.Drawing.Point(53, 44);
            this.usernameFree_label.Name = "usernameFree_label";
            this.usernameFree_label.Size = new System.Drawing.Size(0, 13);
            this.usernameFree_label.TabIndex = 8;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 209);
            this.Controls.Add(this.usernameFree_label);
            this.Controls.Add(this.actualMode_Label);
            this.Controls.Add(this.changeMode_label);
            this.Controls.Add(this.changeMode_button);
            this.Controls.Add(this.pass_label);
            this.Controls.Add(this.password_textbox);
            this.Controls.Add(this.login_textbox);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.confirmAction_button);
            this.Name = "LoginForm";
            this.Text = "TIP_VOIP Client";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmAction_button;
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.TextBox login_textbox;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.Label pass_label;
        private System.Windows.Forms.Button changeMode_button;
        private System.Windows.Forms.Label changeMode_label;
        private System.Windows.Forms.Label actualMode_Label;
        private System.Windows.Forms.Label usernameFree_label;
    }
}
