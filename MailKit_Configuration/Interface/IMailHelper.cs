using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKit_Configuration.Interface
{
    public interface IMailHelper
    {
        void SendMail(string to, string from, string subject, string content);
    }
}
