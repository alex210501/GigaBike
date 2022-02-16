using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    public class Color {
        public int IdColor { get; }
        public string Name { get; }

        public Color(int IdColor, string Name) {
            this.IdColor = IdColor;
            this.Name = Name;
        }
    }
}
