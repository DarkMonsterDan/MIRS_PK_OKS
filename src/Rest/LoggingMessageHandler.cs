using Newtonsoft.Json;
using TestFramework.IO;
using TestFramework.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestFramework.Rest
{
    public class LoggingMessageHandler : DelegatingHandler
    {
        private readonly ILogService log;
        private readonly IFileService fileService;

        public LoggingMessageHandler(HttpMessageHandler innerHandler, ILogService log, IFileService fileService)
            : base(innerHandler)
        {
            this.log = log;
            this.fileService = fileService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var restDirectory = fileService.GetDirectory("REST");
            var requestFile = new TextFile(restDirectory.CreateFile("request.txt"));

                requestFile.AppendLine(request.ToString());
                if (request.Content != null)
                {
                    var json = JsonConvert.DeserializeObject(await request.Content.ReadAsStringAsync());
                    requestFile.AppendLine(JsonConvert.SerializeObject(json, Formatting.Indented));
                }
        

            var response = await base.SendAsync(request, cancellationToken);

            var responseFile = new TextFile(restDirectory.CreateFile("response.txt"));
            {
                responseFile.AppendLine(response.ToString());
                if (response.Content != null)
                {
                    var json = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    responseFile.AppendLine(JsonConvert.SerializeObject(json, Formatting.Indented));
                }
            }

            log.Info("REST запрос: ", new FileAttachment("request", requestFile.FileName), new FileAttachment("response", responseFile.FileName));
            return response;
        }
    }
}
