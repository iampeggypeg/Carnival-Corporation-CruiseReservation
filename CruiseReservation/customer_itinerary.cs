namespace CruiseReservation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customer_itinerary
    {
        [Key]
        public int customer_itinerary_id { get; set; }

        public string customer_id { get; set; }

        public int itinerary_id { get; set; }

        public int no_of_pax { get; set; }

        public int cabin_id { get; set; }

        public virtual cabin cabin { get; set; }

        public virtual itinerary itinerary { get; set; }
    }

    public class customerItineraryViewModel
    {
        public int itinerary_id { get; set; }
        public string itinerary_title { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public int cabin_id { get; set; }
        public string cabin { get; set; }

        public decimal? tour_price { get; set; }

        public decimal? cabin_price { get; set; }

        public string ship { get; set; }

        public string cruise { get; set; }

    }

    public class customerItineraryConfirmModel
    {
        public string customer_id { get; set; }

        public int itinerary_id { get; set; }

        public int no_of_pax { get; set; }

        public int cabin_id { get; set; }
    }

    public class customerItineraryBookingDetailsModel
    {
        public int itinerary_id { get; set; }

        public string itinerary_title { get; set; }

        public int no_of_pax { get; set; }

        public int cabin_id { get; set; }

        public string cabin { get; set; }
    }

}
