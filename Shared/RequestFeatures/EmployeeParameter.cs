using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class EmployeeParameter: RequestParameters
    {
        // FOR FILTERING
        // unsigned integer prop to avoid negatice year values
        // Since the default uint value is 0, we don’t need to explicitly define it; 0 is okay in this case.
        public uint MinAge { get; set; }
        // For MaxAge, we want to set it to the max int value.
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange  => MaxAge > MinAge;

        // FOR SEARCHING
        public string? SearchTerm { get; set; }
    }
}
