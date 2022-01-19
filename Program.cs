using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnalisarPostman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\robso\Estudos\Tecnologia\Postman Collections\";
            string[] files = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
            List<Requisicoes> listaRequisicoes = new List<Requisicoes>();
            int i = 1;
            foreach (string file in files)
            {
                using(StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    JObject jsonCollection = JObject.Parse(json);
                    int contAws = 0;
                    foreach(var item in jsonCollection["item"])
                    {
                        if(item["request"]["url"]["raw"].ToString().IndexOf("viacep") > -1)
                            contAws++;
                    }
                    Requisicoes requisicoes = new Requisicoes
                    {
                        RequisicaoId = i,
                        RequisicaoName = Path.GetFileName(file),
                        QuantidadeUrl = jsonCollection["item"].Count(),
                        QuantidadeUrlAWS = contAws
                    };
                    listaRequisicoes.Add(requisicoes);
                }
                i++;
            }

            Console.ReadKey();
        }
    }
}
