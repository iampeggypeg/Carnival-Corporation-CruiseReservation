namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("destination")]
    public partial class destination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public destination()
        {
            port = new HashSet<port>();
        }

        [Key]
        public int destination_id { get; set; }

        [Required]
        [StringLength(30)]
        public string destination_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<port> port { get; set; }
    }

    public class destinationViewModel
    {
        public int destination_id { get; set; }

        public string destination_name { get; set; }
    }
}
