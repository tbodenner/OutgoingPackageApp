using PrescottOITShipping.Model;

namespace PrescottOITShipping.Controller
{
  internal class DataController
  {
    // our addresses read from the database
    private readonly Dictionary<string, ShippingAddress> _shippingAddresses;
    // constructor
    public DataController()
    {
      // create our database reader
      DatabaseReader dbReader = new();
      // read our addresses from our database
      _shippingAddresses = dbReader.GetAddressesFromDatabase();
    }

    // get the names of the addresses
    public List<string> GetAddressNames()
    {
      // return our keys as a list of strings
      return [.. _shippingAddresses.Keys];
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
  }
}
