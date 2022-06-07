using FutApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FutApi.Services
{
    public class PlayersService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PlayersService> _logger;
        private readonly string _url; 

        public PlayersService(
            HttpClient httpClient,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ILogger<PlayersService> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _logger = logger;

            var futYear = configuration.GetValue<string>("FutYear");

            var cfgSection = configuration.GetSection(nameof(PlayersService));
            var token = cfgSection.GetValue<string>("Token");
            var jsonParameter = cfgSection.GetValue<string>("JsonParameter");

            _url = $"https://www.ea.com/fifa/ultimate-team/web-app/content/{token}/{futYear}/fut/items/web/players.json?_={jsonParameter}";
        }

        public async Task<PlayersContent> GetPlayersAsync()
        {
            var cachePlayers = _memoryCache.Get<PlayersContent>("GetPlayersAsync");
            if (cachePlayers != null)
            {
                _logger.LogInformation($"Getting GetPlayersAsync from cache. {cachePlayers.Players.Count} players");

                return cachePlayers;
            }

            var response = await _httpClient.GetAsync(_url);

            var json = await response.Content.ReadAsStringAsync();

            var players = JsonConvert.DeserializeObject<PlayersContent>(json);

            _memoryCache.Set<PlayersContent>("GetPlayersAsync", players);

            return players;
        }
    }
}
