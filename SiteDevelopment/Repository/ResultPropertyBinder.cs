using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class EnumPropertyBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {

            if (propertyDescriptor.Name == "TypeOfBoard")
            {
                TypeOfBoard tob = (TypeOfBoard)Enum.Parse(typeof(TypeOfBoard), controllerContext.HttpContext.Request.Form["type"]);
                propertyDescriptor.SetValue(bindingContext.Model, tob);
                return;
            }

            if (propertyDescriptor.Name == "Result")
            {
                var request = controllerContext.HttpContext.Request.Form["result"];
                if (request != null)
                {
                    TypeOfResult tor = (TypeOfResult)Enum.Parse(typeof(TypeOfResult), request);
                    propertyDescriptor.SetValue(bindingContext.Model, tor);
                    return;
                }
            }

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}