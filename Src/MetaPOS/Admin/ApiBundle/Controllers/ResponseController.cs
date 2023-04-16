using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MetaPOS.Admin.ApiBundle.Entities;


namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<ContactEntity> Data { get; set; }
        public int Count { get; set; }

    }

    public class ResponseController : IHttpActionResult
    {
        Response _response;
        HttpRequestMessage _request;

        public ResponseController(Response response, HttpRequestMessage request) 
        {
            _response = response;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var responseResult = new HttpResponseMessage()
            {
                Content = new ObjectContent<Response>(_response, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };

            return Task.FromResult(responseResult);
        }
    }
}