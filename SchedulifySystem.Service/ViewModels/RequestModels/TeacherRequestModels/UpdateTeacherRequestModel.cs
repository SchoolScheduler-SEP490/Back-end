﻿using SchedulifySystem.Service.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.ViewModels.RequestModels.TeacherRequestModels
{
    public class UpdateTeacherRequestModel
    {
        [Required(ErrorMessage = "First Name is required."), MaxLength(100, ErrorMessage = "First Name can't be longer than 100 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required."), MaxLength(100, ErrorMessage = "Last Name can't be longer than 100 characters.")]
        public string? LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Abbreviation can't be longer than 50 characters.")]
        public string? Abbreviation { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Range(0, 1, ErrorMessage = "Gender must be either 0 (male) or 1 (female).")]
        public int Gender { get; set; }

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Date of Birth is require")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Date of Birth must be in the format YYYY-MM-DD.")]
        [ValidDateOnly(ErrorMessage = "Date of Birth is not a valid date.")]
        public string DateOfBirth { get; set; }

        public int SchoolId { get; set; }

        public int TeacherGroupId { get; set; }

        public int TeacherRole { get; set; }

        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
