using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgetly.Models.DTOs
{
    public class UsuarioUpdateDTO
    {
        public string? NombreUsuario { get; set; }
        public string? Telefono { get; set; }
        public bool? EsAutenticadoGoogle { get; set; }
    }
}