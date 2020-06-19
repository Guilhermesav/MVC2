using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2AT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Apresentação.ViewModels
{
    public class CidadeEstadoAggregateViewModel
    {
        public int Id { get; set; }

        public string NomeCidade { get; set; }
        public int População { get; set; }

        public string NomeEstado { get; set; }
        public int? EstadoEntityId { get; set; }
        public EstadoEntity? Estado { get; set; }

        public List<SelectListItem>? Estados { get; }

        public string? NomeDoEstado { get; set; }

        public string? Capital { get; set; }

        public string? SiglaEstado { get; set; }

        
        public int? PopulaçãoEstado { get; set; }

        public CidadeEstadoAggregateViewModel() { }

        public CidadeEstadoAggregateViewModel(IEnumerable<EstadoEntity> estados)
        {
            if (!(estados == null))
            {
                Estados = ToEstadoSelectListItem(estados);
            }
            
        }

        private static List<SelectListItem> ToEstadoSelectListItem(IEnumerable<EstadoEntity> estados)
        {
            return estados.Select(x => new SelectListItem
            { Text = $"{x.Nome} {x.Sigla}", Value = x.Id.ToString() }).ToList();
        }

        public CidadeEstadoAggregateEntity ToAggregateEntity()
        {
            var aggregateEntity = new CidadeEstadoAggregateEntity
            {
                CidadeEntity = new CidadeEntity
                {
                    Nome = NomeCidade,
                    População = População,
                    NomeEstado = NomeEstado,
                    Estado = Estado,
                    EstadoEntityId = EstadoEntityId ?? 0
                }
            };

            if (!PopulaçãoEstado.HasValue ||
                string.IsNullOrWhiteSpace(NomeDoEstado) ||
                string.IsNullOrWhiteSpace(SiglaEstado))
                return aggregateEntity;

            aggregateEntity.EstadoEntity = new EstadoEntity
            {
                Nome = NomeDoEstado,
                Capital = Capital,
                Sigla = SiglaEstado,
                População = PopulaçãoEstado.Value,
                Id = EstadoEntityId ?? 0
            };

            return aggregateEntity;
        }
    }
}
