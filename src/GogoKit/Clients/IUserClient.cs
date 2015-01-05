﻿using System.Threading.Tasks;
using GogoKit.RequestModels;
using GogoKit.Resources;

namespace GogoKit.Clients
{
    public interface IUserClient
    {
        Task<User> GetAsync();
        Task<User> UpdateAsync(UserUpdate userUpdate);
    }
}
