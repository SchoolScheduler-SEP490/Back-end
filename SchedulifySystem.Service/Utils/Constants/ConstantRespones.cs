﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Utils.Constants
{
    public static class ConstantRespones
    {
        public const string ACCOUNT_NOT_EXIST = "Account not exist.";
        public const string ACCOUNT_CAN_NOT_ACCESS = "Account can not access.";
        public const string PASSWORD_INCORRECT = "Password incorrect.";
        public const string SCHOOL_NOT_FOUND = "School Not found.";
        public const string SCHOOL_ALREADY_ASSIGNED = "School has been assigned to another account.";
        public const string SCHOOL_MANAGER_CREAT_SUCCESSFUL = "Create school manager successful.";
        public const string REQUEST_RESET_PASSWORD_SUCCESSFUL = "Send otp reset password success.";
        public const string REQUEST_RESET_PASSWORD_FAILED = "Send otp reset password failed.";
    }
}
