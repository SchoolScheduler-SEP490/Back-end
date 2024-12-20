﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchedulifySystem.Repository.Commons;
using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Service.BusinessModels.NotificationBusinessModels;
using SchedulifySystem.Service.BusinessModels.SubmitRequest;
using SchedulifySystem.Service.BusinessModels.SubmitRequestBusinessModels;
using SchedulifySystem.Service.Enums;
using SchedulifySystem.Service.Exceptions;
using SchedulifySystem.Service.Services.Interfaces;
using SchedulifySystem.Service.UnitOfWork;
using SchedulifySystem.Service.Utils.Constants;
using SchedulifySystem.Service.ViewModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Services.Implements
{
    public class SubmitRequestService : ISubmitRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public SubmitRequestService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        #region Get Submit Request Of School Async
        public async Task<BaseResponseModel> GetSubmitRequestOfSchoolAsync(int schoolYearId, int schoolId, ERequestStatus? eRequestStatus)
        {
            var getSchools = await _unitOfWork.SchoolRepo.GetByIdAsync(schoolId, filter: t => t.Status == (int)SchoolStatus.Active)
                ?? throw new NotExistsException(ConstantResponse.SCHOOL_NOT_FOUND);

            var getSchoolYears = await _unitOfWork.SchoolYearRepo.GetByIdAsync(schoolYearId, filter: t => !t.IsDeleted)
                ?? throw new NotExistsException(ConstantResponse.SCHOOL_YEAR_NOT_EXIST);
            var getTeacherIds = (await _unitOfWork.TeacherRepo.GetV2Async(
                filter: t => !t.IsDeleted && t.SchoolId == schoolId)).Select(t => t.Id);

            var submitRequests = await _unitOfWork.SubmitRequestRepo.ToPaginationIncludeAsync(
                filter: t => getTeacherIds.Contains(t.TeacherId) && t.SchoolYearId == schoolYearId
                && (eRequestStatus == null || t.Status == (int)eRequestStatus),
                include: query => query.Include(u => u.Teacher),
                orderBy: o => o.OrderByDescending(r => r.CreateDate));

            var result = _mapper.Map<Pagination<SubmitRequestViewModel>>(submitRequests);
            return new BaseResponseModel()
            {
                Status = StatusCodes.Status200OK,
                Message = ConstantResponse.GET_SUBMIT_REQUEST_SUCCESS,
                Result = result
            };
        }
        #endregion

        #region Send Application Async
        public async Task<BaseResponseModel> SendApplicationAsync(int schoolId, SubmitSendRequestModel applicationRequest)
        {
            var getTeachers = await _unitOfWork.TeacherRepo.GetAsync(
                filter: t => t.SchoolId == schoolId && t.Status == (int)TeacherStatus.HoatDong && !t.IsDeleted);
            var teacher = getTeachers.FirstOrDefault();
            applicationRequest.Status = ERequestStatus.Pending;
            var application = _mapper.Map<SubmitRequest>(applicationRequest);
            application.IsProcess = false;
            await _unitOfWork.SubmitRequestRepo.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();

            var getAccountIdInSchool = (await _unitOfWork.UserRepo.GetAsync(
                filter: t => t.SchoolId == schoolId && t.Status == (int)AccountStatus.Active)).Select(t => t.Id);

            var getRole = (await _unitOfWork.RoleRepo.GetAsync(filter: t => t.Name == RoleEnum.SchoolManager.ToString())).FirstOrDefault();


            var schoolManager = (await _unitOfWork.RoleAssignmentRepo.GetAsync(
                            filter: t => getAccountIdInSchool.Contains(t.AccountId) && t.RoleId == getRole.Id && !t.IsDeleted)).Select(t => t.Id).FirstOrDefault();

            NotificationModel noti = new()
            {
                Title = "Yêu cầu xử lí đơn",
                Message = $"Bạn có 1 yêu cầu từ giáo viên {teacher.FirstName} {teacher.LastName}. Vui lòng kiểm tra đơn cần xử lý.",
                Type = ENotificationType.HeThong,
                Link = ""
            };

            await _notificationService.SendNotificationToUser(schoolManager, noti);

            return new BaseResponseModel()
            {
                Status = StatusCodes.Status200OK,
                Message = ConstantResponse.SUBMIT_REQUEST_SUCCESS
            };
        }
        #endregion

        #region Get Submit Request By Id
        public async Task<BaseResponseModel> GetSubmitRequestByTeacherId(int teacherId, int schoolYearId, ERequestStatus? eRequestStatus)
        {
            var getTeachers = await _unitOfWork.TeacherRepo.GetByIdAsync(teacherId, filter: t => !t.IsDeleted)
               ?? throw new NotExistsException(ConstantResponse.SCHOOL_NOT_FOUND);

            var getSchoolYears = await _unitOfWork.SchoolYearRepo.GetByIdAsync(schoolYearId, filter: t => !t.IsDeleted)
                ?? throw new NotExistsException(ConstantResponse.SCHOOL_YEAR_NOT_EXIST);
           
            var submitRequests = await _unitOfWork.SubmitRequestRepo.ToPaginationIncludeAsync(
                filter: t => t.TeacherId == teacherId && t.SchoolYearId == schoolYearId
                && (eRequestStatus == null || t.Status == (int)eRequestStatus),
                include: query => query.Include(u => u.Teacher),
                orderBy: o => o.OrderByDescending(r => r.CreateDate));

            var result = _mapper.Map<Pagination<SubmitRequestViewModel>>(submitRequests);
            return new BaseResponseModel()
            {
                Status = StatusCodes.Status200OK,
                Message = ConstantResponse.GET_SUBMIT_REQUEST_SUCCESS,
                Result = result
            };
        }
        #endregion

        #region 
        public async Task<BaseResponseModel> ProcessSubmitRequest(int id, ProcessSubmitRequestModel processSubmitRequestModel)
        {
            var getRequest = await _unitOfWork.SubmitRequestRepo.GetByIdAsync(id, filter: t => t.Id == id && !t.IsDeleted, include: query => query.Include(u => u.Teacher))
                ?? throw new NotExistsException(ConstantResponse.REQUEST_NOT_EXIST);

            if(getRequest.IsProcess == true)
            {
                throw new DefaultException(ConstantResponse.REQUEST_ALREADY_SOLVED);
            }

            getRequest.ProcessNote = processSubmitRequestModel.ProcessNote;
            getRequest.RequestTime = DateTime.UtcNow;
            getRequest.Status = (int)processSubmitRequestModel.Status;
            getRequest.IsProcess = true;

            _unitOfWork.SubmitRequestRepo.Update(getRequest);

            NotificationModel noti = new()
            {
                Title = "Xử lý đơn từ",
                Message = "Bạn có 1 đơn đã được xử lý. Vui lòng kiểm mục Báo cáo.",
                Type = ENotificationType.PheDuyet,
                Link = ""
            };
            await _unitOfWork.SaveChangesAsync();

            var getUser = (await _unitOfWork.UserRepo.GetAsync(filter: t => t.Email == getRequest.Teacher.Email)).FirstOrDefault();
            if(getUser != null)
            {
                await _notificationService.SendNotificationToUser(getUser.Id, noti);
            }
            return new BaseResponseModel()
            {
                Status = StatusCodes.Status200OK,
                Message = ConstantResponse.SUBMIT_REQUEST_SUCCESS
            };
        }
        #endregion
    }
}
