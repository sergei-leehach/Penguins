using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;

namespace SiteDevelopment.Repository
{
    public class TagsPropertyBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
            System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "Bundle")
            {
                string value = controllerContext.HttpContext.Request.Form["input-tags"];
                string[] split = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                List<Bundle> bundleList = new List<Bundle>();
                foreach (var str in split)
                {
                    int Id;
                    if (int.TryParse(str, out Id))
                    {
                        Bundle bundle = new Bundle();
                        bundle.TagId = Id;
                        bundleList.Add(bundle);
                    }
                    else
                    {
                        string s = str.Substring(1);
                        string title = s.Trim();

                        if (!String.IsNullOrEmpty(title))
                        {
                            Tag tag = new Tag();
                            tag.Title = title;
                            Bundle bundle = new Bundle();
                            bundle.Tag = tag;
                            bundleList.Add(bundle);
                        }
                    }
                }
                propertyDescriptor.SetValue(bindingContext.Model, bundleList);
            }
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}