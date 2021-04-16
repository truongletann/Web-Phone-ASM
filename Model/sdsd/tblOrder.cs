namespace Model.sdsd
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOrder()
        {
            tblOrderDetails = new HashSet<tblOrderDetail>();
        }

        [Key]
        public int orderID { get; set; }

        [Required]
        [StringLength(50)]
        public string userID { get; set; }

        public double total { get; set; }

        public DateTime? dateBuy { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrderDetail> tblOrderDetails { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}
