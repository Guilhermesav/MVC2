using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MVC2AT.Dominio.Model.Interfaces.Services;

namespace MVC.Apresentação.HttpServices
{
    public class EstadoHttpService : IEstadoService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<EstadoHttpOptions> _estadoHttpOptions;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EstadoHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<EstadoHttpOptions> estadoHttpOptions,
            SignInManager<IdentityUser> signInManager)
        {
            _estadoHttpOptions = estadoHttpOptions ?? throw new ArgumentNullException(nameof(estadoHttpOptions));
            _signInManager = signInManager;

            _httpClient = httpClientFactory?.CreateClient(estadoHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.Timeout = TimeSpan.FromMinutes(_estadoHttpOptions.CurrentValue.Timeout);
        }


        public async Task<IEnumerable<EstadoEntity>> GetAllAsync()
        {

            try
            {
                //exemplo recomendado com a nova API: System.Net.Http.Json
                var estados = await _httpClient.GetFromJsonAsync<List<EstadoEntity>>(_estadoHttpOptions.CurrentValue.EstadoPath);

                return estados;
            }
            catch (HttpRequestException e) when (e.Message.Contains("401"))
            {
                await _signInManager.SignOutAsync();
                return null;
            }
        }

        public async Task<EstadoEntity> GetByIdAsync(int id)
        {
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                //await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<EstadoEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }


        public async Task InsertAsync(EstadoEntity insertedEntity)
        {
           
            var uriPath = $"{_estadoHttpOptions.CurrentValue.EstadoPath}";

            //exemplo recomendado com a nova API: System.Net.Http.Json
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(uriPath, insertedEntity);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(EstadoEntity updatedEntity)
        {
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
