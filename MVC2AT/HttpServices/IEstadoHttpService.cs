using MVC2AT.Dominio.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC2AT.HttpServices
{
    public interface IEstadoHttpService : IEstadoService
    {
        Task<HttpResponseMessage> GetByIdHttpAsync(int id);
    }
}
