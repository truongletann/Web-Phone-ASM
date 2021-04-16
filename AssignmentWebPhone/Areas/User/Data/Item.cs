using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;

namespace AssignmentWebPhone.Areas.User.Data
{
    public class Item
    {
        public tblProduct Product { get; set; }
        public int Quantity { get; set; }

        public Item(tblProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
