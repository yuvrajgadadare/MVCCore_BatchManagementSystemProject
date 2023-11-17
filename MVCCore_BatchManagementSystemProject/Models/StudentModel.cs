namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentModel
    {
      //  public TblstudentDetail studentdetails { get; set; }
        public TblstudentRegistration registration {  get; set; }
        public List<Tblbatch> batches { get; set; }
    }
}
