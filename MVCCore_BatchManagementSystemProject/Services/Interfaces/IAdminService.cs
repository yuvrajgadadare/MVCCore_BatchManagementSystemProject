using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface IAdminService
    {
         TbladminDetail CheckLogin(string user_name,string password);
    }
}
