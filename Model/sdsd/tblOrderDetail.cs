namespace Model.sdsd
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblOrderDetail
    {
        [Key]
        public int detailID { get; set; }

        public int orderID { get; set; }

        public int phoneID { get; set; }

        public int quantity { get; set; }

        public double price { get; set; }

        public virtual tblOrder tblOrder { get; set; }

        public virtual tblProduct tblProduct { get; set; }
    }
}
