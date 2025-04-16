using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgetly.Models
{
    public class Sesion
    {
        public int Id {get; set;}
        public int UsuarioId {get; set;}
        public DateTime FechaIngreso{get; set;}
        public int? DuracionSesion{get; set;}
    }
}