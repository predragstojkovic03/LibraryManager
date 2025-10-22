using System;
using System.Windows.Forms;
using Predrag_Stojkovic_2021_0414.UIControllers;
using Client.Server;
using Domain.Entities;

namespace Predrag_Stojkovic_2021_0414
{
  public partial class CustomersForm : Form
  {
    private readonly CustomerController _customerController;
    private Guid _libraryId;
    private readonly ServerAdapter _serverAdapter;

    public CustomersForm(ServerAdapter serverAdapter, Guid libraryId)
    {
      InitializeComponent();
      this.Text = "Library Customers & Borrowed Books";
      _customerController = new CustomerController(serverAdapter);
      _libraryId = libraryId;
      LoadCustomers();
      lstCustomers.SelectedIndexChanged += LstCustomers_SelectedIndexChanged;
      lstCustomers.DoubleClick += LstCustomers_DoubleClick;
      _serverAdapter = serverAdapter;
    }
    private void LstCustomers_DoubleClick(object? sender, EventArgs e)
    {
      if (lstCustomers.SelectedItem != null)
      {
        dynamic selected = lstCustomers.SelectedItem;
        var customer = _customerController.GetCustomers(_libraryId).FirstOrDefault(c => c.Id == selected.Id);
        if (customer != null)
        {
          var detailsForm = new CustomerDetailsForm(_serverAdapter, _libraryId, customer);
          detailsForm.ShowDialog();
        }
      }
    }

    private void LoadCustomers()
    {
      var customers = _customerController.GetCustomers(_libraryId)
        .Select(c => new { c.Id, FullName = $"{c.FirstName} {c.LastName}" }).ToList();
      lstCustomers.DataSource = customers;
      lstCustomers.DisplayMember = "FullName";
      lstCustomers.ValueMember = "Id";
    }

    private void LstCustomers_SelectedIndexChanged(object? sender, EventArgs e)
    {
      if (lstCustomers.SelectedItem != null)
      {
        dynamic customer = lstCustomers.SelectedItem;
        var copies = _customerController.GetBorrowedCopies(customer.Id) as List<BookCopy>;
        if (copies != null)
        {
          var bookTitles = copies.Select(bc => new { bc.Id, BookTitle = GetBookTitle(bc.Book.Id) }).ToList();
          lstBorrowedCopies.DataSource = bookTitles;
          lstBorrowedCopies.DisplayMember = "BookTitle";
          lstBorrowedCopies.ValueMember = "Id";
        }
      }
    }

    private string GetBookTitle(Guid bookId)
    {
      // You may want to cache or optimize this in a real app
      var allBooks = _customerController.GetAllBooks(_libraryId);
      var book = allBooks.FirstOrDefault(b => b.Id == bookId);
      return book?.Title ?? "Unknown Book";
    }
  }
}
