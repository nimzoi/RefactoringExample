using System;

namespace LegacyApp
{    public interface IUserCreditService
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }

    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;

        // Parameterless constructor for backward compatibility
        public UserService()
        {
            // Direct instantiation of dependencies, not ideal but necessary for backward compatibility
            _clientRepository = new ClientRepository(); // Assuming a default implementation exists
            _userCreditService = new UserCreditService(); // Assuming a default implementation exists
        }

        // Constructor with dependency injection for better design
        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _userCreditService = userCreditService ?? throw new ArgumentNullException(nameof(userCreditService));
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUserInput(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }
    
            var client = _clientRepository.GetById(clientId);
            (var hasCreditLimit, var creditLimit) = DetermineCreditDetails(lastName, dateOfBirth, client);

            if (hasCreditLimit && creditLimit < 500)
            {
                return false;
            }

            var user = new User(firstName, lastName, email, dateOfBirth, client, hasCreditLimit, creditLimit);
            UserDataAccess.AddUser(user);
            return true;
        }
        private bool IsValidUserInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return !string.IsNullOrEmpty(firstName) && 
                   !string.IsNullOrEmpty(lastName) && 
                   IsValidEmail(email) && 
                   !IsUnderage(dateOfBirth);
        }
        private (bool hasCreditLimit, int creditLimit) DetermineCreditDetails(string lastName, DateTime dateOfBirth, Client client)
        {
            if (client.Type == ClientType.VeryImportantClient)
            {
                return (false, 0);
            }

            int creditLimit = _userCreditService.GetCreditLimit(lastName, dateOfBirth);
            if (client.Type == ClientType.ImportantClient)
            {
                creditLimit *= 2;
            }

            return (true, creditLimit);
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsUnderage(DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth) < 21;
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }
    }
}