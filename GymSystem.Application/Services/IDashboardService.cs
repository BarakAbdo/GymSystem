using GymSystem.Application.Dtos.DashboardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.Application.Services
{
    public interface IDashboardService
    {
        DashboardDto GetDashboardData();
    }
}
