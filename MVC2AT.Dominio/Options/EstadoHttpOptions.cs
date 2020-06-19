using System;
using System.Collections.Generic;
using System.Text;

namespace MVC2AT.Dominio.Model.Options
{
    public class EstadoHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string CidadePath { get; set; }

        public string EstadoPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}
