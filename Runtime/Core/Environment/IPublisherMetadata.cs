namespace Chartboost.Core.Environment
{
    #nullable enable
    /// <summary>
    /// Publisher-provided metadata.
    /// </summary>
    public interface IPublisherMetadata
    {
        /// <summary>
        /// Indicates if the user is underage as determined by the publisher.
        /// </summary>
        /// <param name="isUserUnderage">Indicates if user is underage.</param>
        void SetIsUserUnderage(bool isUserUnderage);
        
        /// <summary>
        /// Sets a publisher-defined session identifier.
        /// </summary>
        /// <param name="publisherSessionIdentifier">Session identifier to be set.</param>
        void SetPublisherSessionIdentifier(string? publisherSessionIdentifier);
        
        /// <summary>
        /// Sets a publisher-defined application identifier.
        /// </summary>
        /// <param name="publisherAppIdentifier">App identifier to be set.</param>
        void SetPublisherAppIdentifier(string? publisherAppIdentifier);
        
        /// <summary>
        /// Sets the framework name for this app. 
        /// </summary>
        /// <param name="frameworkName">e.g. “Unity”.</param>
        void SetFrameworkName(string? frameworkName);
        
        /// <summary>
        /// Sets the framework version for this app.
        /// </summary>
        /// <param name="frameworkVersion">Framework version to be set.</param>
        void SetFrameworkVersion(string? frameworkVersion);
        
        /// <summary>
        /// Sets a publisher-defined player identifier.
        /// </summary>
        /// <param name="playerIdentifier">New player identifier.</param>
        void SetPlayerIdentifier(string? playerIdentifier);
    }
    #nullable disable
}
