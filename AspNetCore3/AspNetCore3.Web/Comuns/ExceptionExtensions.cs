﻿using System;
using System.Text;


namespace AspNetCore3.Web.Comuns
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionText(this Exception ex)
        {
            var sb = new StringBuilder();
            if (ex != null)
            {
                var inex = ex;
                var exCount = 0;
                while (inex != null)
                {
                    exCount++;
                    sb.AppendFormat("({0})   <b>Message:</b><pre>{1}</pre>", exCount, inex.Message);
                    sb.AppendFormat("({0})   <b>StackTrace:</b><pre>{1}</pre>", exCount, inex.StackTrace);
                    inex = inex.InnerException;
                }
            }
            return sb.ToString();
        }

        public static string GetExceptionMessage(this Exception ex)
        {
            var sb = new StringBuilder();
            if (ex != null)
            {
                var inex = ex;
                var exCount = 0;
                while (inex != null)
                {
                    exCount++;
                    sb.AppendFormat("({0})   <b>Message:</b><pre>{1}</pre>", exCount, inex.Message);
                    inex = inex.InnerException;
                }
            }
            return sb.ToString();
        }
    }
}
