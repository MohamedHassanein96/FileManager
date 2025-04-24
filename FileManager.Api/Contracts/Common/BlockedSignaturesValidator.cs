using FileManager.Api.Settings;
using FluentValidation;

namespace FileManager.Api.Contracts.Common
{
    public class BlockedSignaturesValidator :AbstractValidator<IFormFile>
    {
        public BlockedSignaturesValidator()
        {

            RuleFor(x => x)
                .Must((request) =>
                {
                    BinaryReader binary = new(request.OpenReadStream());
                    var bytes = binary.ReadBytes(2);

                    var fileSquenceHex = BitConverter.ToString(bytes).ToUpper();

                    return !FileSettings.bLockedSignature.Contains(fileSquenceHex);

                    //return !FileSettings.bLockedSignature
                    //       .Any(x => x.Equals(fileSquenceHex, StringComparison.OrdinalIgnoreCase));


                }).WithMessage("Not allowed file content")
                .When(x => x is not null);
        }
    }
}
