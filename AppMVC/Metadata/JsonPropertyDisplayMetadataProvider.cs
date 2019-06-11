using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Metadata
{
    public class JsonPropertyDisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var attributes = context.Attributes;
            var jsonPropertyAttribute = attributes.OfType<JsonPropertyAttribute>().FirstOrDefault();

            if (jsonPropertyAttribute == null)
                return;

            var displayMetadata = context.DisplayMetadata;
            displayMetadata.DisplayName = () => jsonPropertyAttribute.PropertyName;
        }
    }
}
