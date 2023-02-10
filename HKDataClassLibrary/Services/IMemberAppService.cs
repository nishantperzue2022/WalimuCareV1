using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HKDataClassLibrary.Services
{
    public interface IMemberAppService
    {
        Task<bool> AddMember();
    }
}
