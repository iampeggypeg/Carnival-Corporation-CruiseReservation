namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cabin")]
    public partial class cabin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cabin()
        {
            customer_itinerary = new HashSet<customer_itinerary>();
            ship_cabin = new HashSet<ship_cabin>();
        }

        [Key]
        public int cabin_id { get; set; }

        [Required]
        [StringLength(10)]
        public string cabin_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customer_itinerary> customer_itinerary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ship_cabin> ship_cabin { get; set; }
    }
}
