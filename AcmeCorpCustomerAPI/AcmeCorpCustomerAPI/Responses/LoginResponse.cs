﻿namespace AcmeCorpCustomerAPI.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

    }
}
