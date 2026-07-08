using System;
using System.Collections.Generic;
using System.Text;

namespace XarajatApp.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
