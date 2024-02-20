namespace Manager.Interfaces;

public interface IHttpClientService
{
    Task<IEnumerable<T>> GetAllNotificationsAsync<T>();
    Task<IEnumerable<T>> GetConnectedUsersAsync<T>();
}