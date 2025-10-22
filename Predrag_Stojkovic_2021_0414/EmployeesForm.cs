using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Client.Server;
using Domain.Entities;
using Predrag_Stojkovic_2021_0414.UIControllers;
using Server.Controllers;

namespace Client
{
  public class EmployeesForm : Form
  {
    private readonly ServerAdapter _adapter;
    private readonly Guid _libraryId;
    private ListBox lstEmployees;
    private Button btnRefresh;

    public EmployeesForm(ServerAdapter adapter, Guid libraryId)
    {
      _adapter = adapter;
      _libraryId = libraryId;
      InitializeComponent();
      LoadEmployees();
    }

    private void InitializeComponent()
    {
      lstEmployees = new ListBox { Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(300, 200) };
      btnRefresh = new Button { Text = "Refresh", Location = new System.Drawing.Point(340, 20), Size = new System.Drawing.Size(100, 40) };
      btnRefresh.Click += (s, e) => LoadEmployees();
      Controls.Add(lstEmployees);
      Controls.Add(btnRefresh);
      Text = "Employees";
      ClientSize = new System.Drawing.Size(460, 260);
    }

    private void LoadEmployees()
    {
      lstEmployees.Items.Clear();
      var controller = new Predrag_Stojkovic_2021_0414.UIControllers.EmployeeController(_adapter);
      var employees = controller.GetEmployees(_libraryId);
      foreach (var emp in employees)
      {
        lstEmployees.Items.Add($"{emp.FirstName} {emp.LastName} ({emp.Username})");
      }
    }
  }
}
