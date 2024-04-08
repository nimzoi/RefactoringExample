using System;

namespace LegacyApp
{
    public class User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int ClientId { get; private set; }
        public bool HasCreditLimit { get; private set; }
        public int CreditLimit { get; private set; }
        public Client Client { get; private set; }

        public User(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, Client client, bool hasCreditLimit, int creditLimit)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            if (string.IsNullOrEmpty(emailAddress) || !emailAddress.Contains("@")) throw new ArgumentException("Email address is not valid.", nameof(emailAddress));
    
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
            Client = client;
            ClientId = client.ClientId;
            HasCreditLimit = hasCreditLimit;
            CreditLimit = creditLimit;
        }


    }
}
