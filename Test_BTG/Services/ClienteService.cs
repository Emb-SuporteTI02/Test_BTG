using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_BTG.Model;

namespace Test_BTG.Services
{
    public class ClienteService
    {
        public List<Clientes> GetClientes()
        {
            return new List<Clientes>
            {
                new Clientes { Name = "João", Lastname = "Silva", Age = 30, Address = "Rua A" },
                new Clientes { Name = "Maria", Lastname = "Souza", Age = 25, Address = "Rua B" }
            };
        }
    }
}
