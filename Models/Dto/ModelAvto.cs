using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaCatalog.Models.Dto
{
    public class ModelAvto
    {
        public string cmodnamepc { get; set; }
        public string ngnp { get; set; }
        public override string ToString()
        {
            return cmodnamepc + " " + ngnp;
        }
    }
}
