namespace NorthwindLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Serializable]
    public partial class Category:ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();

        }

        public Category(SerializationInfo info, StreamingContext context)
        {
            CategoryID = info.GetInt32("CategoryID");
            CategoryName = info.GetString("CategoryName");
            Description = info.GetString("Description");
            Picture = (byte[])info.GetValue("Picture", typeof(byte[]));
            Products = (ICollection<Product>)info.GetValue("Products", typeof(ICollection<Product>));
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("OnSerializing");
        }

        [OnSerialized]
        private void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("OnSerialized");
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("OnDeserializing");
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("OnDeserialized");
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
            info.AddValue("CategoryID", CategoryID);
            info.AddValue("CategoryName", CategoryName);
            info.AddValue("Description", Description);
            info.AddValue("Picture", Picture);
            info.AddValue("Products", Products);
        }
    }
}
