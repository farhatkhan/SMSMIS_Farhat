using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace SmsMisWeb.Filters
{
    public class DebugModelBinder //: DefaultModelBinder, IModelBinder
    {
        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    Dictionary<string, ModelMetadata> d = new Dictionary<string, ModelMetadata>(StringComparer.OrdinalIgnoreCase);
        //    foreach (var p in bindingContext.ModelMetadata.Properties)
        //    {
        //        var propertyName = p.PropertyName;
        //        try
        //        {
        //            d.Add(propertyName, null);
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            throw new ArgumentException(String.Format("The Item {0} as already been added", propertyName), ex);
        //        }
        //    }
        //    return base.BindModel(controllerContext, bindingContext);
        //}
    }
}