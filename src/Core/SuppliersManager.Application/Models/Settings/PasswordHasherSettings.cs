using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppliersManager.Application.Models.Settings
{
    public class PasswordHasherSettings : IPasswordHasherSettings
    {
        public string Pepper { get; set; } = "Jf74kwSDNGGmJvYe";
        public int Iteration { get; set; } = 5;
    }
}
