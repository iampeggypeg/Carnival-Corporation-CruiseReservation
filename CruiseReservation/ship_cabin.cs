namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ship_cabin
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ship_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cabin_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int no_of_cabin { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maximum_occupant { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? price { get; set; }

        public virtual cabin cabin { get; set; }

        public virtual ship ship { get; set; }
    }

    public class shipCabinViewModel
    {

        public int ship_id { get; set; }

        public string ship_name { get; set; }

        public int cabin_id { get; set; }

        public string cabin_name { get; set; }

        public int no_of_cabin { get; set; }

        public int maximum_occupant { get; set; }

        public decimal? price { get; set; }

        public int itinerary_id { get; set; }
        public string itinerary_title { get; set; }

        public DateTime itinerary_date { get; set; }
    }
}
