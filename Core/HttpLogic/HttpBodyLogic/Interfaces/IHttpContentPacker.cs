namespace Core.HttpLogic.HttpBodyLogic.Interfaces;

internal interface IHttpContentPacker
{
    string ContentType { get; }
    
    HttpContent PackContent(object body);
}