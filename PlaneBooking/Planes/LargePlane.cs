using System;
using System.Collections.Generic;

namespace PlaneBooking.Planes
{
    public class LargePlane : Plane // Inherit from plane class
    {

        // Private variables to save values from inherited variables. They are mandatory to avoid an infinite setter loop resulting in an stack overflow exception
        private int _seatsNonSmoker;
        private int _seatsSmoker;
        public LargePlane() // Setting plane with default amount of seats
        {
            this.seatsNonSmoker = 50;
            this.seatsSmoker = 50;
        }

        public override int seatsSmoker { get => _seatsSmoker; set => _seatsSmoker = value; } // Getter and setter for inherited vars
        public override int seatsNonSmoker { get => _seatsNonSmoker; set => _seatsNonSmoker = value; } // Getter and setter for inherited vars

        public override List<Customer.Customer> Populate(List<Customer.Customer> passengers) // Populate the plane with inherited method
        {
            // Init needed vars
            int smokerCount = 0;
            int nonSmokerCount = 0;
            int nonSmokersOnSmokerSeat = 0;

            // Iterate through the passengers in reverse order. this way we can delete already seated passengers without messing up the count for i
            for (int i = (passengers.Count - 1); i >= 0; i--)
            {
                // If there are no seats left, end the loop and continue
                if (_seatsSmoker == 0 && _seatsNonSmoker == 0)
                    break;

                // Check if the passenger is smoking
                if (passengers[i].GetIsSmoking())
                {
                    // Check if there are smoker seats left
                    if (_seatsSmoker > 0)
                    {
                        // Update smoker count, decrease seats available and remove passenger
                        smokerCount++;
                        _seatsSmoker--;
                        passengers.RemoveAt(i);
                    }
                }
                else
                {
                    // Check if there are non-smoker seats left
                    if (_seatsNonSmoker > 0)
                    {
                        /*
                            Update non-smoker count, decrease seats available and remove passenger, also continue with the next iteration to avoid decreasing the available seats from 1 to 0 and therefor heading into the next loop
                            this would throw and index out of range exception since the passenger is already deleted but the iterator is still the same
                         */
                        nonSmokerCount++;
                        _seatsNonSmoker--;
                        passengers.RemoveAt(i);
                        continue;
                    }
                    // Check if there are any smoker seats left for a non-smoker passenger
                    if (_seatsNonSmoker == 0 && _seatsSmoker > 0)
                    {
                        // Update counts, decrease seats available and delete passenger
                        nonSmokersOnSmokerSeat++;
                        nonSmokerCount++;
                        _seatsSmoker--;
                        passengers.RemoveAt(i);
                    }
                }

            }

            // Display the results and return the remaining passengers
            Console.WriteLine($"The large plane is done populating. It contains {nonSmokerCount} non-smoking passengers and {smokerCount} smoking passengers.");
            if(nonSmokersOnSmokerSeat > 0)
            {
                Console.WriteLine($"In the large plane {nonSmokersOnSmokerSeat} non-smoking passengers have been seated on smoker seats");
            }
            return passengers;
        }
    }
}
