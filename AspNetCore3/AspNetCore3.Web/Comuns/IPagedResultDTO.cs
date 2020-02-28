using System.Collections.Generic;

namespace AspNetCore3.Web.Comuns
{
    public interface IPagedResultDTO
    {
        int TotalCount { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
    }
    public interface IPagedResultDTO<T> : IPagedResultDTO
    {
        IList<T> Items { get; set; }
    }
}
