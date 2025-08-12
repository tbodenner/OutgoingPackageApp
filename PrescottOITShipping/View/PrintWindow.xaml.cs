using PrescottOITShipping.Controller;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PrescottOITShipping.View
{
  /// <summary>
  /// Interaction logic for PrintWindow.xaml
  /// </summary>
  public partial class PrintWindow : Window
  {
    // richtextbox that will be used to print from
    private readonly RichTextBox _rtbPrint;
    // our paragraph that will hold our text
    private readonly Paragraph _paragraph;
    // print controller
    private readonly PrintController _printController;
    public PrintWindow(Window owner, PrintController printController)
    {
      // set our print controller
      _printController = printController;
      // initialize our interface objects
      InitializeComponent();
      // set our owner
      this.Owner = owner;
      // set our richtextbox
      _rtbPrint = this.RichTextBoxPrintPreview;

      // prepare our richtextbox to add text
      // create a new flowdocument
      _rtbPrint.Document = new();
      // create our paragraph
      _paragraph = new()
      {
        // center our paraphraph
        TextAlignment = TextAlignment.Center
      };
      // add our paragraph to our document
      _rtbPrint.Document.Blocks.Add(_paragraph);

      // add our print data to our richtextbox
      AddPrintDataToRichTextBox();
    }

    private void AddPrintDataToRichTextBox()
    {
      // add our recipient
      AddRichText(new FontFamily("Arial"), false, GetFontSize(20), Brushes.Black, _printController.Recipient);
      // add our location
      //AddRichText(new FontFamily("Arial"), GetFontSize(12), Brushes.Black, printData.Location);
      // add our address
      AddRichText(new FontFamily("Arial"), false, GetFontSize(20), Brushes.Black, _printController.Address);

      // add a break
      AddRichText(new FontFamily("Arial"), false, GetFontSize(30), Brushes.Black, " ");

      // check if we want a return label
      if (_printController.ReturnLabel == true)
      {
        // add our return label requested line
        AddRichText(new FontFamily("Arial"), true, GetFontSize(12), Brushes.Black, "Return Label Requested");
      }
      
      // add a break
      AddRichText(new FontFamily("Arial"), false, GetFontSize(30), Brushes.Black, " ");

      // add our sender's name
      AddRichText(new FontFamily("Arial"), false, GetFontSize(12), Brushes.Black, _printController.SenderName);
      // add our sender's email
      AddRichText(new FontFamily("Arial"), false, GetFontSize(12), Brushes.Black, _printController.SenderEmail);
    }

    private static double GetFontSize(double size)
    {
      // get the ratio of a font size, multiply it by our desired size, and return the result
      return size * (96.0 / 72.0);
    }

    // add a formatted line to our richtextbox
    private void AddRichText(FontFamily font, bool bold, double size, Brush color, string text)
    {
      // create our run
      Run run = new()
      {
        // set our properties
        Text = $"{text}{Environment.NewLine}",
        FontFamily = font,
        FontSize = size,
        Foreground = color
      };
      if (bold == true)
      {
        // add our bold run to our parapgraph
        _paragraph.Inlines.Add(new Bold(run));
      }
      else
      {
        // add our run to our parapgraph
        _paragraph.Inlines.Add(run);
      }
    }

    private void ButtonCancel_Click(object sender, RoutedEventArgs e)
    {
      // close the window
      this.Close();
    }
  }
}
