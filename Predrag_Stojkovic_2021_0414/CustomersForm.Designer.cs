using System.Windows.Forms;

namespace Predrag_Stojkovic_2021_0414
{
  partial class CustomersForm
  {
    private System.ComponentModel.IContainer components = null;
    private ListBox lstCustomers;
    private ListBox lstBorrowedCopies;
    private Label lblCustomers;
    private Label lblBorrowedCopies;

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
      this.lstCustomers = new System.Windows.Forms.ListBox();
      this.lstBorrowedCopies = new System.Windows.Forms.ListBox();
      this.lblCustomers = new System.Windows.Forms.Label();
      this.lblBorrowedCopies = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lstCustomers
      // 
      this.lstCustomers.Location = new System.Drawing.Point(10, 30);
      this.lstCustomers.Size = new System.Drawing.Size(200, 300);
      this.lstCustomers.Name = "lstCustomers";
      // 
      // lstBorrowedCopies
      // 
      this.lstBorrowedCopies.Location = new System.Drawing.Point(220, 30);
      this.lstBorrowedCopies.Size = new System.Drawing.Size(200, 300);
      this.lstBorrowedCopies.Name = "lstBorrowedCopies";
      // 
      // lblCustomers
      // 
      this.lblCustomers.Location = new System.Drawing.Point(10, 10);
      this.lblCustomers.Size = new System.Drawing.Size(200, 20);
      this.lblCustomers.Text = "Customers";
      // 
      // lblBorrowedCopies
      // 
      this.lblBorrowedCopies.Location = new System.Drawing.Point(220, 10);
      this.lblBorrowedCopies.Size = new System.Drawing.Size(200, 20);
      this.lblBorrowedCopies.Text = "Borrowed Copies";
      // 
      // CustomersForm
      // 
      this.ClientSize = new System.Drawing.Size(440, 350);
      this.Controls.Add(this.lstCustomers);
      this.Controls.Add(this.lstBorrowedCopies);
      this.Controls.Add(this.lblCustomers);
      this.Controls.Add(this.lblBorrowedCopies);
      this.Name = "CustomersForm";
      this.Text = "Customers";
      this.ResumeLayout(false);
    }
  }
}
