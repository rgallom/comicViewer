using Comics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Comics.Services
{

    public class ComicService
    {          
        public async Task<ComicViewModel> GetCurrentComic()
        {
            HttpClient httpclient = new HttpClient();
            var response = await httpclient.GetAsync("https://xkcd.com/info.0.json");           
            var bodyContent = await response.Content.ReadAsStringAsync();     
            
            return JsonConvert.DeserializeObject<ComicViewModel>(bodyContent); ;
        }        

        public async Task<ComicViewModel> RequestComicByID(int ID, bool searchForPrevious = true)
        {
            int currentID = ID;
            HttpClient httpclient = new HttpClient();
            bool isComicAvailable = true;          
            HttpResponseMessage response;

            var lastComic = await GetCurrentComic();

            if (ID < 1)
            {
                currentID = 1;
                searchForPrevious = false;             
            }

            if (lastComic.num < ID)
            {
                currentID = lastComic.num;
                searchForPrevious = true;               
            }

            do
            {
                response = await httpclient.GetAsync(GetComicUrl(currentID));
                isComicAvailable = response.StatusCode == HttpStatusCode.OK;                
                currentID = searchForPrevious ? currentID - 1 : currentID + 1;

            } while (!isComicAvailable);        

            var bodyContent = await response.Content.ReadAsStringAsync();
            var comic = JsonConvert.DeserializeObject<ComicViewModel>(bodyContent);           
            comic.ShowPrevious = comic.num > 1;
            comic.ShowNext = comic.num < lastComic.num;

            return comic;
        }

        private string GetComicUrl(int ID)
        {                   
            return $"https://xkcd.com/{ID}/info.0.json";
        }
    }
}