using Raika.Common.SharedApplicationServices.Common;

namespace Raika.Common.SharedApplicationServices.Services
{
    public interface ICommandBase
    {
        protected abstract CommandResponseBase SetResponseWithError(CommandResponseBase response, Exception exeption);
    }

}
