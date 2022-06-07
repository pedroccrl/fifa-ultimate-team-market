using FutApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FutApi.Services
{
    public class FutService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FutService> _logger;
        private string _sidToken;

        public FutService(
            HttpClient httpClient,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ILogger<FutService> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _logger = logger;
        }

        string Token => _configuration.GetValue<string>("Token");
        string SidToken => _sidToken;
        
        public void SetSidToken(string token)
        {
            _sidToken = token;

            if (!_httpClient.DefaultRequestHeaders.Contains("X-UT-SID"))
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-UT-SID", SidToken);
        }

        public async Task<PlayersContent> GetPlayersAsync()
        {
            var cachePlayers = _memoryCache.Get<PlayersContent>("GetPlayersAsync");
            if (cachePlayers != null)
            {
                _logger.LogInformation($"Getting GetPlayersAsync from cache. {cachePlayers.Players.Count} players");
                
                return cachePlayers;
            }

            var response = await _httpClient.GetAsync($"https://www.ea.com/fifa/ultimate-team/web-app/content/{Token}/2022/fut/items/web/players.json?_=22249");

            var json = await response.Content.ReadAsStringAsync();

            var players = JsonConvert.DeserializeObject<PlayersContent>(json);

            _memoryCache.Set<PlayersContent>("GetPlayersAsync", players);

            return players;
        }

        public async Task<List<ItemDatum>> GetClubPlayersAsync(int start = 0)
        {
            var cacheItems = _memoryCache.Get<List<ItemDatum>>("GetClubPlayersAsync");
            if (cacheItems != null)
            {
                _logger.LogInformation($"Getting GetClubPlayersAsync from cache. {cacheItems.Count} players");

                return cacheItems;
            }

            var items = new List<ItemDatum>();

            var count = 1;

            for (start = 0; count > 0; start += 91)
            {
                var request = new
                {
                    type = "player",
                    start,
                    count = 91,
                    sort = "desc",
                    sortBy = "value",
                    isUntradeable = "false",
                    excldef = "167948,84117361,164240,216267,50558438,209331,84107443,50518590,208722,67350066,20801,237692,212188,195864,50540069,192505,231677,205452,232488,0,200145,0,205498"
                };

                var response = await _httpClient.PostAsJsonAsync($"https://utas.external.s2.fut.ea.com/ut/game/fifa22/club", request);

                var json = await response.Content.ReadAsStringAsync();

                var players = JsonConvert.DeserializeObject<ClubPlayers>(json);
                if (players.ItemData.Count == 0)
                    break;

                items.AddRange(players.ItemData);
                count = players.ItemData.Count;
            }

            if (items.Count > 0)
                _memoryCache.Set("GetClubPlayersAsync", items, TimeSpan.FromSeconds(10 * 60));

            return items;
        }

        public async Task<List<AuctionInfo>> GetTransferListAsync(long assedId, long maxBid)
        {
            var cacheKey = $"GetTransferListAsync_{assedId}_{maxBid}";

            var cacheTransfer = _memoryCache.Get<List<AuctionInfo>>(cacheKey);
            if (cacheTransfer != null)
                return cacheTransfer;

            var transfer = new List<AuctionInfo>();

            var count = 1;
            for (int start = 0; count > 0; start += 21)
            {
                if (start > 3 * 21)
                    break;

                var query = $"num=21&start={start}&type=player&maskedDefId={assedId}&maxb={maxBid}";

                var url = $"https://utas.external.s2.fut.ea.com/ut/game/fifa22/transfermarket?{query}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    break;

                var json = await response.Content.ReadAsStringAsync();

                var transferList = JsonConvert.DeserializeObject<TransferList>(json);
                if (transferList.AuctionInfo.Count == 0)
                {
                    if (transfer.Count == 0)
                    {

                    }
                    break;

                }

                transfer.AddRange(transferList.AuctionInfo);
                count = transferList.AuctionInfo.Count;
            }

            if (transfer.Count > 0)
                _memoryCache.Set(cacheKey, transfer, TimeSpan.FromSeconds(60));

            return transfer;
        }

        public async Task<bool> AtualizarJogadorTransferencia(long id)
        {
            var request = new
            {
                itemData = new[] { new { id, pile = "trade" } }
            };

            var response = await _httpClient.PutAsJsonAsync($"https://utas.external.s2.fut.ea.com/ut/game/fifa22/item", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VenderJogador(long id, long buyNowPrice, long startingBid, long duration = 3600)
        {
            if (startingBid < 650)
                startingBid = 650;

            if (buyNowPrice <= startingBid)
                buyNowPrice = startingBid + 50;

            if (buyNowPrice > 10000)
                startingBid = buyNowPrice - 250;

            var request = new
            {
                itemData = new
                {
                    id
                },
                startingBid,
                duration,
                buyNowPrice
            };

            var transferiu = await AtualizarJogadorTransferencia(id);
            if (!transferiu)
                return false;

            var cacheItems = _memoryCache.Get<List<ItemDatum>>("GetClubPlayersAsync");
            if (cacheItems != null)
            {
                var removed = cacheItems.RemoveAll(x => x.Id == id);
                _logger.LogInformation($"Removendo jogador {id} da lista");

                _memoryCache.Set("GetClubPlayersAsync", cacheItems, TimeSpan.FromSeconds(10 * 60));
            }

            var response = await _httpClient.PostAsJsonAsync($"https://utas.external.s2.fut.ea.com/ut/game/fifa22/auctionhouse", request);

            return response.IsSuccessStatusCode;
        }
    }
}
