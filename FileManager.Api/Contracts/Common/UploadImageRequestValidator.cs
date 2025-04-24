using FileManager.Api.Settings;
using FluentValidation;

namespace FileManager.Api.Contracts.Common
{
    public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
    {
        public UploadImageRequestValidator()
        {

            RuleFor(X => X.Image)
                .SetValidator(new FileSizeValidator())
                .SetValidator(new BlockedSignaturesValidator());



            RuleFor(x => x.Image)
                .Must((request, context) =>
                {

                    var extension = Path.GetExtension(request.Image.FileName.ToLower());
                    return FileSettings.AllowedImagesExtensions.Contains(extension);

                }).WithMessage("File extension is not allowed").When(x => x.Image is not null);
        }
    }
}
