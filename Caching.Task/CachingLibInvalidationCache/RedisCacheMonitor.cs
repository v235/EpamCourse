using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using NorthwindLibrary;
using System.Threading;
using CachingProviders.RedisLib;

namespace CachingLibInvalidationCache
{
    public class RedisCacheMonitor
    {
        private IRedisProvider redisCache;
        private SqlChangeMonitor monitor;
        private SqlDependency dependency;
        private bool hasDataChanged;
        private string key;

        const string CONNECTION_STRING = @"data source=EPBYBREW0144\;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        const string SQL_STATEMENT = "SELECT EmployeeID, LastName, FirstName  FROM dbo.Employees";


        public RedisCacheMonitor()
        {
            redisCache = new RedisProvider();
            Initialization();
        }
        void Initialization()
        {
            // Create a dependency connection.
            SqlDependency.Start(CONNECTION_STRING);
        }

        private IEnumerable<Employee> LoadData()
        {
            var data = new List<Employee>();
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_STATEMENT, connection))
                {
                    //Add new dependency
                    dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee()
                            {
                                EmployeeID = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2)
                            };
                            data.Add(employee);
                        }
                    }
                }
            }
            return data;
        }
        private void CacheRemovedCallback(CacheEntryRemovedArguments args)
        {
            if (monitor != null)
                monitor.Dispose();
        }

        void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Some value in DB changed");
            redisCache.DeleteFromRedis(key);
        }

        void Termination()
        {
            // Release the dependency.
            SqlDependency.Stop(CONNECTION_STRING);
        }
        public IEnumerable<Employee> Select()
        {
            key = Thread.CurrentPrincipal.Identity.Name;
            Thread.Sleep(2000);
            var result = redisCache.GetFromRedis<Employee>(key);
            if (result == null)
            {
                var item = LoadData();
                redisCache.AddToRedis(key, item);
                return item;
            }
            return result;
        }
    }
}