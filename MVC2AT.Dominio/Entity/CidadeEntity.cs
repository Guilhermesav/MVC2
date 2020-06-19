using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC2AT.Dominio.Model.Entity
{
    public class CidadeEntity
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string NomeEstado { get; set; }

        public int População { get; set; }
        
        public int EstadoEntityId { get; set; }

        public EstadoEntity Estado { get; set; }

        public List<SelectListItem> Estados { get; }
    }
}
