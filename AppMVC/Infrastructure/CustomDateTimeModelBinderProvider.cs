using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        CustomDateTimeModelBinder binder = null;
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (binder == null)
                binder = new CustomDateTimeModelBinder(new SimpleTypeModelBinder(typeof(DateTime),
                    context.Services.GetRequiredService<ILoggerFactory>()));

            return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
        }
    }
}
