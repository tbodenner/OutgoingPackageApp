using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.IO;

namespace PrescottOITShipping.Model
{
  class DatabaseReader
  {
    private readonly SqliteConnection _connection;
    private static readonly string _sqliteFilename = "Addresses.db";
    public DatabaseReader()
    {
      // check if our databse file exist
      if (File.Exists(_sqliteFilename))
      {
        // create our connection string
        SqliteConnectionStringBuilder connectionString = new()
        {
          DataSource = _sqliteFilename,
          Mode = SqliteOpenMode.ReadOnly
        };
        // create the connection with our connection string
        _connection = new SqliteConnection(connectionString.ToString());
      }
      else
      {
        // throw an exception
        throw new FileNotFoundException($"Unable to find database file '{_sqliteFilename}'.");
      }
    }

    public Dictionary<string, ShippingAddress> GetAddressesFromDatabase()
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
            // get the address data, create the address, and add the address object to our dictionary
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
        throw new Exception("GetRowsFromDatabase Error", ex);
      }

      foreach (string addressName in addressDict.Keys)
      {
        Debug.WriteLine(addressDict[addressName].ToString());
      }

      // return our filled list
      return addressDict;
    }
  }
}
