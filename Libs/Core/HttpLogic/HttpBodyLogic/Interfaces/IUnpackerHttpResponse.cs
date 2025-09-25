﻿namespace Core.HttpLogic.HttpBodyLogic.Interfaces;

internal interface IUnpackerHttpResponse
{
    string ContentType { get; }
    
    Task<TResult?> ExtractContentAsync<TResult>(HttpResponseMessage response);
}