﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// student model this view model represents
        /// </summary>
        private Student _student { get; init; }

        public string FirstName => _student.FirstName;

        public string LastName => _student.LastName;

        public double GPA => _student.GPA;

        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if ((cr.CourseName.Contains("CIS")))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }

                }
                return points / hours;
            }
        }

        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }


        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            _student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
