using Microsoft.AspNetCore.Http;
using StructureMap;
using System;
using System.Threading;

namespace AspNetCore3.Infra.IoC
{
    public class ObjectFactory
    {
        //[ThreadStatic]
        private static AsyncLocal<HttpContext> _httpContext = new AsyncLocal<HttpContext>();

        private static Container _container;

        public ObjectFactory()
        {
        }

        public static void SetContext(HttpContext context)
        {
            _httpContext.Value = context;
        }

        public static HttpContext GetContext()
        {
            return _httpContext?.Value;
        }

        public static T ObterNovaInstancia<T>()
        {
            //return ObterServico<T>();
            return _container.GetInstance<T>();
        }
        public static T Obter<T>()
        {
            try
            {
                return (T)_httpContext.Value.RequestServices.GetService(typeof(T));
            }/*
            catch (System.ObjectDisposedException ex)
            {
                try
                {
                    return _container.GetInstance<T>();
                }
                catch (Exception)
                {
                    throw new Exception($"1:Tipo {typeof(T).FullName} não encontrado", ex);
                }
            }*/
            catch (Exception ex)
            {
                try
                {
                    return _container.GetInstance<T>();
                }
                catch (Exception)
                {
                    throw new Exception($"2:Tipo {typeof(T).FullName} não encontrado", ex);
                }
            }
        }

        public static void Init(Container container)
        {
            _container = container;
        }
    }
}
