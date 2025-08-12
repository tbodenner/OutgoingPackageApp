using PrescottOITShipping.Controller;
using PrescottOITShipping.View;
using System.Windows;
using System.Windows.Controls;

namespace PrescottOITShipping
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // our database controller
    private readonly DatabaseController _databaseController;
    // our print controller
    private readonly PrintController _printController;
    public MainWindow()
    {
      // create our database controller
      _databaseController = new();
      // create our print controller
      _printController = new(_databaseController);
      // initialize our window
      InitializeComponent();
      // set our combobox's itemsource to our address names
      ComboBoxAddressName.ItemsSource = _databaseController.AddressNames;
      // select the first address in our combobox
      ComboBoxAddressName.SelectedIndex = 0;
      // set our user textbox
      //TextBoxFullName.Text = _controller.GetUserFullName();
      TextBoxFullName.IsReadOnly = true;
      // set our user email textbox
      //TextBoxUserEmail.Text = _controller.GetUserEmailAddress();
      TextBoxUserEmail.IsReadOnly = true;
      // make our address textbox read only
      TextBoxFullAddress.IsEnabled = false;
      // set our data context
      this.DataContext = _printController;
    }

    // change our address when our combobox selection changes
    private void ComboBoxAddressName_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // if our checkbox is checked, don't update our textbox on selection change
      if (CheckBoxCustomAddress.IsChecked == true) { return; }
      // check that our combobox and controller are not null
      if (sender is ComboBox comboBox && _databaseController != null)
      {
        if (comboBox.SelectedItem is string name)
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
          if (ComboBoxAddressName.SelectedItem is string name)
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
      Application.Current.Shutdown();
    }

    private void ButtonPrint_Click(object sender, RoutedEventArgs e)
    {
      /*
      // check if our selected location is not a string
      if (ComboBoxAddressName.SelectedItem is not string selectedLocation)
      {
        // this shouldn't happen if the selection is set on app startup, but we will check anyways
        // create and show a message box
        MessageBox.Show("Select a location.", "Location Error", MessageBoxButton.OK, MessageBoxImage.Error);
        // don't do anything else
        return;
      }
      */

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
      // create our window
      PrintWindow printWindow = new(this, _printController);
      // show our window as a dialog window
      printWindow.ShowDialog();
    }
  }
}
