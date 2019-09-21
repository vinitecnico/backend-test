using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public class LoadingFileRepository : ILoadingFileRepository
    {
        public LoadingFileRepository()
        {

        }

        public Extract Handle(IFormFile file)
        {
            if (file.Length > 0)
            {
                var extract = new Extract();
                extract.Pagamentos = new List<Movement>();
                extract.recebimentos = new List<Movement>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var item = reader.ReadLine().Split(";");

                        var movement = new Movement()
                        {
                            Data = item[0],
                            Descricao = item[1],
                            Moeda = "R$",
                            Valor = Convert.ToDecimal(item[2]),
                            categoria = item.Length > 2 ? item[3] : null
                        };

                        if (movement.Valor <= 0)
                        {
                            extract.Pagamentos.Add(movement);
                        }
                        else
                        {
                            extract.recebimentos.Add(movement);
                        }
                    }
                }
                return extract;
            }

            throw new System.Exception("file not found");
        }

    }
}