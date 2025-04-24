using Azure.Core;
using FileManager.Api.Contracts.Common;
using FileManager.Api.Settings;
using FluentValidation;

namespace FileManager.Api.Contracts
{
    public class UploadFileRequestValidator :AbstractValidator<UploadFileRequest>
    {
        public UploadFileRequestValidator()
        {

            RuleFor(x => x.File)
              //.SetValidator(new FileSizeValidator())
              .SetValidator(new BlockedSignaturesValidator())
              .SetValidator(new FileNameValidator());



        }
    }
}
