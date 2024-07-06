using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //public record CompanyForUpdateDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);
    //public record CompanyForUpdateDto
    //{
    //    [Required(ErrorMessage = "Comapny name is a required field.")]
    //    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    //    public string? Name { get; init; }

    //    [Required(ErrorMessage = "Address is a required field.")]
    //    public string? Address { get; init; }

    //    [Required(ErrorMessage = "Country is a required field.")]
    //    public string? Country { get; init; }

    //    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    //}

    public record CompanyForUpdateDto : CompanyForManipulationDto;
}
