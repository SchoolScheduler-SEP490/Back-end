﻿using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Repository;
using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulifySystem.Service.BusinessModels.StudentClassBusinessModels;
using SchedulifySystem.Service.BusinessModels.TeacherBusinessModels;
using SchedulifySystem.Service.BusinessModels.ClassPeriodBusinessModels;
using SchedulifySystem.Service.BusinessModels.SubjectBusinessModels;

namespace SchedulifySystem.Service.BusinessModels.ScheduleBusinessMoldes
{
    public class TimetableIndividual : BaseEntity
    {
        public ETimetableFlag[,] TimetableFlag { get; set; } = null!;
        public List<ClassPeriodScheduleModel> TimetableUnits { get; set; } = [];
        public List<ClassScheduleModel> Classes { get; init; } = [];
        public List<TeacherScheduleModel> Teachers { get; init; } = [];
        public List<ConstraintErrorModel> ConstraintErrors { get; set; } = [];
        public Dictionary<int, List<SubjectScheduleModel>> DoubleSubjectsByGroup { get; set; } = [];
        public int Adaptability { get; set; }
        //tổi của một cá thể là số thế hệ mà cá thể đó đã trải qua trong quá trình tiến hóa.
        //Khi cá thể được tạo ra, tuổi của nó được khởi tạo là 1, và sau mỗi thế hệ, tuổi của nó sẽ tăng lên.
        //Hệ thống có thể sử dụng tuổi để xác định xem cá thể đó có nên tiếp tục tồn tại qua các thế hệ tiếp theo hay không.
        //Nếu một cá thể quá già (tuổi cao), có thể nó sẽ bị loại bỏ để tạo điều kiện cho các cá thể trẻ, mới hơn với khả năng thích nghi tốt hơn.
        public int Age { get; set; }
        //Longevity - tuổi thọ của một cá thể là số thế hệ tối đa mà cá thể đó có thể tồn tại. Đây là một giá trị ngẫu nhiên được xác định khi cá thể được tạo ra
        //Khi tuổi của cá thể bằng hoặc vượt quá tuổi thọ của nó, cá thể sẽ bị loại khỏi quần thể.
        //Tuổi thọ giúp kiểm soát vòng đời của cá thể, đảm bảo rằng các cá thể không tồn tại mãi mãi trong quần thể.
        //Điều này tránh tình trạng "đóng băng" tiến hóa, khi các cá thể kém thích nghi có thể vẫn tồn tại quá lâu và làm giảm hiệu quả của quá trình tối ưu hóa.
        public int Longevity { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Semester { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimetableIndividual(
       ETimetableFlag[,] timetableFlag,
       List<ClassPeriodScheduleModel> timetableUnits,
       List<ClassScheduleModel> classes,
       List<TeacherScheduleModel> teachers,
       Dictionary<int, List<SubjectScheduleModel>> doubleSubjectsByGroup)
        {
            TimetableFlag = timetableFlag;
            TimetableUnits = timetableUnits;
            Classes = classes;
            Teachers = teachers;
            DoubleSubjectsByGroup = doubleSubjectsByGroup;
        }

        public void GetConstraintErrors()
        {
            if (TimetableUnits == null || TimetableUnits.Count == 0)
                return;

            // Khởi tạo hoặc làm trống `ConstraintErrors` nếu cần thiết
            if (ConstraintErrors == null)
            {
                ConstraintErrors = new List<ConstraintErrorModel>();
            }
            else
            {
                ConstraintErrors.Clear();
            }

            foreach (var unit in TimetableUnits)
            {
                if (unit != null && unit.ConstraintErrors != null && unit.ConstraintErrors.Count > 0)
                {
                    // Tạo một bản sao của danh sách `ConstraintErrors` để tránh thay đổi trong khi lặp
                    var errorsCopy = unit.ConstraintErrors.ToArray(); // Dùng `ToArray()` thay vì `ToList()` để tạo bản sao an toàn

                    foreach (var error in errorsCopy)
                    {
                        if (error != null)
                        {
                            // Kiểm tra nếu lỗi đã tồn tại trước khi thêm vào `ConstraintErrors`
                            if (!ConstraintErrors.Any(existingError =>
                                existingError.SubjectName == error.SubjectName &&
                                existingError.TeacherName == error.TeacherName &&
                                existingError.ClassName == error.ClassName &&
                                existingError.Code == error.Code))
                            {
                                ConstraintErrors.Add(error);
                            }
                        }
                    }
                }
            }

            // Sắp xếp `ConstraintErrors` sau khi hoàn tất
            ConstraintErrors = ConstraintErrors.OrderBy(e => e.Age).ToList();
        }

    }
}
