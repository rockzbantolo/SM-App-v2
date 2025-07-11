using ServiceDesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDesk.Core.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(int userId, string moduleName, string action);
        Task<List<Permission>> GetUserPermissionsAsync(int userId);
        Task<List<Module>> GetUserModulesAsync(int userId);
        Task<List<Permission>> GetAllPermissionsAsync();
        Task<List<Module>> GetAllModulesAsync();
    }
}
