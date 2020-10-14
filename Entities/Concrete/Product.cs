using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //default olarak bir class internal'dir
    //referansını verdiğim sürece, diğer library den erişebiliriz, 
    //internal, entity altında tanımlanır
    //protected inherit ettiği yerde o özelliği kullanabiliriz
    //private, sadece tanımlandığı yerde kullanılır..
    public class Product:IEntity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
    }

    
}
