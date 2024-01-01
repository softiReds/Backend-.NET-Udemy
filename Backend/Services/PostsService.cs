using Backend.DTOs;
using System.Text.Json;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient; //  HttpClient -> Permite crear conexiónes a traves de HTTP con otros servicios

        public PostsService() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PostDTO>> Get()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var result = await _httpClient.GetAsync(url);   //  Request Get de tipo async a la url configurada
            var body = await result.Content.ReadAsStringAsync();    //  .Content.ReadAsStringAsync() -> Lee el contenido de un response y lo convierte en string

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  //  true -> Ignora las mayusculas y minusculas de los campos en el servicio y en la clase DTO
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDTO>>(body, options); //  Enviamos las opciones configuradas para que sean implementadas al momento de deserializar los objetos

            return post;
        }
    }
}
