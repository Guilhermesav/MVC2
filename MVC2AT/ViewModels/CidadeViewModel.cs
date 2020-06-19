using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2AT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC2AT.ViewModels
{
    public class CidadeViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Populacao { get; set; }

        public int EstadoEntityId { get; set; }
        public EstadoEntity Estado { get; set; }

        public List<SelectListItem> Estados { get; }

        public CidadeViewModel() { }

        public CidadeViewModel(CidadeEntity cidadeEntity)
        {
            Nome = cidadeEntity.Nome;
            Populacao = cidadeEntity.População;
            EstadoEntityId = cidadeEntity.EstadoEntityId;
            Estado = cidadeEntity.Estado;

        }
        public CidadeViewModel(CidadeEntity cidade, IEnumerable<EstadoEntity> estados) : this(cidade)
        {
            Estados = ToEstadoSelectListItem(estados);
        }

        public CidadeViewModel(IEnumerable<EstadoEntity> estados)
        {
            Estados = ToEstadoSelectListItem(estados);
        }

        private static List<SelectListItem> ToEstadoSelectListItem(IEnumerable<EstadoEntity> estados)
        {
            return estados.Select(x => new SelectListItem
            { Text = $"{x.Nome}", Value = x.Id.ToString() }).ToList();
        }
    }
}
