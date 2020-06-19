using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2AT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Apresentação.ViewModels
{
    public class CidadeViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeEstado { get; set; }

        public int População { get; set; }
        
        public int EstadoEntityId { get; set; }

        public EstadoEntity Estado { get; set; }

        public List<SelectListItem> Estados { get; }

        public CidadeViewModel() { }

        public CidadeViewModel(CidadeEntity cidadeEntity)
        {
            Nome = cidadeEntity.Nome;
            População = cidadeEntity.População;
            NomeEstado = cidadeEntity.NomeEstado;
            EstadoEntityId = cidadeEntity.EstadoEntityId;
            Estado = cidadeEntity.Estado;
                
        }
        public CidadeViewModel(CidadeEntity cidadeEntity, IEnumerable<EstadoEntity> estados) : this(cidadeEntity)
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
            { Text = $"{x.Nome} {x.Sigla}", Value = x.Id.ToString() }).ToList();
        }
    }
}
