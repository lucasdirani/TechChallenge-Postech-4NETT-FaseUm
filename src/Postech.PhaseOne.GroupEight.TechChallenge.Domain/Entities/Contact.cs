using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public class Contact : EntityBase
    {        
        
        public NameValueObject Name { get; set; }

        public EmailValueObject Email { get; set; }

        public PhoneValueObject Phone { get; set; }
        
        public List<AddressValueObject> Addresses { get; set; }

    }


    public class RegionValueObject
    {
        public int Id { get; set; }        
        public string Name { get; set; }

    }

    public class CityValueObject
    {
        public int Id { get; set; }
        public RegionValueObject Region { get; set; }
        public string Name { get; set; }

    }



    public class AddressValueObject
    {

        public CityValueObject City { get; set; }
        public string Address { get; set; }
        public string Number{ get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }

    }

    public class AreaCodeValueObject
    {
        [Required(ErrorMessage = "O DDD é obrigatório ")]
        [StringLength(2)]
        public string Code{ get; set; }

        public RegionValueObject Region { get; set; }

    }

    public class PhoneValueObject
    {        
        public AreaCodeValueObject CodeArea { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório ")]
        [StringLength(11)]
        public string Number { get; set; }
        public bool IsMain { get; set; } 

    }


    public class EmailValueObject
    {
        [Required(ErrorMessage = "O email do contato é obrigatório")]
        [StringLength(100)]
        public string Value { get; set; }
    }

    public class NameValueObject
    {
        [Required(ErrorMessage = "O nome do contato é obrigatório")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "O sobrenome do contato é obrigatório")]
        [StringLength(60)]
        public string LastName { get; set; }
    }
}
