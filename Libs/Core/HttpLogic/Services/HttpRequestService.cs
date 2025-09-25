using Core.HttpLogic.Dto;
using Core.HttpLogic.HttpBodyLogic.Interfaces;
using Core.HttpLogic.Services.Interfaces;
using ITraceWriter = Core.Traces.TraceLogic.Interfaces.ITraceWriter;

namespace Core.HttpLogic.Services;

internal class HttpRequestService : IHttpRequestService
{
    private readonly IHttpConnectionService _httpConnectionService;
    private readonly IEnumerable<IUnpackerHttpResponse> _responseUnpackerList;
    private readonly IEnumerable<IHttpContentPacker> _contentPackerList;
    private readonly IEnumerable<ITraceWriter> _traceWriterList;

    public HttpRequestService(
        IHttpConnectionService httpConnectionService,
        IEnumerable<IUnpackerHttpResponse> responseUnpackerList,
        IEnumerable<IHttpContentPacker> contentPackerList, IEnumerable<ITraceWriter> traceWriterList)
    {
        _httpConnectionService = httpConnectionService;
        _responseUnpackerList = responseUnpackerList;
        _contentPackerList = contentPackerList;
        _traceWriterList = traceWriterList;
    }

    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData)
    {
        var client = _httpConnectionService.CreateHttpClient(connectionData);

        var requestMessage = ConvertToHttpRequestMessage(requestData);

        foreach (var traceWriter in _traceWriterList)
        {
            requestMessage.Headers.Add(traceWriter.Name, traceWriter.GetValue());
        }
        
        var response = await _httpConnectionService.SendRequestAsync(requestMessage, client, connectionData.CancellationToken);
        
        return new HttpResponse<TResponse>
        {
            StatusCode = response.StatusCode,
            Body = await ExtractContentAsync<TResponse>(response),
            Headers = response.Headers,
        };
    }

    private HttpRequestMessage ConvertToHttpRequestMessage(HttpRequestData requestData)
    {
        var uriBuilder = new UriBuilder(requestData.Uri);
        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var keyValuePair in requestData.QueryParameterList)
            query.Add(keyValuePair.Key, keyValuePair.Value);

        uriBuilder.Query = query.ToString();
        
        var requestMessage = new HttpRequestMessage(requestData.Method, uriBuilder.Uri);
        requestMessage.Content = PackContent(requestData.Body, requestData.ContentType);
        foreach (var keyValuePair in requestData.HeaderDictionary)
            requestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);

        return requestMessage;
    }

    private async Task<TResponse?> ExtractContentAsync<TResponse>(HttpResponseMessage response)
    {
        var contentType = response.Content.Headers.ContentType?.MediaType;

        foreach (var responseUnpacker in _responseUnpackerList)
            if (responseUnpacker.ContentType == contentType)
                return await responseUnpacker.ExtractContentAsync<TResponse>(response);
        
        throw new InvalidOperationException(
            $"Couldn't find an {nameof(IUnpackerHttpResponse)} for working with ContentType = {contentType}.");
    }

    private HttpContent PackContent(object body, string contentType)
    {
        foreach (var contentPacker in _contentPackerList)
            if (contentPacker.ContentType == contentType)
                return contentPacker.PackContent(body);
        
        throw new InvalidOperationException(
            $"Couldn't find an {nameof(IHttpContentPacker)} for working with ContentType = {contentType}.");
    }
}