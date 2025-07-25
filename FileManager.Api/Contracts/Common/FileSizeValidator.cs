﻿using FileManager.Api.Settings;
using FluentValidation;

namespace FileManager.Api.Contracts.Common
{
    public class FileSizeValidator :AbstractValidator<IFormFile>
    {
        public FileSizeValidator()
        {

            RuleFor(x => x)
                .Must((request, context) => request.Length <= FileSettings.MaxFileSizeInBytes)
                .WithMessage($"Max File size is {FileSettings.MaxFileSizeInMB} MB.")
                .When(x => x is not null);

        }
    }
}
