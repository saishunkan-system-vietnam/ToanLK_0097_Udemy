using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UdemyNet8.api.model
{
    public class Book
    {
        public int Id { get; set; }
        [Remote("RemoteAlternativeRoute", "ModelBindingAndValidationController","Title is invalid")]
        public string? Title { get; set; }
        public long NumberOfCharacter { get; set; }

        [BindNever]
        public string? Author { get; set; }
        public string? Sign { get; set; }
       
        public override string ToString()
        {
            return $"{Id}-{(Title == null ? "<unknown>":Title)}-{NumberOfCharacter}-{this.Author}-{Sign}";
        }
    }
}
