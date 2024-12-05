using Raika.Common.SharedApplicationServices.Common;

namespace Raika.Common.SharedApplicationServices.Services
{
    public interface IQueryBase
    {
        abstract QueryResponseBase SetQueryResponseWithError();
    }

}
