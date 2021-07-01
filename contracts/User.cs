using System;

namespace CommunAxiom.Transformations.Contracts
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime LastConnected { get; set; }
    }
}
