using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Areas.Store.Models
{
    [NamePasswordEqual]
    public class User : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [BindRequired]
        public string Name { get; set; }

        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        [BindRequired]
        //[BindingBehavior(BindingBehavior.Optional)]
        public int Age { get; set; }

        [BindNever]
        public bool HasRight { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [JsonProperty("password_confirm"), BindProperty(Name = "password_confirm")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Required]
        public string Email { get; set; }

        public string Roles { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Введите имя!", new List<string>() { "Name" }));
            }
            if (string.IsNullOrWhiteSpace(this.Email))
            {
                errors.Add(new ValidationResult("Введите электронный адрес!"));
            }
            if (this.Age < 0 || this.Age > 120)
            {
                errors.Add(new ValidationResult("Недопустимый возраст!"));
            }
            if (!StringComparer.OrdinalIgnoreCase.Equals(Roles, "User"))
            {
                errors.Add(new ValidationResult("Недопустимая роль!"));
            }

            return errors;
        }
    }


    public class NamePasswordEqualAttribute : ValidationAttribute
    {
        public NamePasswordEqualAttribute()
        {
            ErrorMessage = "Имя и пароль не должны совпадать!";
        }
        public override bool IsValid(object value)
        {
            User p = value as User;

            if (p.Name == p.Password)
            {
                return false;
            }
            return true;
        }
    }
}
