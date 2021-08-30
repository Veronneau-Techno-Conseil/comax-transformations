using CommunAxiom.Transformations.Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Helpers
{
    public static class ExtensionMethods
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj)
        where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = Enum.GetName(typeof(TEnum), x),
                Value = (Convert.ToInt32(x))
                .ToString()
            }), "Value", "Text");
        }

        public static void SetErrors(this Controller controller, ValidationResult result)
        {
            var localizer = controller.HttpContext.RequestServices.GetService<IStringLocalizer<SharedResources>>();

            foreach (var item in result.Errors)
                controller.ModelState.AddModelError(item.PropertyName, localizer[item.ErrorCode, item.PropertyName, item.CustomState]);
        }

        public static void SetErrors<TRes>(this Controller controller, Result<TRes> result)
        {
            var localizer = controller.HttpContext.RequestServices.GetService<IStringLocalizer<SharedResources>>();

            foreach (var item in result.ValidationResult.Errors)
                controller.ModelState.AddModelError(item.PropertyName, localizer[item.ErrorCode, item.PropertyName, item.CustomState]);
        }

        public static void SetErrors<TRes>(this Controller controller, Exception exception)
        {
            var localizer = controller.HttpContext.RequestServices.GetService<IStringLocalizer<SharedResources>>();
            controller.ModelState.AddModelError("", localizer["UnexpectedError", exception]);
        }
    }
}
