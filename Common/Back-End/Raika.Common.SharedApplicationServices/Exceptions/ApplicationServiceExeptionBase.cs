namespace Raika.Common.SharedApplicationServices.Exceptions
{
    public class ApplicationServiceExeptionBase : Exception
    {
        string _message;
        public virtual new string Message => _message;
        public ApplicationServiceExeptionBase()
        {

        }
        public ApplicationServiceExeptionBase(string message) : base(message)
        {

        }
        public ApplicationServiceExeptionBase(string message, Exception innerException) : base(message, innerException)
        {

        }
        public ApplicationServiceExeptionBase(string message, string[] parameters)
        {
            _message = string.Format(message, parameters);
        }
    }
}
