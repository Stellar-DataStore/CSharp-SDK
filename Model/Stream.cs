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
    /// Stream
    /// </summary>
    [DataContract(Name = "Stream")]
    public partial class Stream : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stream" /> class.
        /// </summary>
        /// <param name="position">position.</param>
        /// <param name="readTimeout">readTimeout.</param>
        /// <param name="writeTimeout">writeTimeout.</param>
        public Stream(long position = default(long), int readTimeout = default(int), int writeTimeout = default(int))
        {
            this.Position = position;
            this.ReadTimeout = readTimeout;
            this.WriteTimeout = writeTimeout;
        }

        /// <summary>
        /// Gets or Sets CanRead
        /// </summary>
        [DataMember(Name = "canRead", EmitDefaultValue = true)]
        public bool CanRead { get; private set; }

        /// <summary>
        /// Returns false as CanRead should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCanRead()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets CanWrite
        /// </summary>
        [DataMember(Name = "canWrite", EmitDefaultValue = true)]
        public bool CanWrite { get; private set; }

        /// <summary>
        /// Returns false as CanWrite should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCanWrite()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets CanSeek
        /// </summary>
        [DataMember(Name = "canSeek", EmitDefaultValue = true)]
        public bool CanSeek { get; private set; }

        /// <summary>
        /// Returns false as CanSeek should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCanSeek()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets CanTimeout
        /// </summary>
        [DataMember(Name = "canTimeout", EmitDefaultValue = true)]
        public bool CanTimeout { get; private set; }

        /// <summary>
        /// Returns false as CanTimeout should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCanTimeout()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets Length
        /// </summary>
        [DataMember(Name = "length", EmitDefaultValue = false)]
        public long Length { get; private set; }

        /// <summary>
        /// Returns false as Length should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLength()
        {
            return false;
        }
        /// <summary>
        /// Gets or Sets Position
        /// </summary>
        [DataMember(Name = "position", EmitDefaultValue = false)]
        public long Position { get; set; }

        /// <summary>
        /// Gets or Sets ReadTimeout
        /// </summary>
        [DataMember(Name = "readTimeout", EmitDefaultValue = false)]
        public int ReadTimeout { get; set; }

        /// <summary>
        /// Gets or Sets WriteTimeout
        /// </summary>
        [DataMember(Name = "writeTimeout", EmitDefaultValue = false)]
        public int WriteTimeout { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Stream {\n");
            sb.Append("  CanRead: ").Append(CanRead).Append("\n");
            sb.Append("  CanWrite: ").Append(CanWrite).Append("\n");
            sb.Append("  CanSeek: ").Append(CanSeek).Append("\n");
            sb.Append("  CanTimeout: ").Append(CanTimeout).Append("\n");
            sb.Append("  Length: ").Append(Length).Append("\n");
            sb.Append("  Position: ").Append(Position).Append("\n");
            sb.Append("  ReadTimeout: ").Append(ReadTimeout).Append("\n");
            sb.Append("  WriteTimeout: ").Append(WriteTimeout).Append("\n");
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
