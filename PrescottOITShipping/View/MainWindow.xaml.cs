using PrescottOITShipping.Controller;
using PrescottOITShipping.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PrescottOITShipping
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // our database controller
    private readonly DatabaseController? _databaseController;
    // our print controller
    private readonly PrintController? _printController;
    public MainWindow()
    {
      // catch any errors creating the main window
      try
      {
        // initialize our window
        InitializeComponent();
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to create main window.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      // catch any errors creating the main window
      try
      {
        // create our database controller
        _databaseController = new();
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to create database controller.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      // catch any errors creating the main window
      try
      {
        // our string to pass on to our print controller
        string userName = string.Empty;
        string userEmail = string.Empty;
        // check if our database controller is not null
        if (_databaseController != null)
        {
          // set our values from the database controller
          userName = _databaseController.UserFullName;
          userEmail = _databaseController.UserEmailAddress;
        }
        // create our print controller
        _printController = new(userName, userEmail);
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to create print controller.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      // catch any errors creating the main window
      try
      {
        // check if our database controller is not null
        if (_databaseController != null)
        {
          // set our combobox's itemsource to our address names
          ComboBoxAddressName.ItemsSource = _databaseController.AddressNames;
        }
        // check if our source contains items
        if (ComboBoxAddressName.Items.Count > 1)
        {
          // select the first address in our combobox
          ComboBoxAddressName.SelectedIndex = 0;
        }
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to set combobox properties.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      // catch any errors creating the main window
      try
      {
        // make our user textbox to read only
        TextBoxFullName.IsReadOnly = true;
        // make our user email textbox to read only
        TextBoxUserEmail.IsReadOnly = true;
        // make our address textbox read only
        TextBoxFullAddress.IsEnabled = false;
        // set our data context
        DataContext = _printController;
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to set textbox properties.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      // catch any errors creating the main window
      try
      {
        TextBoxFullName.DataContext = _printController;
        TextBoxFullName.SetBinding(TextBox.TextProperty, new Binding("SenderName"));

        TextBoxUserEmail.DataContext = _printController;
        TextBoxUserEmail.SetBinding(TextBox.TextProperty, new Binding("SenderEmail"));

        CheckBoxReturnLabel.DataContext = _printController;
        CheckBoxReturnLabel.SetBinding(CheckBox.IsCheckedProperty, new Binding("ReturnLabel"));

        CheckBoxQuickPrint.DataContext = _printController;
        CheckBoxQuickPrint.SetBinding(CheckBox.IsCheckedProperty, new Binding("QuickPrint"));

        LabelPrinterName.DataContext = _printController;
        LabelPrinterName.SetBinding(Label.ContentProperty, new Binding("PrinterName"));

        TextBoxShipToPerson.DataContext = _printController;
        TextBoxShipToPerson.SetBinding(TextBox.TextProperty, new Binding("Recipient"));

        TextBoxFullAddress.DataContext = _printController;
        TextBoxFullAddress.SetBinding(TextBox.TextProperty, new Binding("Address"));
      }
      catch (Exception ex)
      {
        // show the error
        MessageBox.Show($"Failed to set data bindings.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    // change our address when our combobox selection changes
    private void ComboBoxAddressName_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // if our checkbox is checked, don't update our textbox on selection change
      if (CheckBoxCustomAddress.IsChecked == true) { return; }
      // check that our combobox and controller are not null
      if (sender is ComboBox comboBox && _databaseController != null)
      {
        if (_printController != null && comboBox.SelectedItem is string name)
        {
          // get our text from out address
          _printController.Address = _databaseController.Addresses[name].ToString();
        }
      }
    }

    private void CheckBoxCustomAddress_CheckState(object sender, RoutedEventArgs e)
    {
      // check that our sender is the correct control
      if (sender is CheckBox checkBox)
      {
        // check if our checkbox is checked
        if (checkBox.IsChecked == true)
        {
          // if checked, enable our textbox
          TextBoxFullAddress.IsEnabled = true;
        }
        // check if our checkbox is not checked
        else if (checkBox.IsChecked == false)
        {
          // if not checked, disable our textbox
          TextBoxFullAddress.IsEnabled = false;
          // check if our combobox has a valid selection
          if (_databaseController != null && ComboBoxAddressName.SelectedItem is string name)
          {
            // set our address from our combox's selected item
            TextBoxFullAddress.Text = _databaseController.Addresses[name].ToString();
          }
        }
      }
    }

    private void ButtonExit_Click(object sender, RoutedEventArgs e)
    {
      // exit our application
      System.Windows.Application.Current.Shutdown();
    }

    private void ButtonPrint_Click(object sender, RoutedEventArgs e)
    {
      // check our recipient
      if (TextBoxShipToPerson.Text == string.Empty || TextBoxShipToPerson.Text == "Recipient Name")
      {
        // create and show a message box
        MessageBox.Show("Add a recipient name.", "Recipient Error", MessageBoxButton.OK, MessageBoxImage.Error);
        // focus the textbox
        TextBoxShipToPerson.Focus();
        // select all the text in the textbox
        TextBoxShipToPerson.SelectAll();
        // don't do anything else
        return;
      }

      // check if our print controller is not null
      if (_printController != null)
      {
        // check if we are quick printing
        if (_printController.QuickPrint == true)
        {
          // create our document and print it
          PrintController.PrintDocument(_printController.QuickPrint, _printController.CreateDocument(), _printController.Margin);
        }
        else
        {
          // otherwise, create our window add our document
          PrintWindow printWindow = new(this, _printController.CreateDocument(), _printController.Margin);
          // show our window as a dialog window
          printWindow.ShowDialog();
        }
      }
    }
  }
}
