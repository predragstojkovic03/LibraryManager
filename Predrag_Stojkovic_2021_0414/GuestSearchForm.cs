using System;
using System.Windows.Forms;
using Client.Server;
using Predrag_Stojkovic_2021_0414.UIControllers;

namespace Predrag_Stojkovic_2021_0414
{
  public class GuestSearchForm : Form
  {
    private readonly ServerAdapter _serverAdapter;
    private readonly UIControllers.GuestController _guestController;
    private TextBox? txtSearch;
    private Button? btnSearch;
    private ListBox? lstResults;
    private Label? lblInfo;

    public GuestSearchForm(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
      _guestController = new Predrag_Stojkovic_2021_0414.UIControllers.GuestController(_serverAdapter);
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      txtSearch = new TextBox();
      btnSearch = new Button();
      lstResults = new ListBox();
      lblInfo = new Label();
      SuspendLayout();
      // 
      // txtSearch
      // 
      txtSearch.Location = new Point(12, 12);
      txtSearch.Name = "txtSearch";
      txtSearch.Size = new Size(179, 27);
      txtSearch.TabIndex = 0;
      txtSearch.TextChanged += txtSearch_TextChanged;
      // 
      // btnSearch
      // 
      btnSearch.Location = new Point(208, 12);
      btnSearch.Name = "btnSearch";
      btnSearch.Size = new Size(101, 27);
      btnSearch.TabIndex = 1;
      btnSearch.Text = "Pretrazi";
      btnSearch.Click += BtnSearch_Click;
      // 
      // lstResults
      // 
      lstResults.Location = new Point(12, 57);
      lstResults.Name = "lstResults";
      lstResults.Size = new Size(746, 284);
      lstResults.TabIndex = 2;
      // 
      // lblInfo
      // 
      lblInfo.Location = new Point(0, 0);
      lblInfo.Name = "lblInfo";
      lblInfo.Size = new Size(100, 23);
      lblInfo.TabIndex = 3;
      // 
      // GuestSearchForm
      // 
      ClientSize = new Size(770, 362);
      Controls.Add(txtSearch);
      Controls.Add(btnSearch);
      Controls.Add(lstResults);
      Controls.Add(lblInfo);
      Name = "GuestSearchForm";
      Text = "Book & Library Search";
      ResumeLayout(false);
      PerformLayout();
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
      if (txtSearch == null || lstResults == null || lblInfo == null)
        return;
      string query = txtSearch.Text.Trim();
      lstResults.Items.Clear();
      if (string.IsNullOrEmpty(query))
      {
        lblInfo.Text = "Please enter a search term.";
        return;
      }
      var results = _guestController.SearchBooksAndLibraries(query);
      if (results.Count == 0)
      {
        lstResults.Items.Add("No results found.");
        lblInfo.Text = "No books found for your search.";
      }
      else
      {
        foreach (var result in results)
        {
          var copy = result.copy;
          var book = result.book;
          var library = result.library;
          lstResults.Items.Add($"Book: {book.Title} by {book.Author} | Library: {library.Name} | Copy ID: {copy.Id}");
        }
        lblInfo.Text = "Results shown below.";
      }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
