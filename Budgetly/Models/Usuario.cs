using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Budgetly.Models;

namespace Budgetly.Models
{
    public class Usuario
    {
        public int Id {get; set;}
        [Required]
        public string? NombreUsuario {get;set;}
        [Required, EmailAddress]
        public string? CorreoElectronico {get; set;}
        [Required]
        public string? PassWordHash {get;set;}
        public string? Telefono {get; set;}
        public DateTime FechaRegistro {get; set;}
        public bool EsAutenticadoGoogle {get; set;}
        public ICollection<Sesion>? Sesiones {get; set;}
    }
}