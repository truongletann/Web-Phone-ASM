namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProduct")]
    public partial class tblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProduct()
        {
            tblOrderDetails = new HashSet<tblOrderDetail>();
        }

        [Key]
        public int phoneID { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public int quantity { get; set; }

        public double price { get; set; }

        public int categoryID { get; set; }

       
        public string imagePath { get; set; }

        public bool? status { get; set; }

        public virtual tblCategory tblCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrderDetail> tblOrderDetails { get; set; }
    }
}
