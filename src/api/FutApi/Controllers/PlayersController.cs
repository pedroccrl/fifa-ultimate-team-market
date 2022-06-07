﻿using FutApi.Models;
using FutApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace FutApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly FutService _futService;

        public PlayersController(
            IConfiguration configuration,
            FutService futService)
        {
            _configuration = configuration;
            _futService = futService;
        }

        [HttpGet]
        public async Task<PlayersSellResponse> Get(
            string token,  
            [Required]
            long discardValue,
            int total = 20)
        {
            _futService.SetSidToken(token);

            var club = await _futService.GetClubPlayersAsync();
            var players = await _futService.GetPlayersAsync();

            var playersSell = new List<PlayerSell>();

            var clubPlayers = club
                .Where(x => x.Untradeable == false && x.DiscardValue >= discardValue)
                .Take(total)
                .OrderByDescending(x => x.DiscardValue)
                .ToList();

            foreach (var player in clubPlayers)
            {
                var playerAsset = players.Players.FirstOrDefault(x => x.AssetId == player.AssetId);
                if (playerAsset == null)
                    continue;

                var market = await _futService.GetTransferListAsync(player.AssetId, 150000);

                var sell = new PlayerSell
                {
                    AssetId = player.AssetId,
                    Id = player.Id,
                    Nome = playerAsset.Apelido ?? playerAsset.LastName,
                    Rating = player.Rating,
                    ValorVenda = player.DiscardValue,
                };

                if (market.Count > 0)
                {
                    var minMarket = market.Min(x => x.BuyNowPrice);
                    sell.MenorPrecoMercado = minMarket;
                    sell.Vendeu = await _futService.VenderJogador(player.Id, minMarket, minMarket - 100);
                }
                else
                {
                    //sell.Vendeu = await _futService.VenderJogador(player.Id, 700, 650);
                }

                playersSell.Add(sell);
            }

            return new PlayersSellResponse
            {
                Players = playersSell,
                TotalVendaRapida = playersSell.Sum(x=>x.ValorVenda),
                TotalVendaMercado = playersSell.Sum(x => x.MenorPrecoMercado == 0 ? x.ValorVenda : x.MenorPrecoMercado),
                TotalJogadores = clubPlayers.Count,
                TotalErro = playersSell.Sum(x=> !x.Vendeu ? 1 : 0),
                TotalVendido = playersSell.Sum(x => x.Vendeu ? 1 : 0),
            };
        }
    }

    public class PlayerSell
    {
        public long Id { get; set; }
        public long AssetId { get; set; }
        public string Nome { get; set; }
        public long Rating { get; set; }
        public long ValorVenda { get; set; }
        public long MenorPrecoMercado { get; set; }
        public bool Vendeu { get; set; }
    }

    public class PlayersSellResponse
    {
        public List<PlayerSell> Players { get; set; }
        public long TotalJogadores { get; set; }
        public long TotalVendaRapida { get; set; }
        public long TotalVendaMercado { get; set; }
        public long TotalVendido { get; set; }
        public long TotalErro { get; set; }
    }
}