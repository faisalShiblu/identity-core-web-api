using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Service.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; } = 587;
        public bool UseSsl { get; set; } = false;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
