﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.Core.Areas;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Database;
using Senparc.Ncf.XncfBase;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Xncf.WeixinManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Xncf.WeixinManager
{
    public partial class Register : IXncfDatabase  //注册 XNCF 模块数据库（按需选用）
    {
        #region IXncfDatabase 接口

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTag_WeixinUserConfigurationMapping());
            modelBuilder.ApplyConfiguration(new WeixinUserConfigurationMapping());
            modelBuilder.ApplyConfiguration(new UserTagConfigurationMapping());
        }

        public void AddXncfDatabaseModule(IServiceCollection services)
        {
            Console.WriteLine("-----------------AddXncfDatabaseModule for Weixin 1");
            services.AddScoped<MpAccount>();
            services.AddScoped<MpAccountDto>();
            services.AddScoped<MpAccount_CreateOrUpdateDto>();

            services.AddScoped<WeixinUser>();
            services.AddScoped<WeixinUserDto>();

            services.AddScoped<UserTag>();
            services.AddScoped<UserTag_WeixinUser>();

            //AutoMap映射不能在这里做，因为执行到此处时，相关过程已经执行完毕
            //base.AddAutoMapMapping(profile =>
            //{
            //});

        }

        public const string DATABASE_PREFIX = "WeixinManager_";

        public string DatabaseUniquePrefix => DATABASE_PREFIX;

        public Type XncfDatabaseDbContextType => typeof(WeixinSenparcEntities);

        public Type TryGetXncfDatabaseDbContextType => MultipleDatabasePool.Instance.GetXncfDbContextType(this);


        #endregion
    }
}
