using System.Windows.Forms;

namespace Predrag_Stojkovic_2021_0414
{
  partial class LoginForm
  {
    private System.ComponentModel.IContainer components = null;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Label lblUsername;
    private Label lblPassword;
    private Button btnLogin;
    private Button btnGuest;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.txtUsername = new System.Windows.Forms.TextBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblUsername = new System.Windows.Forms.Label();
      this.lblPassword = new System.Windows.Forms.Label();
      this.btnLogin = new System.Windows.Forms.Button();
      this.btnGuest = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtUsername
      // 
      this.txtUsername.Location = new System.Drawing.Point(12, 52);
      this.txtUsername.Name = "txtUsername";
      this.txtUsername.Size = new System.Drawing.Size(125, 27);
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(12, 120);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size(125, 27);
      this.txtPassword.PasswordChar = '*';
      // 
      // lblUsername
      // 
      this.lblUsername.AutoSize = true;
      this.lblUsername.Location = new System.Drawing.Point(12, 29);
      this.lblUsername.Name = "lblUsername";
      this.lblUsername.Size = new System.Drawing.Size(75, 20);
      this.lblUsername.Text = "Username";
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(12, 97);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(70, 20);
      this.lblPassword.Text = "Password";
      // 
      // btnLogin
      // 
      this.btnLogin.Location = new System.Drawing.Point(12, 179);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new System.Drawing.Size(125, 41);
      this.btnLogin.Text = "Login";
      this.btnLogin.UseVisualStyleBackColor = true;
      // 
      // btnGuest
      // 
      this.btnGuest.Location = new System.Drawing.Point(12, 226);
      this.btnGuest.Name = "btnGuest";
      this.btnGuest.Size = new System.Drawing.Size(125, 41);
      this.btnGuest.Text = "Guest";
      this.btnGuest.UseVisualStyleBackColor = true;
      // 
      // LoginForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(417, 276);
      this.Controls.Add(this.btnLogin);
      this.Controls.Add(this.btnGuest);
      this.Controls.Add(this.lblPassword);
      this.Controls.Add(this.lblUsername);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.txtUsername);
      this.Name = "LoginForm";
      this.Text = "Login";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
