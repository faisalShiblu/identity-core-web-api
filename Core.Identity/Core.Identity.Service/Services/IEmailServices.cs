using Core.Identity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Service.Services
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
