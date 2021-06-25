using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPZWPF
{
    class Values
    {
        private static int valueZoom = 2;
        public static int ValueZoom
        {
            get
            {
                return valueZoom;
            }
            set
            {
                if (value > 100)
                    valueZoom = 100;
                else if (value < 2)
                    valueZoom = 2;
                else
                    valueZoom = value;
            }
        }
    }
}
