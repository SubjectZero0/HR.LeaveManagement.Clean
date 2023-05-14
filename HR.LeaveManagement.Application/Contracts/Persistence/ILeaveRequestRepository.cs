﻿using HR.LeaveManagement.Domain;
using System.Security.Claims;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        public Task<LeaveRequest?> GetLeaveRequestWithDetails(int id);

        public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();

        public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string employeeId);
    }
}