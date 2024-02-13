namespace Manager.Interfaces;

public interface IHttpClientService
{
    Task<IEnumerable<T>> GetAllNotificationsAsync<T>();
}