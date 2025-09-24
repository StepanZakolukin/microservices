namespace Core.HttpLogic.Dto;

public record HttpResponse<TResponse> : BaseHttpResponse
{
    public TResponse? Body { get; set; }
}