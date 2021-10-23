using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class ProductionHouse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Producer> Producers { get; set; }

        public ProductionHouse(int iD, string name, string phoneNumber, string email, List<Producer> producers)
        {
            ID = iD;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Producers = producers;
        }

        public ProductionHouse(string name, string phoneNumber, string email, List<Producer> producers)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Producers = producers;
        }
    }
}
