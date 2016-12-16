namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class itinerary_port
    {
        [Key]
        [Column(Order = 0)]
        public int itinerary_port_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itinerary_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int port_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? arrival_date_time { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? departure_date_time { get; set; }

        public virtual itinerary itinerary { get; set; }

        public virtual port port { get; set; }
    }

    public class itineraryPortViewModel
    {
        public int port_id { get; set; }

        public DateTime? arrival_date_time { get; set; }

        public DateTime? departure_date_time { get; set; }

        public string port_name { get; set; }

        public int itinerary_id { get; set; }

        public string itinerary_title { get; set; }

    }
}
