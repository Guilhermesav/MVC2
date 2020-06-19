using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MVC2AT.Dominio.Model.Entity
{
    public class EstadoEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public string Capital { get; set; }

        public string Sigla { get; set; }

        public int População { get; set; }
        
        public List<CidadeEntity> Cidades { get; set; }
    }
}
