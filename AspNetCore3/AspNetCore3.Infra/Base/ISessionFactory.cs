
using System.Data;



namespace AspNetCore3.Infra.Base
{
    public interface ISessionFactory
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        //IMultitentantSession GetCurrentSession();
        //IMultitentantSession GetCurrentClienteSession(bool forcarConectarBanco = false);
        //IMultitentantSession GetSession(string contexto, bool conectarBanco = true, bool iniciarTransacao = true);
        //IMultitentantSession GetSession(string contexto, string connectionString, ICliente cliente, bool iniciarTransacao);
        void DisposeCurrentSession(bool commit = true);
        void StartClienteSession(string clienteConnectionString, bool iniciarTransacao);
    }
}
