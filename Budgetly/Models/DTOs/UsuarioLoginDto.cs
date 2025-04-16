using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgetly.Models.DTOs
{
    public class UsuarioLoginDto
    {
        public string? CorreoElectronico { get; set; }
        public string? Clave { get; set; }
    }
}