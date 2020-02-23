using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Pens
    {
        public static readonly IList<char> Symbols = new ReadOnlyCollection<char>
            (new List<char>
            {
                ' ','*','-','+','0'
            });
    }
}
