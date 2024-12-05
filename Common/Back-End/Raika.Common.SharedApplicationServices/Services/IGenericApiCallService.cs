namespace Raika.Common.SharedApplicationServices.Services
{
    public interface IGenericApiCallService
    {
        Task<TResponse> GetJsonApiAsync<TResponse>(string apiHost, string apiUrl, CancellationToken cancellationToken) where TResponse : class;
        Task<bool> PostJsonApiAsync<TRequest>(string apiHost, string apiUrl, TRequest dto, CancellationToken cancellationToken) where TRequest : class;
        Task<bool> PostJsonApiAsync(string apiHost, string apiUrl, object data, CancellationToken cancellationToken);
    }
}
