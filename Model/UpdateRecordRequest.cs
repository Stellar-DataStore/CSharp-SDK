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
    /// UpdateRecordRequest
    /// </summary>
    [DataContract(Name = "UpdateRecordRequest")]
    public partial class UpdateRecordRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRecordRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected UpdateRecordRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRecordRequest" /> class.
        /// </summary>
        /// <param name="idList">idList.</param>
        /// <param name="record">record (required).</param>
        public UpdateRecordRequest(List<Object> idList = default(List<Object>), Dictionary<string, Object> record = default(Dictionary<string, Object>))
        {
            // to ensure "record" is required (not null)
            if (record == null)
            {
                throw new ArgumentNullException("record is a required property for UpdateRecordRequest and cannot be null");
            }
            this.Record = record;
            this.IdList = idList;
        }

        /// <summary>
        /// Gets or Sets IdList
        /// </summary>
        [DataMember(Name = "idList", EmitDefaultValue = true)]
        public List<Object> IdList { get; set; }

        /// <summary>
        /// Gets or Sets Record
        /// </summary>
        [DataMember(Name = "record", IsRequired = true, EmitDefaultValue = true)]
        public Dictionary<string, Object> Record { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UpdateRecordRequest {\n");
            sb.Append("  IdList: ").Append(IdList).Append("\n");
            sb.Append("  Record: ").Append(Record).Append("\n");
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
