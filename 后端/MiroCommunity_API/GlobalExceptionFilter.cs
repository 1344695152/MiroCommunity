using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroCommunity_API
{
    /// <summary>
    /// 全局过滤器
    /// </summary>
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private const string datettem = "yyyyMMddHH";

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            StringBuilder sb = new StringBuilder();
            string date = DateTime.Now.ToString(datettem);
            sb.AppendLine("Date:");
            sb.AppendLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
            sb.AppendLine("Message:");
            sb.AppendLine(context.Exception.Message);
            sb.AppendLine("StackTrace:");
            sb.AppendLine(context.Exception.StackTrace);
            sb.AppendLine("Source:");
            sb.AppendLine(context.Exception.Source);
            sb.AppendLine("InnerException.Message:");
            sb.AppendLine(context.Exception.InnerException?.Message);
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, $"{DateTime.Now.ToString(datettem)}.txt");
            await File.AppendAllTextAsync(path, sb.ToString(), Encoding.UTF8);
            context.ExceptionHandled = true;

            Result result = new Result();
            result.Code = 500;
            result.Message = "系统出现错误,请稍后重试!";
            context.Result = new ActionResult<Result>(result).Result;
        }
    }
}