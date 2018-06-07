using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc
{
    public class ProductsListViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            

            var result = (ProductsListViewModel)base.BindModel(controllerContext, bindingContext);
            result.PageNumber--;

            if (result.PageNumber < 0)
                result.PageNumber = 0;

            result.PageSize = AppConfiguration.DefaultPageSize;

            return result;
        }
    }
}