using System;
using System.Collections.Generic;
using System.IO;
using ArangoDB.Client.Data;
using AstDomain;
using AstShared;

namespace AstArangoDbConnector
{
    public class DatabaseManager : ArangoDbConnector, IDatabaseManager
    {
        public bool CreateDatabase(Config config, string dbName)
        {
            using (var db = GetDatabase(config))
            {
                try
                {
                    Console.WriteLine(config.ArangoDbUser);
                    db.CreateDatabase(
                    dbName, 
                    new List<DatabaseUser>
                    { 
                        new DatabaseUser 
                        { 
                            Username = config.ArangoDbUser,
                            Passwd = config.ArangoDbPassword,
                            Active = true
                        }
                    });
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.GetType().Name} {ex.Message}");
                }
                
                return false;
            }
        }

        public bool CheckDatabase(Config config)
        {
            using (var db = GetDatabase(config))
            {
                try
                {
                    DatabaseInformation info = db.CurrentDatabaseInformation();
                    Console.WriteLine("==================================");
                    Console.WriteLine($"Database id : {info.Id}");
                    Console.WriteLine($"Database Name : {info.Name}");
                    Console.WriteLine($"Database is system : {info.IsSystem}");
                    Console.WriteLine($"Database path : {info.Path}");
                    Console.WriteLine("==================================");
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                return false;
                
            }
        }

        public bool BackupDatabase(Config config, string backupFolder, string backupFileName)
        {
            //TODO: https://www.arangodb.com/docs/stable/programs-arangobackup-examples.html
            return false;
        }

        public bool DeleteDatabase(Config config)
        {
            using (var db = GetDatabase(config))
            {
                try
                {
                    db.DropDatabase(config.ArangoDbDatabse);
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                return false;
            }
        }
    }
}
