using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Size {
        public int IdSize { get; }
        public string Name { get; }

        public Size(int IdSize, string Name) {
            this.IdSize = IdSize;
            this.Name = Name;
        }
    }
}
