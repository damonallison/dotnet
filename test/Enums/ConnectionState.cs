namespace DamonAllison.CSharpTests.Enums
{
    /// <summary>
    /// Enums should be named as a singular (ConnectionState, BackupType).
    /// Bit flags enums should be named plural (ConnectionOptions, ReadFlags)
    /// 
    /// By default, the underlying enum type is `int`. You can specify any integral
    /// type as the enum's underlying type.
    /// </summary>
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
        Error
    }
}