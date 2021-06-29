using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using AppScoreExemplo.Model;
using Newtonsoft.Json;

namespace AppScoreExemplo.Services
{
    public class GameScoreApi
    {
        //endereço dos dados
        const String URL = "http://restdfilitto.herokuapp.com/highscores";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "Application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");
            return client;
        }
        //Pega todos os dados da API fintrados de highscores
        //retorna uma lista com um objeto gamescore ou uma lista vazia
        public async Task<List<GameScore>> GetHighScores()
        {
            HttpClient client = GetClient();
            //HttpResponseMessage response = await client.GetAsync(dados);
            var response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GameScore>>(content);
            }
            return new List<GameScore>();
        }

        public async Task<GameScore> GetHighScore(int Id)
        {
            String dados = URL + "?id=" + Id;
            //Uri uri = new Uri(dados);
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.GetAsync(dados);
            //var response = await client.GetAsync(dados);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<GameScore>>(content);
                return games[0];
            }
            return new GameScore();
        }

        public async Task CreateHighScore(GameScore game)
        {
            String dados = URL;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(dados, content);
        }

        public async Task UpDateHighScore(GameScore game)
        {
            String dados = URL + "/" + game.id;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(dados, content);
        }

        public async Task DeleteHighScore(int Id)
        {
            String dados = URL + "/" + Id.ToString();
            HttpClient client = GetClient();
            HttpResponseMessage response = await client.DeleteAsync(dados);
        }
    }
}
