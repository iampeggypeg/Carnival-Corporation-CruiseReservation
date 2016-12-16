namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("itinerary")]
    public partial class itinerary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public itinerary()
        {
            customer_itinerary = new HashSet<customer_itinerary>();
            itinerary_port = new HashSet<itinerary_port>();
            ship = new HashSet<ship>();
        }

        [Key]
        public int itinerary_id { get; set; }

        [Required]
        [StringLength(50)]
        public string itinerary_title { get; set; }

        [Column(TypeName = "date")]
        public DateTime start_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime end_date { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal cruise_price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customer_itinerary> customer_itinerary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itinerary_port> itinerary_port { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ship> ship { get; set; }
    }

    public class itineraryViewModel
    {
        public int itinerary_id { get; set; }

        public string itinerary_title { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public decimal cruise_price { get; set; }

        public string ship_name { get; set; }
    }

    public class itineraryDetailsViewModel
    {
        public int itinerary_id { get; set; }

        public string itinerary_title { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public decimal cruise_price { get; set; }

        public string ship_name { get; set; }

        public int duration { get; set; }
    }
}
