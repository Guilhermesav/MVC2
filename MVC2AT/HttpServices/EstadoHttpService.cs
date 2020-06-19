using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;

namespace MVC2AT.HttpServices
{
    public class EstadoHttpService : IEstadoHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<EstadoHttpOptions> _estadoHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EstadoHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<EstadoHttpOptions> estadoHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _estadoHttpOptions = estadoHttpOptions ?? throw new ArgumentNullException(nameof(estadoHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;

            _httpClient = httpClientFactory?.CreateClient(estadoHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.Timeout = TimeSpan.FromMinutes(_estadoHttpOptions.CurrentValue.Timeout);
        }

        private async Task<bool> AddAuthJwtToRequest()
        {
            var jwtCookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("estadoToken", out var jwtFromCookie);
            if (!jwtCookieExists)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtFromCookie);
            return true;
        }

        public async Task<IEnumerable<EstadoEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }

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
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                //await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<EstadoEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetByIdHttpAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            return httpResponseMessage;
        }

        public async Task InsertAsync(EstadoEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
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
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
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
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.EstadoPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
