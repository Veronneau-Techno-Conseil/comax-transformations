﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace CommunAxiom.Transformations.DAL
{
    public static class ServiceProviderExtensions
    {
        public static Task<TResult> WithContext<TResult>(this IServiceProvider serviceProvider, Func<Models.TransformationsDbContext, Task<TResult>> action)
        {
            return Task.Run(async () =>
            {
                using var scope = serviceProvider.CreateScope();
                using (var ctxt = scope.ServiceProvider.GetService<Models.TransformationsDbContext>())
                {
                    return await action(ctxt);
                }
            });
        }

        public static TResult WithContext<TResult>(this IServiceProvider serviceProvider, Func<Models.TransformationsDbContext, TResult> action)
        {
            using var scope = serviceProvider.CreateScope();
            using (var ctxt = scope.ServiceProvider.GetService<Models.TransformationsDbContext>())
            {
                return action(ctxt);
            }
        }

        public static Task WithContext(this IServiceProvider serviceProvider, Func<Models.TransformationsDbContext, Task> action)
        {
            return Task.Run(async () =>
            {
                using var scope = serviceProvider.CreateScope();
                using (var ctxt = scope.ServiceProvider.GetService<Models.TransformationsDbContext>())
                {
                    await action(ctxt);
                }
            });
        }

        public static void WithContext(this IServiceProvider serviceProvider, Action<Models.TransformationsDbContext> action)
        {
            using var scope = serviceProvider.CreateScope();
            using (var ctxt = scope.ServiceProvider.GetService<Models.TransformationsDbContext>())
            {
                action(ctxt);
            }
        }

        public static async IAsyncEnumerable<TResult> WithContext<TResult>(this IServiceProvider serviceProvider, Func<Models.TransformationsDbContext, IAsyncEnumerable<TResult>> action)
        {
            using var scope = serviceProvider.CreateScope();
            using (var ctxt = scope.ServiceProvider.GetService<Models.TransformationsDbContext>())
            {
                await foreach (var item in action(ctxt))
                    yield return item;
            }
        }
    }
}
