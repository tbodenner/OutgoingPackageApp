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
    // our data controller
    private readonly DataController _controller;
    public MainWindow()
    {
      // create our data controller
      _controller = new();
      // initialize our window
      InitializeComponent();
      // set our combobox's itemsource to our address names
      ComboBoxAddressName.ItemsSource = _controller.GetAddressNames();
      // select the first address in our combobox
      ComboBoxAddressName.SelectedIndex = 0;
      // set our user textbox
      TextBoxFullName.Text = _controller.GetUserFullName();
      TextBoxFullName.IsReadOnly = true;
      // set our user email textbox
      TextBoxUserEmail.Text = _controller.GetUserEmailAddress();
      TextBoxUserEmail.IsReadOnly = true;
      // make our address textbox read only
      TextBoxFullAddress.IsEnabled = false;
    }

    // change our address when our combobox selection changes
    private void ComboBoxAddressName_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // if our checkbox is checked, don't update our textbox on selection change
      if (CheckBoxCustomAddress.IsChecked == true) { return; }
      // check that our combobox and controller are not null
      if (sender is ComboBox comboBox && _controller != null)
      {
        if (comboBox.SelectedItem is string name)
        {
          // get our text from out address
          TextBoxFullAddress.Text = _controller.GetAddressString(name);
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
            TextBoxFullAddress.Text = _controller.GetAddressString(name);
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
      PrintWindow printWindow = new(this);
      printWindow.ShowDialog();
    }
  }
}
