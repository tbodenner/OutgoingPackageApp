using Microsoft.Data.Sqlite;
using PrescottOITShipping.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrescottOITShipping.Controller
{
    class SqliteReader
    {
        private readonly SqliteConnection _connection;
        private static readonly string _sqliteFilename = "Addresses.db";
        private readonly List<ShippingAddress> _shippingAddresses;
        public SqliteReader()
        {
            // create our connection string
            string connectionString = $"Data Source={_sqliteFilename}";
            // create the connection with our connection string
            _connection = new SqliteConnection(connectionString);
            // get our addresses
            _shippingAddresses = getRowsFromDatabase();
        }

        private List<ShippingAddress> getRowsFromDatabase()
        {
            // the command to read all our rows
            string sqlCommand = "SELECT * FROM Addresses;";

            // try to read from the database
            try
            {
                // open the database
                _connection.Open();
                // create our command
                using (SqliteCommand command = new(sqlCommand))
                {
                    // execute our command
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        // read our data
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string name;
                                string address;
                                string city;
                                string state;
                                string zip;
                                // get the entire row
                                object[] row = new object[reader.FieldCount];
                                reader.GetValues(row);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // return an empty list of the return type
            return [];
        }
        // connect to db
        // read db
        // create objects
    }
}
