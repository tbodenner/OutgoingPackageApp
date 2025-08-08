using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}
