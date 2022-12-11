using apiSearch.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace apiSearch
{
public class PesquisaApp
{
    static HttpClient httpClient = new HttpClient();

        public static async Task<string> GetHtmlAsync(string search)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri($"https://www.google.com/search?q={search}&ie=UTF-8");

            }
            else
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri($"https://www.google.com/search?q={search}&ie=UTF-8");
            }
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            string html = "";
            HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                html = await response.Content.ReadAsStringAsync();

                return html;
            }
            return html;
        }

        public static async Task<string> PesquisaAsync(string pesq)
        {
            string html = await GetHtmlAsync(pesq);

            Resultado result = new Resultado();
            result.Resultados = new List<Pesquisa>();

            convert(result, html);

            var json = JsonConvert.SerializeObject(result);
            return json;
        }

        private static void convert(Resultado result, String html)
        {
            while (html.Contains("<a href=\"/url?q="))
            {
                var str = FindText.findText(html, "<a href=\"/url?q=", ">");
                var len = str.Length;
                var link = str.Contains("%") ? str.Substring(0, FindNth.findNth(str, '"', 1)) :
                    str.Substring(0, FindNth.findNth(str, '&', 1));

                string newTitle;
                try
                {
                    newTitle = FindText.findText(html, "<a href=\"/url?q=", "</h3>");
                    var newLink = newTitle.Substring(FindNth.findNth(newTitle, '>', 5) + 1);
                    var title = newLink.Substring(0, FindNth.findNth(newLink, '<', 1));

                    if (title.Contains("&#8211"))
                    {
                        title = title.Replace("&#8211;", "-");
                    }

                    var resultado = new Pesquisa();
                    resultado.Titulo = title;
                    resultado.Link = link;

                    if (!result.Resultados.Contains(resultado) & title.Length > 3)
                    {
                        if (!link.Equals("https://support.google.com") &
                            !title.Equals(""))
                        {
                            result.Resultados.Add(resultado);
                        }
                    }
                }
                catch (Exception)
                {

                }
                html = html.Substring(html.IndexOf(">") + len);
            }
        }
    }
}