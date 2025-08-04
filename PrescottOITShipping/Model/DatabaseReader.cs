using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Windows;

namespace PrescottOITShipping.Model
{
  class DatabaseReader
  {
    private readonly SqliteConnection _connection;
    private static readonly string _sqliteFilename = "Addresses.db";
    private readonly Dictionary<string, ShippingAddress> _shippingAddresses;
    public DatabaseReader()
    {
      // create our connection string
      //string connectionString = $"Data Source={_sqliteFilename}";
      SqliteConnectionStringBuilder connectionString = new()
      {
        DataSource = _sqliteFilename
      };
      // create the connection with our connection string
      _connection = new SqliteConnection(connectionString.ToString());
      // get our addresses
      _shippingAddresses = GetRowsFromDatabase();
    }

    private Dictionary<string, ShippingAddress> GetRowsFromDatabase()
    {
      // the command to read all our rows
      string sqlCommand = "SELECT * FROM Addresses;";
      // our address dictionary to return
      Dictionary<string, ShippingAddress> addressDict = [];

      // try to read from the database
      try
      {
        // open the database
        _connection.Open();
        // create our command
        using SqliteCommand command = new(sqlCommand, _connection);
        // execute our command
        using SqliteDataReader reader = command.ExecuteReader();
        // read our data
        if (reader != null && reader.HasRows)
        {
          // loop through our data
          while (reader.Read())
          {
            // get the address name
            string name = reader.GetString(reader.GetOrdinal("name"));
            // get the address data, create the address, and add the address object to our list
            addressDict.Add
            (
              name,
              new
              (
                name,
                reader.GetString(reader.GetOrdinal("address")),
                reader.GetString(reader.GetOrdinal("city")),
                reader.GetString(reader.GetOrdinal("state")),
                reader.GetString(reader.GetOrdinal("zip"))
              )
            );
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "GetRowsFromDatabase Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      foreach (string addressName in addressDict.Keys)
      {
        Debug.WriteLine(addressDict[addressName].ToString());
      }

      // return out filled list
      return addressDict;
    }
    // connect to db
    // read db
    // create objects
  }
}
