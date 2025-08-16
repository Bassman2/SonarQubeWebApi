namespace SonarQubeWebApi.Service;


// https://sonarqube-pj-cos-1.ebgroup.elektrobit.com/web_api/api/server?deprecated=true&internal=true

internal class SonarQubeService(Uri host, IAuthenticator? authenticator, string appName)
    : JsonService(host, authenticator, appName, SourceGenerationContext.Default)
{
    private const int limit = 500;

    protected override string? AuthenticationTestUrl => "api/v1/hardware?limit=1&offset=0";

    //protected override async Task ErrorCheckAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    await base.ErrorCheckAsync(response, memberName, cancellationToken);

    //    string str = await response.Content.ReadAsStringAsync(cancellationToken);
    //    if (str.StartsWith("{\"status\":\"error\""))
    //    {
    //        //var res = await ReadFromJsonAsync<ResultModel>(response, cancellationToken);

    //        JsonTypeInfo<ResultModel> jsonTypeInfo = (JsonTypeInfo<ResultModel>)context.GetTypeInfo(typeof(ResultModel))!;
    //        var res = await response.Content.ReadFromJsonAsync<ResultModel>(jsonTypeInfo, cancellationToken);

    //        throw new WebServiceException(res!.Messages);
    //    }
    //}

    


    public async Task<string?> GetVersionAsync(CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetStringAsync("api/server/version", cancellationToken);
        return res;
    }
}
