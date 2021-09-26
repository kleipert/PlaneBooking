using System;
using System.Collections.Generic;

namespace PlaneBooking.Planes
{
    public class SmallPlane : Plane // Inherit from plane class
    {
        // Private variables to save values from inherited variables. They are mandatory to avoid an infinite setter loop resulting in an stack overflow exception
        private int _seatsNonSmoker;
        private int _seatsSmoker;
        public SmallPlane() // Setting plane with default amount of seats
        {
            this.seatsSmoker = 0;
            this.seatsNonSmoker = 50;
        }

        public override int seatsNonSmoker { get => _seatsNonSmoker; set => _seatsNonSmoker = value; } // Getter and setter for inherited vars
        public override int seatsSmoker { get => _seatsSmoker; set => _seatsSmoker = value; } // Getter and setter for inherited vars

        public override List<Customer.Customer> Populate(List<Customer.Customer> passengers) // Populate the plane with inherited method
        {
            // Init needed vars
            int nonSmokerCount = 0;

            // Iterate through the passengers in reverse order. this way we can delete already seated passengers without messing up the count for i
            for (int i = passengers.Count - 1; i >= 0; i--)
            {
                // Stop the loop when there are no seats left
                if (_seatsNonSmoker == 0)
                    break;

                // Check if passenger is not smoking
                if (!passengers[i].GetIsSmoking())
                {
                    // Update count, decrease available seats and remove passenger
                    nonSmokerCount++;
                    _seatsNonSmoker--;
                    passengers.RemoveAt(i);
                    
                }

            }


            // Display the result and return the remaining passengers
            Console.WriteLine($"The small plane is done populating. It contains {nonSmokerCount} non-smoking passengers.");

            if(_seatsNonSmoker > 0)
                Console.WriteLine($"In the small plane {_seatsNonSmoker} seats remain empty since there are no non smokers left.");

            return passengers;
        }
    }
}
