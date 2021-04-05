using System.ComponentModel.DataAnnotations;

namespace TesteCiatecnica.Models.Enums
{
    public enum CustomerTypes
    {
        [Display(Name = "Pessoa Física")]
        PhysicalPerson = 0,

        [Display(Name = "Pessoa Jurídica")]
        LegalPerson = 1,
    }
}
