namespace RokuECP
{
    public class RokuApp(long appId, string appType, string appVersion, string appName)
    {
        public long AppId { get; private set; } = appId;
        public string AppType { get; private set; } = appType;
        public string AppVersion { get; private set; } = appVersion;
        public string AppName { get; private set; } = appName;
    }
}