using System;
using System.Net;


namespace AspNetCore3.Web.Comuns
{
    public class DomainLayerException : Exception
    {

        public DomainLayerException(string message, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
        {
            HttpStatusCode = statusCode;
            Status = status;
        }

        public DomainLayerException(string message, string details, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
        {
            HttpStatusCode = statusCode;
            Status = status;
            Details = details;
        }

        public DomainLayerException(string message, string details, string detailsInHtml, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message)
        {
            HttpStatusCode = statusCode;
            Status = status;
            Details = details;
            DetailsInHtml = detailsInHtml;
        }


        public DomainLayerException(string message, Exception innerException, int status = 1, HttpStatusCode statusCode = HttpStatusCode.OK) : base(message, innerException)
        {
            HttpStatusCode = statusCode;
            Status = status;
        }

        public int Status { get; protected set; }
        public HttpStatusCode HttpStatusCode { get; protected set; }
        public string Details { get; set; }
        public string DetailsInHtml { get; set; }
    }
}
