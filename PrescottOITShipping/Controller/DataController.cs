using PrescottOITShipping.Model;

namespace PrescottOITShipping.Controller
{
  internal class DataController
  {
    // our addresses read from the database
    private readonly Dictionary<string, ShippingAddress> _shippingAddresses;
    // our database reader
    private readonly DatabaseReader _dbReader;
    // our directory reader
    private readonly DirectoryReader _directoryReader;
    // constructor
    public DataController()
    {
      // create our database reader
      _dbReader = new();
      // read our addresses from our database
      _shippingAddresses = _dbReader.GetAddressesFromDatabase();
      // create our directory reader
      _directoryReader = new();
    }

    // get the names of the addresses
    public List<string> GetAddressNames()
    {
      // create our return list
      List<string> names = [];
      // loop through our address names
      foreach (string name in _shippingAddresses.Keys)
      {
        // add each name to our list
        names.Add(name);
      }
      // sort our list
      names.Sort();
      // return our list
      return names;
    }

    // get a formatted address string
    public string GetAddressString(string name)
    {
      // check if our addresses has data
      if (_shippingAddresses != null)
      {
        // check if our name is in our dictionary
        if (_shippingAddresses.TryGetValue(name, out ShippingAddress? value))
        {
          // return our string
          return value.ToString();
        }
        else
        {
          // if the value is nto in our keys, return an empty string
          return string.Empty;
        }
      }
      else
      {
        // the addresses are null, return an empty string
        return string.Empty;
      }
    }

    // get the full name of the user
    public string GetUserFullName()
    {
      return $"{_directoryReader.FirstName} {_directoryReader.LastName}";
    }

    // get the email address for the user
    public string GetUserEmailAddress ()
    {
      return _directoryReader.Email;
    }

  }
}
