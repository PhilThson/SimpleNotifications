using Manager.Domain.DTOs;
using Manager.Domain.ViewModels;

namespace Manager.Helpers;

public static class MappingExtensions
{
    public static IEnumerable<NotificationViewModel> MapToListVM(
        this IEnumerable<NotificationDto> dtos)
    {
        foreach(var dto in dtos)
        {
            yield return new NotificationViewModel(dto);
        }
    }
}

