using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luiza.Labs.Domain.Models
{
    public class Mail
    {

        public string Subject { get; set; } = string.Empty;
        public string NameReceive { get; set; } = string.Empty;
        public string NameSend { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string EmailAddressFrom { get; set; } = string.Empty;
        public string EmailAddressTo { get; set; } = string.Empty;
        public string TextBody { get; set; } = string.Empty;
    }
}
