using System.Net.Mime;
using Core.HttpLogic.HttpBodyLogic.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.HttpLogic.HttpBodyLogic;

internal class JsonUnpackerHttpResponse : IUnpackerHttpResponse
{
    public string ContentType => MediaTypeNames.Application.Json;
    
    private readonly JsonSerializerSettings _jsonDeserializationSettings = new()
    {
        ContractResolver = new DefaultContractResolver { NamingStrategy = new DefaultNamingStrategy() }
    };

    public async Task<TResult?> ExtractContentAsync<TResult>(HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<TResult>(json, _jsonDeserializationSettings);
    }
}