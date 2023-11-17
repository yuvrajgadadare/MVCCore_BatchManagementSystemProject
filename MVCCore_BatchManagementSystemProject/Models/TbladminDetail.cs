using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class TbladminDetail
{
    public int AdminId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
