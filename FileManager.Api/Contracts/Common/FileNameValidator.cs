using FileManager.Api.Settings;
using FluentValidation;

namespace FileManager.Api.Contracts.Common
{
    public class FileNameValidator :AbstractValidator<IFormFile>
    {
        public FileNameValidator()
        {
            RuleFor(x => x.FileName)
               .Matches(FileSettings.AllowedPattern)
               .WithMessage("File name must not contain '/'.")
               .When(x => x is not null);

        }
    }
}
