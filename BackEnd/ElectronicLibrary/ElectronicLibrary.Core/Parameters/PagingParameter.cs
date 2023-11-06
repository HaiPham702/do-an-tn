using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary.Core.Parameters
{
    public class PagingParameter
    {
        public string Columns { get; set; }

        public string Sort { get; set; }

        public string Filter { get; set; }

        public int Limt { get; set; }
            
        public int Skip { get; set; }
    }

    public class Filter
    {
        public string ColName { get; set; }

        public string Value { get; set; }

        public string Operator { get; set; }
    }
}
