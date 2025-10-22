using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Predrag_Stojkovic_2021_0414.UIControllers;
using Server.Dtos;
using Client.Server;
using Domain.Entities;

namespace Predrag_Stojkovic_2021_0414
{
  public partial class CustomerDetailsForm : Form
  {
    private readonly CustomerController _customerController;
    private readonly Guid _libraryId;
    private readonly Customer _customer;
    private readonly ServerAdapter _serverAdapter;
    private List<BookCopy> _borrowedCopies;
    private List<BookCopy> _availableCopies;

    private ListBox lstBorrowedCopies;
    private ListBox lstAvailableCopies;
    private Button btnReturnCopy;
    private Button btnBorrowCopy;
    private Label lblBorrowed;
    private Label lblAvailable;

    public CustomerDetailsForm(ServerAdapter serverAdapter, Guid libraryId, Customer customer)
    {
      _serverAdapter = serverAdapter;
      _customerController = new CustomerController(serverAdapter);
      _libraryId = libraryId;
      _customer = customer;
      InitializeComponent();
      LoadCopies();
    }

    private void InitializeComponent()
    {
      this.Text = $"Customer Details - {_customer.FirstName} {_customer.LastName}";
      this.Size = new System.Drawing.Size(500, 400);

      lblBorrowed = new Label { Text = "Borrowed Copies", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(200, 20) };
      lstBorrowedCopies = new ListBox { Location = new System.Drawing.Point(10, 40), Size = new System.Drawing.Size(200, 250) };
      btnReturnCopy = new Button { Text = "Return Selected Copy", Location = new System.Drawing.Point(10, 300), Size = new System.Drawing.Size(200, 30) };
      btnReturnCopy.Click += BtnReturnCopy_Click;

      lblAvailable = new Label { Text = "Available Copies", Location = new System.Drawing.Point(250, 10), Size = new System.Drawing.Size(200, 20) };
      lstAvailableCopies = new ListBox { Location = new System.Drawing.Point(250, 40), Size = new System.Drawing.Size(200, 250) };
      btnBorrowCopy = new Button { Text = "Borrow Selected Copy", Location = new System.Drawing.Point(250, 300), Size = new System.Drawing.Size(200, 30) };
      btnBorrowCopy.Click += BtnBorrowCopy_Click;

      this.Controls.Add(lblBorrowed);
      this.Controls.Add(lstBorrowedCopies);
      this.Controls.Add(btnReturnCopy);
      this.Controls.Add(lblAvailable);
      this.Controls.Add(lstAvailableCopies);
      this.Controls.Add(btnBorrowCopy);
    }

    private void LoadCopies()
    {
      var allCopies = _customerController.GetAllBookCopies(_libraryId);
      _borrowedCopies = allCopies.Where(c => c.Borrower.Id == _customer.Id).ToList();
      _availableCopies = allCopies.Where(c => c.Borrower.Id == Guid.Empty).ToList();

      lstBorrowedCopies.DataSource = _borrowedCopies
        .GroupBy(bc => bc.Book.Id)
        .Select(g => g.First())
        .Select(bc => new { bc.Id, Title = GetBookTitle(bc.Book.Id) })
        .ToList();
      lstBorrowedCopies.DisplayMember = "Title";
      lstBorrowedCopies.ValueMember = "Id";

      lstAvailableCopies.DataSource = _availableCopies
        .GroupBy(bc => bc.Book.Id)
        .Select(g => g.First())
        .Select(bc => new { bc.Id, Title = GetBookTitle(bc.Book.Id) })
        .ToList();
      lstAvailableCopies.DisplayMember = "Title";
      lstAvailableCopies.ValueMember = "Id";
    }

    private string GetBookTitle(Guid bookId)
    {
      var books = _customerController.GetAllBooks(_libraryId);
      var book = books.FirstOrDefault(b => b.Id == bookId);
      return book?.Title ?? "Unknown Book";
    }

    private void BtnReturnCopy_Click(object? sender, EventArgs e)
    {
      if (lstBorrowedCopies.SelectedItem != null)
      {
        dynamic selected = lstBorrowedCopies.SelectedItem;
        Guid copyId = selected.Id;
        _customerController.ReturnBookCopy(copyId, _libraryId);
        LoadCopies();
      }
    }

    private void BtnBorrowCopy_Click(object? sender, EventArgs e)
    {
      if (lstAvailableCopies.SelectedItem != null)
      {
        dynamic selected = lstAvailableCopies.SelectedItem;
        Guid copyId = selected.Id;
        _customerController.BorrowBookCopy(_customer.Id, copyId, _libraryId);
        LoadCopies();
      }
    }
  }
}
