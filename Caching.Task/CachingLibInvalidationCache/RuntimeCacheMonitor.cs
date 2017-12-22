using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using NorthwindLibrary;
using System.Threading;

namespace CachingLibInvalidationCache
{
    public class RuntimeCacheMonitor
    {
        private static MemoryCache cache;
        private SqlChangeMonitor monitor;
        private SqlDependency dependency;
        private bool hasDataChanged;

        const string CONNECTION_STRING = @"data source=EPBYBREW0144\;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        const string SQL_STATEMENT = "SELECT EmployeeID, LastName, FirstName  FROM dbo.Employees";


        public RuntimeCacheMonitor()
        {
            cache = MemoryCache.Default;
            Initialization();
        }
        void Initialization()
        {
            // Create a dependency connection.
            SqlDependency.Start(CONNECTION_STRING);
        }

        private CacheItem LoadData(out CacheItemPolicy policy, string key)
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
                    // add monitor
                    monitor = new SqlChangeMonitor(dependency);

                    policy = new CacheItemPolicy();
                    policy.ChangeMonitors.Add(monitor);
                    policy.RemovedCallback = CacheRemovedCallback;

                    var item = new CacheItem(key, data);
                    return item;
                }
            }
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
        }

        void Termination()
        {
            // Release the dependency.
            SqlDependency.Stop(CONNECTION_STRING);
        }
        public List<Employee> Select()
        {
            var key = Thread.CurrentPrincipal.Identity.Name;
            Thread.Sleep(1000);

            if (cache[key] == null)
            {
                CacheItemPolicy policy = null;
                var item = LoadData(out policy, key);
                cache.Set(item, policy);
            }
            return cache[key] as List<Employee>;
        }
    }
}
