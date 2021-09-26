using System;
using System.Collections.Generic;
using System.Linq;
using PlaneBooking.Customer;
using PlaneBooking.Planes;

namespace MainApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSimulation(GenerateRandomPassengers()); // Main Task --> runs simulation with random amount of smokers and non-smokers in random order

            //RunPresetRandom(30, 120); // runs simulation with set amount of smokers(30) and non-smokers(120) in a random order

            //RunPresetSmokersFirst(60, 90); // runs simulation with set amount of smokers(60) and non-smokers(90) where smokers get their seat first

            //RunPresetNonSmokersFirst(60,90); // runs simulation with set amount of smokers(60) and non-smokers(90) where non-smokers get their seat first

            //RunPresetAllSmokers(); // runs simulation with 150 smokers only

            //RunPresetAllNonSmokers(); // runs simulation with 150 non-smokers only
        }

        #region Methods

        private static void RunSimulation(List<Customer> passengerList) // Runs the simulation with the given List of Passengers
        {
            // init needed variables
            List<Customer> passengers = passengerList;
            List<Plane> planes = new List<Plane>();

            int smokers = 0;
            int nonSmokers = 0;

            int passengersLeft = 0;
            int smokersLeft = 0;

            // Add planes in the correct order to the list 
            planes.Add(new LargePlane());
            planes.Add(new SmallPlane());

            // iterate through the passenger list and count smokers and non-smokers
            foreach (Customer passenger in passengers)
            {
                if (passenger.GetIsSmoking())
                    smokers++;
                else
                    nonSmokers++;
            }
            // display the amount of smokers and non-smokers
            Console.WriteLine($"Passengers generated. There are {nonSmokers} non-smokers and {smokers} smokers");

            // populate the planes in the correct order
            foreach (Plane plane in planes)
            {
                plane.Populate(passengers);
            }

            // check if there are any passengers left and count them
            if (passengers.Count > 0)
            {
                foreach (Customer passenger in passengers)
                {
                    if (passenger.GetIsSmoking())
                    {
                        passengersLeft++;
                        smokersLeft++;
                    }
                    else
                        passengersLeft++;
                }

                // display information about the remaining passengers if there are any passengers left
                if(passengersLeft > 0)
                {
                    if (passengersLeft == smokersLeft)
                        Console.WriteLine($"Not all Passengers have got a seat. There are {smokersLeft} smokers left");

                    if (passengersLeft > smokersLeft)
                        Console.WriteLine($"Not all Passengers have got a seat. There are {smokersLeft} smokers and {passengersLeft - smokersLeft} non-smokers left");
                }
            }
            // wait for input to keep console window open
            Console.ReadKey();
        }

        private static List<Customer> GenerateRandomPassengers() // Generates 150 random passengers
        {
            List<Customer> passengers = new List<Customer>();
            for (int i = 0; i < 150; i++)
            {
                passengers.Add(new Customer());
            }

            return passengers;
        }

        private static List<Customer> GeneratePassengersNonSmokersFist(int nonSmokers, int smokers) // Generate a given amount of passengers and order them
        {
            List<Customer> passengers = new List<Customer>();
            
            // Adding smokers first, since i am iterating through the list in reverse later
            for (int i = 0; i < smokers; i++)
            {
                passengers.Add(new Customer(true));
            }
            // Adding non-smokers last, since i am iterating through the list in reverse later
            for (int i = 0; i < nonSmokers; i++)
            {
                passengers.Add(new Customer(false));
            }

            return passengers;
        }

        private static List<Customer> GeneratePassengersSmokersFist(int nonSmokers, int smokers) // Generate a given amount of passengers and order them
        {
            List<Customer> passengers = new List<Customer>();
            
            // Adding non-smokers first, since i am iterating through the list in reverse later
            for (int i = 0; i < nonSmokers; i++)
            {
                passengers.Add(new Customer(false));
            }
            // Adding smokers last, since i am iterating through the list in reverse later
            for (int i = 0; i < smokers; i++)
            {
                passengers.Add(new Customer(true));
            }

            return passengers;
        }

        #endregion

        #region Debugging_Testing_Methods

        // This region defines all methods i implemented to test a variety of testcases and edge/extreme situations
        // The function is explained in the above main method

        private static void RunPresetRandom(int smokers, int nonSmokers) 
        {
            List<Customer> passengers = GeneratePassengersSmokersFist(nonSmokers, smokers).OrderBy(passenger => Guid.NewGuid()).ToList();
            RunSimulation(passengers);

        }

        private static void RunPresetSmokersFirst(int smokers, int nonSmokers)
        {
            List<Customer> passengers = GeneratePassengersSmokersFist(nonSmokers, smokers);
            RunSimulation(passengers);
        }

        private static void RunPresetNonSmokersFirst(int smokers, int nonSmokers)
        {
            List<Customer> passengers = GeneratePassengersNonSmokersFist(nonSmokers, smokers);
            RunSimulation(passengers);
        }

        private static void RunPresetAllSmokers()
        {
            List<Customer> passengers = GeneratePassengersSmokersFist(0,150); 
            RunSimulation(passengers);
        }

        private static void RunPresetAllNonSmokers()
        {
            List<Customer> passengers = GeneratePassengersSmokersFist(150, 0);
            RunSimulation(passengers);
        }



        #endregion


    }
}
