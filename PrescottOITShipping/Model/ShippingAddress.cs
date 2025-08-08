namespace PrescottOITShipping.Model
{
  // store each part of an address
  class ShippingAddress(string name, string address, string state, string city, string zip)
  {
    // properties
    private readonly string _name = name;
    private readonly string _address = address;
    private readonly string _city = city;
    private readonly string _state = state;
    private readonly string _zipcode = zip;
    // return the shipping formatted address
    override public string ToString()
    {
      string nl = System.Environment.NewLine;
      return $"{_name}{nl}{_address}{nl}{_city}, {_state} {_zipcode}";
    }
  }
}
