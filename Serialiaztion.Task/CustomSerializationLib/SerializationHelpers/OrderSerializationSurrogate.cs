using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using NorthwindLibrary;

namespace CustomSerializationLib.SerializationHelpers
{
    internal class OrderSerializationSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            if (type==typeof(IEnumerable<Order>))
                return typeof(IEnumerable<OrderSurrogate>);
            return type;
        }
        public object GetObjectToSerialize(object obj, Type targetType)
        {

            List<Order> orders = obj as List<Order>;
            if (orders != null)
            {
                List<OrderSurrogate> surrogatedOrders = new List<OrderSurrogate>();
                foreach (var o in orders)
                {
                    surrogatedOrders.Add(new OrderSurrogate()
                    {
                        OrderID = o.OrderID,
                        //CustomerID = o.CustomerID,
                        //EmployeeID = o.EmployeeID,
                        //OrderDate = o.OrderDate,
                        //RequiredDate = o.RequiredDate,
                        //ShippedDate = o.ShippedDate,
                        //ShipVia = o.ShipVia,
                        //Freight = o.Freight,
                        //ShipName = o.ShipName,
                        //ShipAddress = o.ShipAddress,
                        //ShipCity = o.ShipCity,
                        //ShipRegion = o.ShipRegion,
                        //ShipPostalCode = o.ShipPostalCode,
                        //ShipCountry = o.ShipCountry,

                        //Customer = new Customer()
                        //{
                        //    CustomerID = o.Customer.CustomerID,
                        //    CompanyName = o.Customer.CompanyName,
                        //    ContactName = o.Customer.ContactName,
                        //    ContactTitle = o.Customer.ContactTitle,
                        //    Address = o.Customer.Address,
                        //    City = o.Customer.City,
                        //    Region = o.Customer.Region,
                        //    PostalCode = o.Customer.PostalCode,
                        //    Country = o.Customer.Country,
                        //    Phone = o.Customer.Phone,
                        //    Fax = o.Customer.Fax
                        //},
                        //Employee = new Employee()
                        //{
                        //    EmployeeID = o.Employee.EmployeeID,
                        //    LastName = o.Employee.LastName,
                        //    FirstName = o.Employee.FirstName,
                        //    Title = o.Employee.Title,
                        //    TitleOfCourtesy = o.Employee.TitleOfCourtesy,
                        //    BirthDate = o.Employee.BirthDate,
                        //    HireDate = o.Employee.HireDate,
                        //    Address = o.Employee.Address,
                        //    City = o.Employee.City,
                        //    Region = o.Employee.Region,
                        //    PostalCode = o.Employee.PostalCode,
                        //    Country = o.Employee.Country,
                        //    HomePhone = o.Employee.HomePhone,
                        //    Extension = o.Employee.Extension,
                        //    Photo = o.Employee.Photo,
                        //    Notes = o.Employee.Notes,
                        //    ReportsTo = o.Employee.ReportsTo,
                        //    PhotoPath = o.Employee.PhotoPath
                        //},
                        //Shipper = new Shipper()
                        //{
                        //    ShipperID = o.Shipper.ShipperID,
                        //    CompanyName = o.Shipper.CompanyName,
                        //    Phone = o.Shipper.Phone
                        //},

                        //Order_Details = o.Order_Details
                    });
                }
                return surrogatedOrders;
            }
            return null;
        }
        public object GetDeserializedObject(object obj, Type targetType)
        {
            throw new NotImplementedException();
        }
        #region NotImplemented
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }



        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
#endregion
    }
}
