using System;
using System.Collections.Generic;

namespace PlaneBooking.Planes
{
    public abstract class Plane // Abstract class to inherit from, this class could be expanded in the future if needed
    {
        // Declare variables
        public abstract int seatsSmoker { get; set; }
        public abstract int seatsNonSmoker { get; set; }

        // Declare methods
        public abstract List<Customer.Customer> Populate(List<Customer.Customer> passengers);
    }
}
