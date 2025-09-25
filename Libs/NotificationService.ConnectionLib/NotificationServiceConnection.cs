using Core.HttpLogic.Dto;
using Core.HttpLogic.Services.Interfaces;
using NotificationService.ConnectionLib.Interfaces;

namespace NotificationService.ConnectionLib;

internal class NotificationServiceConnection : INotificationServiceConnection
{
    private const string BaseUrl = "https://localhost:5003/api";
    private readonly IHttpRequestService _httpRequestService;

    public NotificationServiceConnection(IHttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }
    
    public async Task<Guid> CreateNotificationAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken)
    {
        var requestData = new HttpRequestData
        {
            Method = HttpMethod.Post,
            Uri = CombineUri(BaseUrl, "notifications"),
            Body = notificationRequest
        };

        var connectionData = new HttpConnectionData
        {
            ClientName = "NotificationService",
            CancellationToken = cancellationToken
        };

        var response = await _httpRequestService.SendRequestAsync<Guid>(requestData, connectionData);
        
        if (response.IsSuccessStatusCode) return response.Body;
        
        throw new HttpRequestException($"The notification creation request failed. Microservice response: {response}.");
    }

    private string CombineUri(string baseUri, string relativeUri)
    {
        return new Uri(new Uri(baseUri), relativeUri).ToString();
    }
}