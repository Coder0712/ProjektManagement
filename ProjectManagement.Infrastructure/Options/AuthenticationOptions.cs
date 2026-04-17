namespace ProjectManagement.Infrastructure.Options
{
    /// <summary>
    /// Represents the authentication options.
    /// </summary>
    public sealed class AuthenticationOptions
    {
        /// <summary>
        /// Gets the authority.
        /// </summary>
        public string Authority { get; init; }

        /// <summary>
        /// Gets the issuer of the claim.
        /// </summary>
        public string ClaimIssuer { get; init; }

        /// <summary>
        /// Gets the meta data address.
        /// </summary>
        public string MetadataAddress { get; init; }

        /// <summary>
        /// Gets the requirement if https is required.
        /// </summary>
        public bool RequireHttpsMetadata { get; init; }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        public string ClientId { get; init; }
    }
}
