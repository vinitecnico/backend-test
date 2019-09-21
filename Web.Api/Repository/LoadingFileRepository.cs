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
                extract.pagamentos = new List<MovementResult>();
                extract.recebimentos = new List<MovementResult>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        var item = reader.ReadLine().Split(";");

                        var movement = new MovementResult()
                        {
                            data = item[0],
                            descricao = item[1],
                            moeda = "R$",
                            valor = item[2],
                            categoria = item.Length > 2 ? item[3] : null
                        };

                        var valor = Convert.ToDecimal(movement.valor);

                        if (valor <= 0)
                        {
                            extract.pagamentos.Add(movement);
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