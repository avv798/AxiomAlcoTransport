using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace UtmTestServer.Controllers
{
    public class UtmTestController : ApiController
    {
        private readonly string storagePath;

        public UtmTestController()
        {
            storagePath = @"c:\utmTest";
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage {Content = new StringContent(ReadFile("Index.html"))};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html") {CharSet = "UTF8"};

            return response;
        }

        [HttpGet]
        [Route("opt/out/{docType?}/{docNumber?}")]
        public HttpResponseMessage GetFile(string docType = null, string docNumber = null)
        {
            var fileName = string.IsNullOrEmpty(docType)
                ? "opt/out/index.xml"
                : Path.Combine("opt/out", docType, $"{docNumber}.xml");
            var response = new HttpResponseMessage {Content = new StringContent(ReadFile(fileName))};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml") {CharSet = "UTF8"};

            return response;
        }
      
        private string ReadFile(string fileName)
        {
            var path = Path.Combine(storagePath, fileName);
            return !File.Exists(path) ? null : File.ReadAllText(path, Encoding.UTF8);
        }
    }
}