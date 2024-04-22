﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Domain.Models;
using V_Tube.Persisitence.DataBase;

namespace V_Tube.Persisitence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }
    }
}