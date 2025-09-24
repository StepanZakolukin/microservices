using Core.HttpLogic.Dto;
using Core.HttpLogic.Services.Interfaces;
using NotificationService.ConnectionLib.Interfaces;

namespace NotificationService.ConnectionLib;

public class NotificationServiceConnection : INotificationServiceConnection
{
    private const string BaseUrl = "https://localhost:5003/api";
    private readonly IHttpRequestService _httpRequestService;

    public NotificationServiceConnection(IHttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }
    
    public async Task CreateNotificationAsync(NotificationInfo notificationInfo, CancellationToken cancellationToken)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Post,
            Uri = CombineUri(BaseUrl, "notifications"),
        };

        var connectionData = new HttpConnectionData
        {
            ClientName = "NotificationService",
            CancellationToken = cancellationToken
        };

        var response = await _httpRequestService.SendRequestAsync<string>(requestData, connectionData);
        
        if (response.IsSuccessStatusCode) return;
        
        throw new HttpRequestException($"The notification creation request failed. Microservice response: {response}.");
    }

    private string CombineUri(string baseUri, string relativeUri)
    {
        return new Uri(new Uri(baseUri), relativeUri).ToString();
    }
}