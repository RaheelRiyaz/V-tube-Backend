﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.DTO;
using V_Tube.Domain.Models;
using V_Tube.Persisitence.DataBase;
using V_Tube.Persisitence.Repositories;

namespace V_Tube.Persisitence.LinqMethods
{
    public static class DapperExtensions
    {
        public async static Task<IEnumerable<T>> QueryAsync<T>
            (
            this DbContext dbContext,
            string sql,
            object? param = default,
            DbTransaction? transaction = default,
            int? commandTimeout = default,
            CommandType commandType = CommandType.Text
            )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());
            return await sqlConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async static Task<int> ExecuteAsync
           (
           this DbContext dbContext,
           string sql,
           object? param = default,
           DbTransaction? transaction = default,
           int? commandTimeout = default,
           CommandType commandType = CommandType.Text
           )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());
            return await sqlConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }




        public async static Task<T?> FirstOrdefaultAsync<T>
            (
            this DbContext dbContext,
            string sql,
            object? param = default,
            DbTransaction? transaction = default,
            int? commandTimeout = default,
            CommandType commandType = CommandType.Text
            )
        {
            using SqlConnection sqlConnection = new SqlConnection(dbContext.Database.GetConnectionString());
            return await sqlConnection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
