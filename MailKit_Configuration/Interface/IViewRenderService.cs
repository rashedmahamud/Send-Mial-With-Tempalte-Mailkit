using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKit_Configuration.Interface
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName);
        Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
        string RenderToString<TModel>(string viewPath, TModel model);
        string RenderToString(string viewPath);
    }
}
