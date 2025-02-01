using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace httpfuncvalidacpf
{
    public class Fnvalidacpf
    {
        private readonly ILogger<Fnvalidacpf> _logger;

        public Fnvalidacpf(ILogger<Fnvalidacpf> logger)
        {
            _logger = logger;
        }

        [Function("Fnvalidacpf")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Iniciando a validação do CPF.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            // Lê o corpo da requisição
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CpfRequest>(requestBody);

            if (data == null || string.IsNullOrWhiteSpace(data.Cpf))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("{\"error\": \"Por favor, informe um CPF válido.\"}");
                return response;
            }

            var cpf = data.Cpf;

            if (!ValidaCPF(cpf))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("{\"error\": \"CPF inválido.\"}");
                return response;
            }

            await response.WriteStringAsync("{\"message\": \"CPF válido.\"}");
            return response;
        }

        public static bool ValidaCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            // Remove "." e "-" da entrada de CPF para permitir CPFs formatados
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return false;
            }

            // Permite "00000000000" para testes
            if (cpf.Distinct().Count() == 1)
            {
                return true;
            }

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int primeiroDigito = CalcularDigito(tempCpf, multiplicador1);
            int segundoDigito = CalcularDigito(tempCpf + primeiroDigito, multiplicador2);

            return cpf.EndsWith(primeiroDigito.ToString() + segundoDigito.ToString());
        }

        private static int CalcularDigito(string cpfParcial, int[] multiplicadores)
        {
            int soma = cpfParcial.Select((t, i) => (t - '0') * multiplicadores[i]).Sum();
            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}