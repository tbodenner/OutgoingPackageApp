using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrescottOITShipping.Controller;
using PrescottOITShipping.Model;

namespace PrescottOITShipping
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // our data controller
    private DataController _controller;
    public MainWindow()
    {
      // create our data controller
      _controller = new();
      // initialize our window
      InitializeComponent();
      ComboBoxAddressName.ItemsSource = _controller.GetAddressNames();
      ComboBoxAddressName.SelectedIndex = 0;
    }

    // change our address when our combobox selection changes
    private void ComboBoxAddressName_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // check that our combobox and controller are not null
      if (sender is ComboBox comboBox && _controller != null)
      {
        string? name = comboBox.SelectedItem as string;
        if (name != null)
        {
          // get our text from out address
          string address = _controller.GetAddressString(name);
          TextBlockFullAddress.Text = address;
        }
      }
    }
  }
}
