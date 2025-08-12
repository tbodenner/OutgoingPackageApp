using System.ComponentModel;

namespace PrescottOITShipping.Controller
{
  public class PrintController : INotifyPropertyChanged
  {
    // our data controller
    private readonly DatabaseController _dataController;
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

    // constructor
    public PrintController(DatabaseController dataController)
    {
      // initialize our properties
      _dataController = dataController;
      _location = _dataController.AddressNames[0];
      _recipient = "Recipient Name";
      _address = _dataController.Addresses[_location].ToString();
      _senderName = _dataController.UserFullName;
      _senderEmail = _dataController.UserEmailAddress;
      _returnLabel = true;
      _customAddress = false;
      _quickPrint = false;
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

    // implement our interface 
    public event PropertyChangedEventHandler? PropertyChanged;
    // create a notify event
    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
