﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace timesheet.Models;

public partial class Timesheet
{
    public int TimesheetId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime Date { get; set; }

    public decimal HoursWorked { get; set; }

    public string Status { get; set; }


}