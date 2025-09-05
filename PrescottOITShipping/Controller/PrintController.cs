using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace PrescottOITShipping.Controller
{
  public class PrintController : INotifyPropertyChanged
  {
    // data bindings
    // location
    private string _location;
    // recipient's name
    private string _recipient;
    // location address
    private string _address;
    // sender's name
    private string _senderName;
    // sender's email
    private string _senderEmail;
    // return label required checkbox
    private bool _returnLabel;
    // custom address checkbox
    private bool _customAddress;
    // quick print checkbox
    private bool _quickPrint;
    // landscape checkbox
    private bool _landscape;

    // default printer
    private readonly string _printerName;
    // set our margin to 2 inches - print padding is measured in 1/96th of an inch
    private readonly Thickness _margin;

    // constructor
    public PrintController(string userName, string userEmail)
    {
      // initialize our properties
      _location = string.Empty;
      _recipient = "Recipient Name";
      _address = string.Empty;
      _senderName = userName;
      _senderEmail = userEmail;
      _returnLabel = true;
      _customAddress = false;
      _quickPrint = false;
      _landscape = false;
      _margin = new(2 * 96);

      try
      {
        // get our printer settings
        PrinterSettings printSettings = new();
        // get our default printer
        string printerName = printSettings.PrinterName;
        // check if our printer name has a slash
        if (printerName.Contains('\\'))
        {
          // if it does, split our printer string
          string[] splitName = printerName.Trim('\\').Split('\\');
          // check if we have one or more strings
          if (splitName != null && splitName.Length >= 1)
          {
            // get the printer's name
            _printerName = splitName[1];
          }
          else
          {
            // something went wrong with our split, so set our string to empty
            _printerName = string.Empty;
          }
        }
        else
        {
          // otherwise, set our printer name
          _printerName = printerName;
        }
      }
      catch
      {
        // on any errors our string will be empty
        _printerName = string.Empty;
      }
    }

    public FlowDocument CreateDocument()
    {
      // our document stored by our richtextbox
      FlowDocument document = new()
      {
        // set our padding to 2 inches - print padding is measured in 1/96th of an inch
        PagePadding = _margin
      };
      // our paragraph that will hold our text
      Paragraph paragraph = new()
      {
        // center our paraphraph
        TextAlignment = TextAlignment.Center
      };
      // add our paragraph to our document
      document.Blocks.Add(paragraph);

      // add our recipient
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(20), Brushes.Black, $"ATTN: {_recipient}"));
      // add our address
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(20), Brushes.Black, _address));

      // add a break
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(30), Brushes.Black, " "));

      // check if we want a return label
      if (_returnLabel == true)
      {
        // add our bolded return label requested line
        paragraph.Inlines.Add(new Bold(CreateRun(new FontFamily("Arial"), GetFontSize(12), Brushes.Black, "Return Label Requested")));
      }

      // add a break
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(30), Brushes.Black, " "));

      // add our sender's name
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(12), Brushes.Black, _senderName));
      // add our sender's email
      paragraph.Inlines.Add(CreateRun(new FontFamily("Arial"), GetFontSize(12), Brushes.Black, _senderEmail));

      // return the completed document
      return document;
    }

    public static void PrintDocument(bool quickPrint, FlowDocument document, Thickness margin, bool landscape)
    {
      // create our print dialog
      PrintDialog printDialog = new();

      // check if we are not quick printing
      if (quickPrint != true)
      {
        // show our dialog, if not true, do nothing
        if (printDialog.ShowDialog() != true) { return; }
      }

      // start our print
      // clone our document
      FlowDocument? clonedDocument = PrintController.CloneDocument(document);
      // check if our document is not null
      if (clonedDocument != null)
      {
        // set our document's page padding
        clonedDocument.PagePadding = margin;
        // check if we are printing in landscape
        if (landscape == true)
        {
          // set our print direction
          printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
        }
        // create our paginator
        IDocumentPaginatorSource pageSource = clonedDocument;
        // print our document
        printDialog.PrintDocument(pageSource.DocumentPaginator, "Prescott OIT Shipping");
      }
    }

    // static methods
    public static FlowDocument? CloneDocument(FlowDocument document)
    {
      // check if our document is null, if null, return a null
      if (document == null) { return null; }

      // serialize the document to xaml
      string documentXaml = XamlWriter.Save(document);

      // load our xaml into a memory stream
      using MemoryStream stream = new(System.Text.Encoding.UTF8.GetBytes(documentXaml));
      // create a new document by deserializing the xaml
      FlowDocument clonedDocument = (FlowDocument)XamlReader.Load(stream);
      // return the cloned document
      return clonedDocument;
    }

    private static double GetFontSize(double size)
    {
      // get the ratio of a font size, multiply it by our desired size, and return the result
      return size * (96.0 / 72.0);
    }

    // add a formatted line to our richtextbox
    private static Run CreateRun(FontFamily font, double size, Brush color, string text)
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
      //return the completed run
      return run;
    }

    // property getters and setters
    public string Location
    {
      get { return _location; }
      set
      {
        if (_location != value)
        {
          _location = value;
          OnPropertyChanged(nameof(Location));
        }
      }
    }
    public string Recipient
    {
      get { return _recipient; }
      set
      {
        if (_recipient != value)
        {
          _recipient = value;
          OnPropertyChanged(nameof(Recipient));
        }
      }
    }
    public string Address
    {
      get { return _address; }
      set
      {
        if (_address != value)
        {
          _address = value;
          OnPropertyChanged(nameof(Address));
        }
      }
    }
    public string SenderName
    {
      get { return _senderName; }
      set
      {
        if (_senderName != value)
        {
          _senderName = value;
          OnPropertyChanged(nameof(SenderName));
        }
      }
    }
    public string SenderEmail
    {
      get { return _senderEmail; }
      set
      {
        if (_senderEmail != value)
        {
          _senderEmail = value;
          OnPropertyChanged(nameof(SenderEmail));
        }
      }
    }
    public bool ReturnLabel
    {
      get { return _returnLabel; }
      set
      {
        if (_returnLabel != value)
        {
          _returnLabel = value;
          OnPropertyChanged(nameof(ReturnLabel));
        }
      }
    }
    public bool CustomAddress
    {
      get { return _customAddress; }
      set
      {
        if (_customAddress != value)
        {
          _customAddress = value;
          OnPropertyChanged(nameof(_customAddress));
        }
      }
    }
    public bool QuickPrint
    {
      get { return _quickPrint; }
      set
      {
        if (_quickPrint != value)
        {
          _quickPrint = value;
          OnPropertyChanged(nameof(QuickPrint));
        }
      }
    }
    public bool Landscape
    {
      get { return _landscape; }
      set
      {
        if (_landscape != value)
        {
          _landscape = value;
          OnPropertyChanged(nameof(Landscape));
        }
      }
    }
    public string PrinterName
    {
      get { return _printerName; }
    }
    public Thickness Margin
    {
      get { return _margin; }
    }
    // implement our interface 
    public event PropertyChangedEventHandler? PropertyChanged;
    // create a notify event
    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
