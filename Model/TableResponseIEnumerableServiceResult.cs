/*
 * Stellar DataStore
 *
 * The Stellar DataStore service is your instantly available, secure and worry-free cloud data storage service. Sign-up now and minutes later, you can start putting your data in the cloud.
 *

 */


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace StellarDs.SDK.Model
{
    /// <summary>
    /// TableResponseIEnumerableServiceResult
    /// </summary>
    [DataContract(Name = "TableResponseIEnumerableServiceResult")]
    public partial class TableResponseIEnumerableServiceResult : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableResponseIEnumerableServiceResult" /> class.
        /// </summary>
        /// <param name="messages">messages.</param>
        /// <param name="data">data.</param>
        public TableResponseIEnumerableServiceResult(List<ServiceMessage> messages = default(List<ServiceMessage>), List<TableResponse> data = default(List<TableResponse>))
        {
            this.Messages = messages;
            this.Data = data;
        }

        /// <summary>
        /// Gets or Sets Messages
        /// </summary>
        [DataMember(Name = "messages", EmitDefaultValue = true)]
        public List<ServiceMessage> Messages { get; set; }

        /// <summary>
        /// Gets or Sets IsSuccess
        /// </summary>
        [DataMember(Name = "isSuccess", EmitDefaultValue = true)]
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Returns false as IsSuccess should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeIsSuccess()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = true)]
        public List<TableResponse> Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TableResponseIEnumerableServiceResult {\n");
            sb.Append("  Messages: ").Append(Messages).Append("\n");
            sb.Append("  IsSuccess: ").Append(IsSuccess).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
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
