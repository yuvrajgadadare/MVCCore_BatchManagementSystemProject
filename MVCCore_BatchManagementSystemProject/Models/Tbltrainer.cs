using System;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class Tbltrainer
{
    public int TrainerId { get; set; }

    public string? TrainerName { get; set; }

    public string? Qualification { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? Password { get; set; }

    public string? MobileNumber { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePhoto { get; set; }

    public virtual ICollection<Tblbatch> Tblbatches { get; set; } = new List<Tblbatch>();

    public virtual ICollection<TbltrainerTopic> TbltrainerTopics { get; set; } = new List<TbltrainerTopic>();
}
