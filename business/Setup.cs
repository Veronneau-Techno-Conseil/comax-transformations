using CommunAxiom.Transformations.AppModel.Business;
using CommunAxiom.Transformations.Business.Module;
using CommunAxiom.Transformations.Business.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.Business
{
    public static class Setup
    {
        public static void Configure(IServiceCollection sc)
        {
            sc.AddTransient<IValidator<Contracts.Module>, ModuleValidator>();
            sc.AddTransient<IModuleBusiness, ModuleBusiness>();

            sc.AddTransient<IValidator<Contracts.User>, UserValidator>();
            sc.AddTransient<IUserBusiness, UserBusiness>();
        }
    }
}
