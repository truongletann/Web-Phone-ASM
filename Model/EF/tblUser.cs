namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            tblOrders = new HashSet<tblOrder>();
        }

        [Key]
        [StringLength(50)]
        public string userID { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [StringLength(10)]
        public string roleID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder> tblOrders { get; set; }

        public virtual tblRole tblRole { get; set; }
    }
}
