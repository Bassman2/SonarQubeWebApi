namespace SonarQubeWebApi;

public class SonarQube : IDisposable
{
    private SonarQubeService? service;

    /// <summary>
    /// Initializes a new instance of the <see cref="SonarQube"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key to retrieve the host and token from the key store.</param>
    /// <param name="appName">The name of the application using the API.</param>
    public SonarQube(string storeKey, string appName)
    : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SnipeIT"/> class using a host URI, token, and application name.
    /// </summary>
    /// <param name="host">The base URI of the Snipe-IT API.</param>
    /// <param name="token">The authentication token for the API.</param>
    /// <param name="appName">The name of the application using the API.</param>
    public SonarQube(Uri host, string token, string appName)
    {
        service = new(host, new BearerAuthenticator(token), appName);
    }

    /// <summary>
    /// Disposes the resources used by the <see cref="SnipeIT"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    public async Task<Version?> GetVersionAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetVersionAsync(cancellationToken);
        return new Version(res!);
    }
}
