using Microsoft.AspNetCore.Mvc.ModelBinding;
using UdemyNet8.api.model;

namespace UdemyNet8.CustomModelBinder
{
    public class BookCustomBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Book book = new Book();
            if (bindingContext.ValueProvider.GetValue("Title").Count() > 0)
            {
                book.Sign += bindingContext.ValueProvider.GetValue("Title").FirstValue;
                if(bindingContext.ValueProvider.GetValue("Author").Count() > 0)
                {
                    book.Sign += bindingContext.ValueProvider.GetValue("Author").FirstValue;
                }
            }
            bindingContext.Result = ModelBindingResult.Success(book);
            return Task.CompletedTask;
        }
    }
}
