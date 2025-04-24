using FileManager.Api.Contracts.Common;
using FluentValidation;

namespace FileManager.Api.Contracts
{
    public class UploadManyFilesRequestValidator :AbstractValidator<UploadManyFilesRequest>
    {
        public UploadManyFilesRequestValidator()
        {
            RuleForEach(X => X.Files).SetValidator(new FileSizeValidator());
            RuleForEach(X => X.Files).SetValidator(new BlockedSignaturesValidator());
            RuleForEach(X => X.Files).SetValidator(new FileNameValidator());
        }
    }
}
