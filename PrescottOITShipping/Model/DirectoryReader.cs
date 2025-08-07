using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PrescottOITShipping.Model
{
  internal class DirectoryReader
  {
    // current user
    private readonly string _userDomainName;
    private readonly string _userName;
    private readonly string _domain;
    // domains
    private readonly Dictionary<string, string> _domainNames = new()
    {
      { "va", "va.gov" },
      { "vha18", "v18.med.va.gov" }
    };
    private readonly Dictionary<string, DirectoryEntry> _domainEntries = [];
    // current user AD info
    private string? _firstName = null;
    private string? _lastName = null;
    private string? _email = null;

    public DirectoryReader()
    {
      // get our full user domain name
      _userDomainName = WindowsIdentity.GetCurrent().Name;
      // split our domain name
      string[] splitDomainName = _userDomainName.Split('\\');
      // get our domain
      _domain = splitDomainName[0].ToLower();
      // get our user
      _userName = splitDomainName[1].ToLower();
      // get our domain entries from our domain names
      foreach (string key in _domainNames.Keys)
      {
        // create a directory entry from the full domain name
        _domainEntries.Add(key, new DirectoryEntry(_domainNames[key]));
        _domainEntries[key].AuthenticationType = AuthenticationTypes.Secure;
      }
      // get ad info
      GetUserADInfo();
    }

    private void GetUserADInfo()
    {
      // create our object to search for our user
      DirectorySearcher searcher = new(_domainNames[_domain]);
      searcher.Filter = $"(SamAccountName={_userName})";
      // search for our user
      SearchResult? result = searcher.FindOne();
      // check if our result returned a user
      if (result != null)
      {
        // check if our result contains a given name
        if (result.Properties.Contains("givenname"))
        {
          // get the given name value and convert to a string
          _firstName = Convert.ToString(result.Properties["givenname"][0]);
        }
        // check if our result contains a surname
        if (result.Properties.Contains("sn"))
        {
          // get the surname value and convert to a string
          _lastName = Convert.ToString(result.Properties["sn"][0]);
        }
        // check if our result contains an email address
        if (result.Properties.Contains("mail"))
        {
          // get the email address value and convert to a string
          _email = Convert.ToString(result.Properties["mail"][0]);
          // check if our email is not null
          if (_email != null)
          {
            // change our email to lower case
            _email = _email.ToLower();
          }
        }
      }
    }

    // get email property
    public string Email
    {
      get
      {
        if (_email == null)
        {
          return string.Empty;
        }
        else
        {
          return _email;
        }
      }
    }
    // get first name property
    public string FirstName
    {
      get
      {
        if (_firstName == null)
        {
          return string.Empty;
        }
        else
        {
          return _firstName;
        }
      }
    }
    // get last name property
    public string LastName
    {
      get
      {
        if (_lastName == null)
        {
          return string.Empty;
        }
        else
        {
          return _lastName;
        }
      }
    }

  }
}
