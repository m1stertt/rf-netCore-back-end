﻿using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User FindById(int id);
        User Update(User user);
        User DeleteById(int id);
    }
}