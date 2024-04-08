using System;

namespace LegacyApp
{
    public enum ClientType
    {
        ImportantClient,
        NormalClient,
        VeryImportantClient
    }

    public class Client
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ClientType Type { get; set; }

        public Client(string name, int clientId, string email, string address, ClientType type)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            // Additional validation as needed

            Name = name;
            ClientId = clientId;
            Email = email;
            Address = address;
            Type = type;
        }

        // Methods to interact with the object if needed
    }
}