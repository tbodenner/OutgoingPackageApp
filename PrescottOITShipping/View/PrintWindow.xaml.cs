using PrescottOITShipping.Controller;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PrescottOITShipping.View
{
  /// <summary>
  /// Interaction logic for PrintWindow.xaml
  /// </summary>
  public partial class PrintWindow : Window
  {
    // the document to be printed
    private readonly FlowDocument _document;
    // our page margin
    private readonly Thickness _margin;
    // constructor
    public PrintWindow(Window owner, FlowDocument document, Thickness margin)
    {
      // initialize our interface objects
      InitializeComponent();
      // set our owner
      this.Owner = owner;
      // set our document
      _document = document;
      // set our margin
      _margin = margin;
      // set our richtextbox's document
      this.RichTextBoxPrintPreview.Document = document;
    }

    private void ButtonClose_Click(object sender, RoutedEventArgs e)
    {
      // close the window
      this.Close();
    }

    private void ButtonPrint_Click(object sender, RoutedEventArgs e)
    {
      // print our document
      PrintController.PrintDocument(false, _document, _margin);
      // close the window
      this.Close();
    }
  }
}
