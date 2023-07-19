using BMB.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Services.Abstractions
{
    public interface IUserService
    {
        void Add(User user);
        void Delete(string id);
        List<User> GetAll();
        User GetById(string id);

    }
}
