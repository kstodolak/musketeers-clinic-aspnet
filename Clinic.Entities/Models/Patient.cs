using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        [Required(ErrorMessage = "Wprowadź imię!")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Wprowadź nazwisko!")]
        public string Surname { get; set; }
        [DisplayName("Numer telefonu")]
        [Required(ErrorMessage = "Wprowadź numer telefonu!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Wprowadzony numer telefony jest nieprawidłowy.")]
        public string Phone { get; set; }
        [DisplayName("Ulica")]
        [Required(ErrorMessage = "Uzuepłnij adres!")]
        public string Adress { get; set; }
        [DisplayName("Kod pocztowy")]
        [Required(ErrorMessage = "Wprowadź kod pocztowy!")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})$",
                   ErrorMessage = "Wprowadzony kod pocztowy jest nieprawidłowy.")]
        public string PostCode { get; set; }
        [DisplayName("Miasto")]
        [Required(ErrorMessage = "Wprowadź miast!")]
        public string City { get; set; }
        public string AccountId { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
