using System.Runtime.CompilerServices;

namespace Raika.Common.SharedApplicationServices.Common
{
    public class CommandResponseBase
    {
        public bool Success { get; set; }
        public bool IsDuplicateData { get; set; } = false;
        public bool NotFound { get; set; } = false;
        public Exception Exception { get; set; }
    }
}
