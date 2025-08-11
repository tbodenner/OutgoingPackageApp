namespace PrescottOITShipping.Controller
{
  class PrintData(string location, string recipient, string address, string senderName, string senderEmail, bool returnLabel)
  {
    // location name
    private readonly string _location = location;
    // recipient's name
    private readonly string _recipient = recipient;
    // location address
    private readonly string _address = address;
    // sender's name
    private readonly string _senderName = senderName;
    // sender's email
    private readonly string _senderEmail = senderEmail;
    // return label required
    private readonly bool _returnLabel = returnLabel;

    // getters
    public string Location { get { return _location; } }
    public string Recipent { get { return _recipient; } }
    public string Address { get { return _address; } }
    public string SenderName { get { return _senderName; } }
    public string SenderEmail { get { return _senderEmail; } }
    public bool ReturnLabel { get { return _returnLabel; } }
  }
}
