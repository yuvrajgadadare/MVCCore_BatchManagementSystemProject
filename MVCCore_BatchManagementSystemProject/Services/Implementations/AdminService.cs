using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class AdminService : IAdminService
    {
        BatchManagementDbContext _dbcontext;
        public AdminService(BatchManagementDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public TbladminDetail CheckLogin(string user_name, string password)
        {
            TbladminDetail ad = _dbcontext.TbladminDetails.ToList().FirstOrDefault(e => e.UserName.Equals(user_name) & e.Password.Equals(password));
            return ad;
        }
    }
}