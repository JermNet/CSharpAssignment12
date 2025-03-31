using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12
{
    // Now a seperate class to avoid repeated code (it also just makes more sense)
    // Enumeration defines the type of meshes in the grid spots
    public enum GridVal
    {
        X = 0, O = 1, center = 2, dot = 3
    }
}
