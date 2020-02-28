using AspNetCore3.Web.Comuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using AspNetCore3.Infra.IoC;
using ObjectFactory = AspNetCore3.Infra.IoC.ObjectFactory;

namespace AspNetCore3.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RespostaBaseAttribute : Attribute
    {
        public RespostaBaseAttribute()
        {
            Aplicar = true;
        }

        public RespostaBaseAttribute(bool aplicar)
        {
            Aplicar = aplicar;
        }
        public bool Aplicar { get; set; }
    }

    public class RespostaBaseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var habilitado = false;
            var cas = context.Controller.GetType().GetCustomAttributes(typeof(RespostaBaseAttribute), false);
            if (cas != null && cas.Length > 0 && (cas[0] is RespostaBaseAttribute rb && rb.Aplicar))
                habilitado = true;

            var actionDescription = context.ActionDescriptor as ControllerActionDescriptor;
            cas = actionDescription.MethodInfo.GetCustomAttributes(typeof(RespostaBaseAttribute), false);
            if (cas != null && cas.Length > 0 && (cas[0] is RespostaBaseAttribute rbm))
                habilitado = rbm.Aplicar;

            if (!habilitado)
                return;

            if (!context.ModelState.IsValid)
            {
                var erros = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors.Select(x => new RespostaBaseValidacaoViewModel(key, x.ErrorMessage)))
                    .ToList();

                var reposta = new RespostaBaseViewModelGenerico()
                {
                    Status = 1,
                    Erros = erros,
                    MensagemRetorno = $"Campos obrigatórios:<br />{string.Join(" <br />", erros.Select(i => i.Campo).ToArray())}"
                };

                context.Result = new JsonResult(reposta);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var habilitado = false;
            var cas = context.Controller.GetType().GetCustomAttributes(typeof(RespostaBaseAttribute), false);
            if (cas != null && cas.Length > 0 && (cas[0] is RespostaBaseAttribute rb && rb.Aplicar))
                habilitado = true;

            var actionDescription = context.ActionDescriptor as ControllerActionDescriptor;
            cas = actionDescription.MethodInfo.GetCustomAttributes(typeof(RespostaBaseAttribute), false);
            if (cas != null && cas.Length > 0 && (cas[0] is RespostaBaseAttribute rbm))
                habilitado = rbm.Aplicar;

            if (!habilitado)
                return;

            var pageResultData = context.Result is ObjectResult jsonPageValue && jsonPageValue.Value is IPagedResultDTO pageResult ? pageResult : null;

            if (context.Result != null)
            {
                var registros = ((Microsoft.AspNetCore.Mvc.JsonResult)context.Result).Value;              
            }         

            RespostaBaseViewModelGenerico resposta;
            if (pageResultData != null)
                resposta = new RespostaBasePaginadoViewModelGenerico() { TotalItens = pageResultData.TotalCount, PaginaAtual = pageResultData.CurrentPage, TotalPaginas = pageResultData.TotalPages };
            else
                resposta = new RespostaBaseViewModelGenerico();

            var statusCode = System.Net.HttpStatusCode.OK;
            var exception = context.Exception;
            if (exception != null)
            {
                //var logger = ObjectFactory.Obter<ILogger<RespostaBaseAttribute>>();

                //ObjectFactory.Obter<ISessionFactory>()?.DisposeCurrentSession(false);

                //Verificando se existe innerException com o tipo DomainLayerException
                var exValidate = exception;
                while (exValidate != null && !(exValidate is DomainLayerException))
                {
                    if (exValidate.InnerException is DomainLayerException)
                    {
                        exception = exValidate.InnerException;
                        break;
                    }
                    exValidate = exValidate.InnerException;
                }

                if (exception is DomainLayerException dle)
                {
                    resposta.MensagemRetorno = dle.Message;
                    resposta.Status = dle.Status;

                    resposta.Excecao = dle.Message;
                    if (!string.IsNullOrWhiteSpace(dle.Details))
                        resposta.Excecao = dle.Details;
                    if (!string.IsNullOrWhiteSpace(dle.DetailsInHtml))
                        resposta.Excecao = dle.DetailsInHtml;

                    statusCode = dle.HttpStatusCode;

                    //logger.LogDebug(exception, exception.Message);
                }
                else if (exception is Exception)
                {
                    resposta.MensagemRetorno = "EstouChegandoResource.comum_erro_inesperado";
                    resposta.Status = -1;
#if DEBUG
                    resposta.Excecao = exception.GetExceptionText();
#endif
                    //logger.LogError(exception, exception.Message);
                }
            }

            if (context.Result is JsonResult json)
            {
                resposta.Conteudo = json.Value;
                context.Result = new JsonResult(resposta);
            }
            else if (context.Result is ObjectResult objResult)
            {
                resposta.Conteudo = objResult.Value;
                context.Result = new JsonResult(resposta);
            }
            else
            {
                context.Result = new JsonResult(resposta);
            }

            context.Exception = null;
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }
    }
}
