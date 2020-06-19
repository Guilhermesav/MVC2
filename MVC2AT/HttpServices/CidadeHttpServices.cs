using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Interfaces.Services;
using MVC2AT.Dominio.Model.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVC2AT.HttpServices
{
    public class CidadeHttpService : ICidadeService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<EstadoHttpOptions> _estadoHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CidadeHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<EstadoHttpOptions> estadoHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _estadoHttpOptions = estadoHttpOptions ?? throw new ArgumentNullException(nameof(estadoHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory.CreateClient(estadoHttpOptions.CurrentValue.Name);
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

        public async Task<IEnumerable<CidadeEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_estadoHttpOptions.CurrentValue.CidadePath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CidadeEntity>>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
            }

            return null;
        }

        public async Task<CidadeEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.CidadePath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CidadeEntity>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
                new RedirectToActionResult("Livro", "Index", null);
            }

            return null;
        }

        public async Task InsertAsync(CidadeEstadoAggregateEntity cidadeEstadoAggregateEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_estadoHttpOptions.CurrentValue.CidadePath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(cidadeEstadoAggregateEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(CidadeEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.CidadePath}/{updatedEntity.Id}";

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
            var pathWithId = $"{_estadoHttpOptions.CurrentValue.CidadePath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

       
    }
}

