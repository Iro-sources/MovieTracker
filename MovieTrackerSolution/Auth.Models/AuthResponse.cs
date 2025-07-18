﻿namespace Auth.Models
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
