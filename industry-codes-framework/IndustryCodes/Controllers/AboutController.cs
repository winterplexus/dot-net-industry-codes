﻿//
//  AboutController.cs
//
//  Copyright (c) Wiregrass Code Technology 2014-2019
//
using System;
using System.Globalization;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using IndustryCodes.ViewModels;

namespace IndustryCodes.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult About()
        {
            var model = new AboutViewModel();

            SetAboutValues(model);

            return View(model);
        }

        private static void SetAboutValues(AboutViewModel model)
        {
            var baseType = BuildManager.GetGlobalAsaxType().BaseType;
            if (baseType == null)
            {
                return;
            }

            var assembly = baseType.Assembly;

            var assemblyName = assembly.GetName();
            model.Version = Convert.ToString(assemblyName.Version, CultureInfo.CurrentCulture);

            var titleAttributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (titleAttributes.Length > 0)
            {
                model.Application = ((AssemblyTitleAttribute)titleAttributes[0]).Title;
            }
            var descriptionAttributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (descriptionAttributes.Length > 0)
            {
                model.Description = ((AssemblyDescriptionAttribute)descriptionAttributes[0]).Description;
            }
            var copyrightAttributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (copyrightAttributes.Length > 0)
            {
                model.Copyright = ((AssemblyCopyrightAttribute)copyrightAttributes[0]).Copyright;
            }
        }
    }
}