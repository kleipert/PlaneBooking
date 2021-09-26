using System;


namespace PlaneBooking.Customer
{
    public class Customer
    {
        private bool isSmoking;

        public Customer() // adjusted the default constructor to set the isSmoking value based on a random double value
        {
            Random rnd = new Random();
            if (rnd.NextDouble() > 0.5f)
                isSmoking = true;
            else
                isSmoking = false;
        }

        public Customer(bool isSmoker) // provided an additional contructor to create passengers as they are needed
        {
            this.isSmoking = isSmoker;
        }

        public bool GetIsSmoking() // since isSmoking is private, we need a method the retrieve its value
        {
            return isSmoking;
        }
    }
}
