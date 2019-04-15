using System.ComponentModel.DataAnnotations;

using MovieList.Properties;

namespace MovieList.Validation
{
    public class DefaultSeasonTitleAttribute : ValidationAttribute
    {
        public DefaultSeasonTitleAttribute()
        {
            this.ErrorMessageResourceName = "DefaultSeasonTitleInvalid";
            this.ErrorMessageResourceType = typeof(Messages);
        }

        public override bool IsValid(object value)
            => value is string title && title.Contains(Messages.DefaultSeasonNumberPlaceholder);
    }
}
