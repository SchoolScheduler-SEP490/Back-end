﻿using SchedulifySystem.Service.ViewModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Services.Interfaces
{
    public interface IOtpService
    {
        Task<bool> SendOTPResetPassword(int accountId, string email);
        Task<bool> ConfirmResetPassword(int accountId, int code);
    }
}
