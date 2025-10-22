using Client.Server;
using Infrastructure.Communication;
using Predrag_Stojkovic_2021_0414;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class DashboardForm : Form
    {
        private readonly ServerAdapter _adapter;
        private readonly Guid _libraryId;
        private Button btnCustomerDetails;
        private Button btnEmployees;

        public DashboardForm(ServerAdapter adapter, Guid libraryId)
        {
            InitializeComponent();
            _adapter = adapter;
            _libraryId = libraryId;
            btnCustomerDetails = new Button
            {
                Text = "Customer Details",
                Location = new System.Drawing.Point(30, 30),
                Size = new System.Drawing.Size(150, 40)
            };
            btnCustomerDetails.Click += BtnCustomerDetails_Click;
            Controls.Add(btnCustomerDetails);

            btnEmployees = new Button
            {
                Text = "Employees",
                Location = new System.Drawing.Point(30, 80),
                Size = new System.Drawing.Size(150, 40)
            };
            btnEmployees.Click += BtnEmployees_Click;
            Controls.Add(btnEmployees);
        }

        private void BtnCustomerDetails_Click(object sender, EventArgs e)
        {
            var customersForm = new CustomersForm(_adapter, _libraryId);
            customersForm.Show();
        }

        private void BtnEmployees_Click(object sender, EventArgs e)
        {
            var employeesForm = new EmployeesForm(_adapter, _libraryId);
            employeesForm.Show();
        }
    }
}
