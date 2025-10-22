using System;
using System.Windows.Forms;
using Predrag_Stojkovic_2021_0414.UIControllers;
using Client.Server;

using Infrastructure.Communication;
using Server.Dtos;
using System.Text.Json.Nodes;
using System.Text.Json;
using Domain.Entities;

namespace Predrag_Stojkovic_2021_0414
{
  public partial class LoginForm : Form
  {
    private readonly LoginController _loginController;
    private readonly ServerAdapter _serverAdapter;

    public LoginForm()
    {
      InitializeComponent();
      _serverAdapter = new ServerAdapter();
      _serverAdapter.Connect();
      _loginController = new LoginController(_serverAdapter);
      btnLogin.Click += BtnLogin_Click;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
      var username = txtUsername.Text;
      var password = txtPassword.Text;
      var response = _loginController.Login(username, password);
      Employee? employee = null;
      if (response != null && response.Data != null && response.Status == 0)
      {
        try
        {
          if (response.Data is JsonElement element)
          {
            employee = JsonSerializer.Deserialize<Employee>(element.GetRawText());
          }
          else
          {
            employee = JsonSerializer.Deserialize<Employee>(response.Data.ToString());
          }
        }
        catch
        {
          employee = null;
        }
      }
      if (employee != null)
      {
        var customersForm = new CustomersForm(_serverAdapter, employee.Library.Id);
        customersForm.Show();
        this.Hide();
      }
      else
      {
        MessageBox.Show(response?.Message ?? "Login failed");
      }
    }
  }
}
