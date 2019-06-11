using AppMVC.Areas.Store.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Infrastructure
{
    public class EventModelBinderProvider : IModelBinderProvider
    {
        IModelBinder binder = null;

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (binder == null)
                binder = new EventModelBinder();

            return context.Metadata.ModelType == typeof(Event) ? binder : null;
        }
    }
}
