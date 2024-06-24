using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    // A Record type provides us an easier way to create an immutable reference type in .NET.
    // This means that the Record’s instance property values cannot change after its initialization.

    //[Serializable]
    //public record CompanyDto(Guid Id, string Name, string FullAddress);

    public record CompanyDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? FullAddress { get; init; }
    }


}
