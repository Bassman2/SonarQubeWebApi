namespace SonarQubeWebApiUnitTest;

[TestClass]
public class SonarQubeVersionUnitTest : SonarQubeBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var sonarQube = new SonarQube(storeKey, appName);

        var version = await sonarQube.GetVersionAsync();

        Assert.IsNotNull(version, "SonarQube version is null");
        Assert.AreEqual(new Version(2025,1,1,104738), version, nameof(version));
    }
}
