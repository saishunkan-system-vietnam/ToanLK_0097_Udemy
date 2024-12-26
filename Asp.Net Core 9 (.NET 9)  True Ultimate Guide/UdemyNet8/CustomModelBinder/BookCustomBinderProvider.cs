using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using UdemyNet8.api.model;

namespace UdemyNet8.CustomModelBinder
{
    public class BookCustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Book))
            {
                return new BinderTypeModelBinder(typeof(BookCustomBinder));
            }
            return null;
        }
    }
}
