﻿using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class RegistrationModel : BaseViewModel, IMapFrom<User>
    {
        [ScaffoldColumn(false)]
        public override string Id { get; set; }

        [Required]
        [Display(Name = "Електронна адреса")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Група")]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
