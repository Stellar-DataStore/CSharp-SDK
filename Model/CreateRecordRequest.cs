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
    /// CreateRecordRequest
    /// </summary>
    [DataContract(Name = "CreateRecordRequest")]
    public partial class CreateRecordRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRecordRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected CreateRecordRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRecordRequest" /> class.
        /// </summary>
        /// <param name="records">records (required).</param>
        public CreateRecordRequest(List<Dictionary<string, object?>>? records = default)
        {
            // to ensure "records" is required (not null)
            this.Records = records ?? throw new ArgumentNullException("records is a required property for CreateRecordRequest and cannot be null");
        }

        /// <summary>
        /// Gets or Sets Records
        /// </summary>
        [DataMember(Name = "records", IsRequired = true, EmitDefaultValue = true)]
        public List<Dictionary<string, Object>> Records { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateRecordRequest {\n");
            sb.Append("  Records: ").Append(Records).Append("\n");
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
            yield break;
        }
    }

}
