namespace SonarQubeWebApi;

public sealed class SonarQube : JsonService
{
    ///// <summary>
    ///// Initializes a new instance of the <see cref="SonarQube"/> class using a store key and application name.
    ///// </summary>
    ///// <param name="storeKey">The key to retrieve the host and token from the key store.</param>
    ///// <param name="appName">The name of the application using the API.</param>
    public SonarQube(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }
       
    public SonarQube(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Configures the provided <see cref="HttpClient"/> instance with specific default headers required for API requests.
    /// This includes setting the User-Agent, Accept, and API version headers.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> to configure for GitHub API usage.</param>
    /// <param name="appName">The name of the application, used as the User-Agent header value.</param>
    protected override void InitializeClient(HttpClient client, string appName)
    {
        client.DefaultRequestHeaders.Add("User-Agent", appName);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    protected override string? AuthenticationTestUrl => "api/server/version";

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

    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetStringAsync("api/server/version", cancellationToken);
        return res;
    }
}

