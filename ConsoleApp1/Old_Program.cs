using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sarinha
{
    class MeuAmorzao
    {/*
        static void Main(string[] args)
        {

            var requisicaoWeb = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts/1");
            //requisicaoWeb.Method = "GET";


            var postData = "1";

            var data = Encoding.ASCII.GetBytes(postData);

            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";

            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());
                Console.WriteLine(post.Id + " " + post.title + " " + post.body);

                if (post.Id.Equals(1))
                {
                    Console.WriteLine("é válido");
                }
                else
                {
                    Console.WriteLine("não é valido!! fudeu mermão!! corre! x9");
                }

                Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }
            Console.ReadLine();
        }*/
    }
    public class Post
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
