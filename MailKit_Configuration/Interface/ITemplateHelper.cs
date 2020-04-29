using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKit_Configuration.Interface
{
   public interface ITemplateHelper
    {
        string GetTemplateHtmlAsStringAsync<T>(string viewName, T model);
    }
}
