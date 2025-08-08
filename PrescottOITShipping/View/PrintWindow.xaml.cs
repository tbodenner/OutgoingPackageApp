using System.Windows;
using System.Windows.Controls;

namespace PrescottOITShipping.View
{
  /// <summary>
  /// Interaction logic for PrintWindow.xaml
  /// </summary>
  public partial class PrintWindow : Window
  {
    // richtextbox that will be used to print from
    private readonly RichTextBox _rtfPrint;
    public PrintWindow(Window owner)
    {
      InitializeComponent();
      // set our owner
      this.Owner = owner;
      // set our richtextbox
      _rtfPrint = this.RichTextBoxPrintPreview;
    }
  }
}
