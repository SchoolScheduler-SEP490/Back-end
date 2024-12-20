﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchedulifySystem.Repository.EntityModels;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using SchedulifySystem.Repository.DBContext;
using SchedulifySystem.Service.UnitOfWork;
using SchedulifySystem.Service.Mapper;
using SchedulifySystem.Repository.Repositories.Interfaces;
using SchedulifySystem.Repository.Repositories.Implements;
using SchedulifySystem.Service.Services.Interfaces;
using SchedulifySystem.Service.Services.Implements;
using SchedulifySystem.Service.BusinessModels.EmailModels;
using System.Security.Claims;


namespace SchedulifySystem.API
{
    public static class DependencyInjection
    {
        public static void AddWebAPIService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                // Prevent circular references (ignores cycles in object graphs)
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                // Set property naming policy to kebab-case (lowercase with hyphens)
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.KebabCaseLower;

                // Pretty-print JSON for readability
                options.JsonSerializerOptions.WriteIndented = true;

                // Add converter for enums to serialize them as strings
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();

            #region add cors
            services.AddCors(options =>
            {
                options.AddPolicy("app-cors",
                    builder =>
                    {
                        builder.WithOrigins("https://schedulify.id.vn", "http://localhost:8080", "http://localhost:3000")  // Chỉ cho phép domain này
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("X-Pagination")
                        .AllowAnyMethod();
                    });
            });


            #endregion

            #region signal R
            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(30);
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
            });

            #endregion

            #region config authen swagger
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Schedulify Web API", Version = "v.10.24" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add Authentication and JwtBearer
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                     ValidAudience = builder.Configuration["JWT:ValidAudience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero // Giảm thời gian lệch đồng hồ
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            #endregion
            #region Email setting
            //add dj mail service
            services.AddTransient<IMailService, MailService>();

            //Add config mail setting
            services.Configure<EmailConfig>(builder.Configuration.GetSection("MailSettings"));
            #endregion
        }
        public static IServiceCollection AddInfractstructure(this IServiceCollection services, IConfiguration config)
        {
            #region config service 

            //Configure UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MapperConfigs).Assembly);

            // config claim service 
            services.AddTransient<IClaimsService, ClaimsService>();

            //config user service and repo
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            //config account service and repo
            services.AddTransient<IAccountService, AccountService>();

            //config teacher service and repo
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<ITeacherService, TeacherService>();

            //config role service and repo
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleService, RoleService>();

            //config student class service and repo
            services.AddTransient<IStudentClassesRepository, StudentClassesRepository>();
            services.AddTransient<IStudentClassService, StudentClassService>();

            //config role assignment service and repo
            services.AddTransient<IRoleAssignmentRepository, RoleAssignmentRepository>();
            services.AddTransient<IRoleAssignmentService, RoleAssignmentService>();

            //config school service and repo
            services.AddTransient<ISchoolRepository, SchoolRepository>();
            services.AddTransient<ISchoolService, SchoolService>();

            //config subject service and repo
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ISubjectService, SubjectService>();

            //config building service and repo
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IBuildingService, BuildingService>();


            //config room service and repo
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IRoomService, RoomService>();

            //config room subject service and repo
            services.AddTransient<IRoomSubjectRepository, RoomSubjectRepository>();
            services.AddTransient<IRoomSubjectService, RoomSubjectService>();

            //config student class room subject repo
            services.AddTransient<IStudentClassRoomSubjectRepository, StudentClassRoomSubjectRepository>();
            //services.AddTransient<IRoomSubjectService, RoomSubjectService>();

            //config subject group service and repo
            services.AddTransient<ICurriculumRepository, CurriculumRepository>();
            services.AddTransient<ICurriculumService, CurriculumService>();

            //config district service and repo
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IDistrictService, DistrictService>();

            //config province service and repo
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IProvinceService, ProvinceService>();

            //config otp service and repo
            services.AddTransient<IOtpRepository, OtpRepository>();
            services.AddTransient<IOtpService, OtpService>();

            //config school year service and repo
            services.AddTransient<ISchoolYearRepository, SchoolYearRepository>();
            services.AddTransient<ISchoolYearService, SchoolYearService>();

            //config Department service and repo
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            //config TeacherAssignment service and repo
            services.AddTransient<ITeacherAssignmentRepository, TeacherAssignmentRepository>();
            services.AddTransient<ITeacherAssignmentService, TeacherAssignmentService>();

            //config TeachableSubject service and repo
            services.AddTransient<ITeachableSubjectRepository, TeachableSubjectRepository>();
            services.AddTransient<ITeachableSubjectService, TeachableSubjectService>();

            //config Term service and repo
            services.AddTransient<ITermRepository, TermRepository>();
            services.AddTransient<ITermService, TermService>();

            //config Subject in group service and repo
            services.AddTransient<ICurriculumDetailRepository, CurriculumDetailRepository>();
            services.AddTransient<ICurriculumDetailService, CurriculumDetailService>();

            //config Timetable service and repo
            services.AddTransient<ISchoolScheduleRepository, SchoolScheduleRepository>();
            services.AddTransient<ITimetableService, TimeTableService>();

            //config Timetable service and repo
            services.AddTransient<IStudentClassGroupRepository, StudentClassGroupRepository>();
            services.AddTransient<IStudentClassGroupService, StudentClassGroupService>();

            //config Submit Request service and repo
            services.AddTransient<ISubmitRequestRepository, SubmitRequestRepository>();
            services.AddTransient<ISubmitRequestService, SubmitRequestService>();

            //config Class period service and repo
            services.AddTransient<IClassPeriodRepository, ClassPeriodRepository>();
            services.AddTransient<IClassPeriodService, ClassPeriodService>();

            //config Change period service and repo
            services.AddTransient<IPeriodChangeRepository, PeriodChangeRepository>();
            //services.AddTransient<IClassPeriodService, ClassPeriodService>();

            //config class schedule service and repo
            services.AddTransient<IClassScheduleRepository, ClassScheduleRepository>();
            //services.AddTransient<IClassPeriodService, ClassPeriodService>();

            //config Notification
            services.AddSignalR();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            #endregion

            #region add db context
            services.AddDbContext<SchedulifyContext>(options =>
                options.UseNpgsql(config.GetConnectionString("SchedulifyPostgres")));
            return services;
            #endregion
        }
    }
}
