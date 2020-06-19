using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2AT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC2AT.ViewModels
{
    public class CidadeEstadoAggViewModel
    {
        public int Id { get; set; }

        public string NomeCidade { get; set; }

        public int Populacao { get; set; }

        public int? EstadoEntityId { get; set; }
#nullable enable
        public EstadoEntity? Estado { get; set; }
        #nullable enable
        public List<SelectListItem>? Estados { get; }

        public string? NomeEstado { get; set; }

        public string? Capital { get; set; }

        public string? Sigla { get; set; }

        public int PopulaçãoEstado { get; set; }

        public CidadeEstadoAggViewModel() { }

        public CidadeEstadoAggViewModel(IEnumerable<EstadoEntity> estados)
        {
            Estados = ToEstadoSelectListItem(estados);
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
                    População = Populacao,
                    EstadoEntityId = EstadoEntityId ?? 0
                }
            };

            if (string.IsNullOrWhiteSpace(NomeEstado) ||
                string.IsNullOrWhiteSpace(Sigla) ||
                string.IsNullOrWhiteSpace(Capital))
                return aggregateEntity;

            aggregateEntity.EstadoEntity = new EstadoEntity
            {
                Nome = NomeEstado,
                Capital = Capital,
                Sigla = Sigla,
                População = PopulaçãoEstado,
                Id = EstadoEntityId ?? 0
            };

            return aggregateEntity;
        }
    }
}
