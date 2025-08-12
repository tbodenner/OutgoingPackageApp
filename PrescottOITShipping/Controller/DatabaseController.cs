using PrescottOITShipping.Model;

namespace PrescottOITShipping.Controller
{
  public class DatabaseController
  {
    // addresses read from the database
    private readonly Dictionary<string, ShippingAddress> _shippingAddresses;
    // database reader
    private readonly DatabaseReader _dbReader;
    // directory reader
    private readonly DirectoryReader _directoryReader;
    // adddress list
    private readonly List<string> _addressNames;

    // constructor
    public DatabaseController()
    {
      // create our database reader
      _dbReader = new();
      // read our addresses from our database
      _shippingAddresses = _dbReader.GetAddressesFromDatabase();
      // create our directory reader
      _directoryReader = new();
      // create our address list
      _addressNames = CreateAddressNamesList();
    }

    private List<string> CreateAddressNamesList()
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

    // get the names of the addresses
    public List<string> AddressNames
    {
      get
      {
        return _addressNames;
      }
    }

    // get a formatted address string
    public Dictionary<string, ShippingAddress> Addresses
    {
      get
      {
        return _shippingAddresses;
      }
    }

    // get the full name of the user
    public string UserFullName
    {
      get
      {
        return $"{_directoryReader.FirstName} {_directoryReader.LastName}";
      }
    }

    // get the email address for the user
    public string UserEmailAddress
    {
      get
      {
        return _directoryReader.Email;
      }
    }
  }
}
