using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASD.Identidade.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; }
        public int ExpiracaoHoras { get; }
        public string Emissor { get; }
        public string ValidoEm { get; }
    }
}
