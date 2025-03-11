/*
 * Stellar DataStore
 *
 * The Stellar DataStore service is your instantly available, secure and worry-free cloud data storage service. Sign-up now and minutes later, you can start putting your data in the cloud.
 *

 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace StellarDs.SDK.Model
{
    /// <summary>
    /// RevokeTokenRequest
    /// </summary>
    [DataContract(Name = "RevokeTokenRequest")]
    public partial class RevokeTokenRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeTokenRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected RevokeTokenRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeTokenRequest" /> class.
        /// </summary>
        /// <param name="refreshToken">refreshToken (required).</param>
        /// <param name="clientId">clientId (required).</param>
        /// <param name="clientSecret">clientSecret (required).</param>
        public RevokeTokenRequest(string refreshToken = default(string), Guid clientId = default(Guid), string clientSecret = default(string))
        {
            // to ensure "refreshToken" is required (not null)
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken is a required property for RevokeTokenRequest and cannot be null");
            }
            this.RefreshToken = refreshToken;
            this.ClientId = clientId;
            // to ensure "clientSecret" is required (not null)
            if (clientSecret == null)
            {
                throw new ArgumentNullException("clientSecret is a required property for RevokeTokenRequest and cannot be null");
            }
            this.ClientSecret = clientSecret;
        }

        /// <summary>
        /// Gets or Sets RefreshToken
        /// </summary>
        [DataMember(Name = "refresh_token", IsRequired = true, EmitDefaultValue = true)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or Sets ClientId
        /// </summary>
        [DataMember(Name = "client_id", IsRequired = true, EmitDefaultValue = true)]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or Sets ClientSecret
        /// </summary>
        [DataMember(Name = "client_secret", IsRequired = true, EmitDefaultValue = true)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RevokeTokenRequest {\n");
            sb.Append("  RefreshToken: ").Append(RefreshToken).Append("\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  ClientSecret: ").Append(ClientSecret).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // RefreshToken (string) minLength
            if (this.RefreshToken != null && this.RefreshToken.Length < 1)
            {
                yield return new ValidationResult("Invalid value for RefreshToken, length must be greater than 1.", new [] { "RefreshToken" });
            }

            // ClientSecret (string) minLength
            if (this.ClientSecret != null && this.ClientSecret.Length < 1)
            {
                yield return new ValidationResult("Invalid value for ClientSecret, length must be greater than 1.", new [] { "ClientSecret" });
            }

            yield break;
        }
    }

}
