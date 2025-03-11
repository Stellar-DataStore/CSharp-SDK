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
    /// ProjectRequest
    /// </summary>
    [DataContract(Name = "ProjectRequest")]
    public partial class ProjectRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected ProjectRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRequest" /> class.
        /// </summary>
        /// <param name="name">name (required).</param>
        /// <param name="description">description.</param>
        /// <param name="isMultitenant">isMultitenant (required).</param>
        /// <param name="allowNewUsers">allowNewUsers (required).</param>
        public ProjectRequest(string name = default(string), string description = default(string), bool isMultitenant = default(bool), bool allowNewUsers = default(bool))
        {
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for ProjectRequest and cannot be null");
            }
            this.Name = name;
            this.IsMultitenant = isMultitenant;
            this.AllowNewUsers = allowNewUsers;
            this.Description = description;
        }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = true)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets IsMultitenant
        /// </summary>
        [DataMember(Name = "isMultitenant", IsRequired = true, EmitDefaultValue = true)]
        public bool IsMultitenant { get; set; }

        /// <summary>
        /// Gets or Sets AllowNewUsers
        /// </summary>
        [DataMember(Name = "allowNewUsers", IsRequired = true, EmitDefaultValue = true)]
        public bool AllowNewUsers { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProjectRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  IsMultitenant: ").Append(IsMultitenant).Append("\n");
            sb.Append("  AllowNewUsers: ").Append(AllowNewUsers).Append("\n");
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
            // Name (string) maxLength
            if (this.Name != null && this.Name.Length > 100)
            {
                yield return new ValidationResult("Invalid value for Name, length must be less than 100.", new [] { "Name" });
            }

            // Name (string) minLength
            if (this.Name != null && this.Name.Length < 1)
            {
                yield return new ValidationResult("Invalid value for Name, length must be greater than 1.", new [] { "Name" });
            }

            yield break;
        }
    }

}
