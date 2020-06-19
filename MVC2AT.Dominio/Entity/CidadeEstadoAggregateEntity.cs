using System;
using System.Collections.Generic;
using System.Text;

namespace MVC2AT.Dominio.Model.Entity
{
    public class CidadeEstadoAggregateEntity
    {
        public CidadeEntity CidadeEntity { get; set; }

        public EstadoEntity EstadoEntity { get; set; }
    }
}
