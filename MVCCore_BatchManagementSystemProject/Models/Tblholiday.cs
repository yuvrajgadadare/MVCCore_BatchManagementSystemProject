using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class Tblholiday
{
    public int HolidayId { get; set; }

    public DateTime? HolidayDate { get; set; }

    public string? HolidayPurpose { get; set; }
}
