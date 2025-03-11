/*
 * Stellar DataStore
 *
 * The Stellar DataStore service is your instantly available, secure and worry-free cloud data storage service. Sign-up now and minutes later, you can start putting your data in the cloud.
 *

 */


using System;
using System.Collections.Generic;
using StellarDs.SDK.Client;
using StellarDs.SDK.Model;

namespace StellarDs.SDK.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDataApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Download a file from a blob field as a stream.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Stream</returns>
        Stream BlobGet(Guid project, long table, long record, string field, int operationIndex = 0);

        /// <summary>
        /// Download a file from a blob field as a stream.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Stream</returns>
        ApiResponse<Stream> BlobGetWithHttpInfo(Guid project, long table, long record, string field, int operationIndex = 0);
        /// <summary>
        /// Upload a file to a blob field.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Stream</returns>
        Stream BlobPost(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0);

        /// <summary>
        /// Upload a file to a blob field.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Stream</returns>
        ApiResponse<Stream> BlobPostWithHttpInfo(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0);
        /// <summary>
        /// Deletes all records from the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void ClearDelete(Guid project, long table, int operationIndex = 0);

        /// <summary>
        /// Deletes all records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> ClearDeleteWithHttpInfo(Guid project, long table, int operationIndex = 0);
        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void Delete(Guid project, long table, int? record = default(int?), int operationIndex = 0);

        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteWithHttpInfo(Guid project, long table, int? record = default(int?), int operationIndex = 0);
        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeletePost(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0);

        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeletePostWithHttpInfo(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0);
        /// <summary>
        /// Gets the records for a given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        AbstractObjectQueryResult Get(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0);

        /// <summary>
        /// Gets the records for a given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        ApiResponse<AbstractObjectQueryResult> GetWithHttpInfo(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0);
        /// <summary>
        /// Adds records to the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        AbstractObjectQueryResult Post(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0);

        /// <summary>
        /// Adds records to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        ApiResponse<AbstractObjectQueryResult> PostWithHttpInfo(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0);
        /// <summary>
        /// Updates records in the given table based on the values in record.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        AbstractObjectQueryResult Put(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0);

        /// <summary>
        /// Updates records in the given table based on the values in record.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        ApiResponse<AbstractObjectQueryResult> PutWithHttpInfo(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDataApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Download a file from a blob field as a stream.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Stream</returns>
        System.Threading.Tasks.Task<Stream> BlobGetAsync(Guid project, long table, long record, string field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Download a file from a blob field as a stream.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Stream)</returns>
        System.Threading.Tasks.Task<ApiResponse<Stream>> BlobGetWithHttpInfoAsync(Guid project, long table, long record, string field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Upload a file to a blob field.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Stream</returns>
        System.Threading.Tasks.Task<Stream> BlobPostAsync(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Upload a file to a blob field.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Stream)</returns>
        System.Threading.Tasks.Task<ApiResponse<Stream>> BlobPostWithHttpInfoAsync(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Deletes all records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task ClearDeleteAsync(Guid project, long table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Deletes all records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> ClearDeleteWithHttpInfoAsync(Guid project, long table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteAsync(Guid project, long table, int? record = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, long table, int? record = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeletePostAsync(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Deletes records from the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeletePostWithHttpInfoAsync(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Gets the records for a given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        System.Threading.Tasks.Task<AbstractObjectQueryResult> GetAsync(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Gets the records for a given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> GetWithHttpInfoAsync(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Adds records to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        System.Threading.Tasks.Task<AbstractObjectQueryResult> PostAsync(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Adds records to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> PostWithHttpInfoAsync(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Updates records in the given table based on the values in record.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        System.Threading.Tasks.Task<AbstractObjectQueryResult> PutAsync(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Updates records in the given table based on the values in record.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> PutWithHttpInfoAsync(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDataApi : IDataApiSync, IDataApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class DataApi : IDataApi
    {
        private ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DataApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DataApi(string basePath)
        {
            this.Configuration = SDK.Client.Configuration.MergeConfigurations(
                GlobalConfiguration.Instance,
                new Configuration { BasePath = basePath }
            );
            this.Client = new ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = SDK.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public DataApi(Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = SDK.Client.Configuration.MergeConfigurations(
                GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new ApiClient(this.Configuration.BasePath);
            ExceptionFactory = SDK.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public DataApi(ISynchronousClient client, IAsynchronousClient asyncClient, IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = SDK.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Download a file from a blob field as a stream. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Stream</returns>
        public Stream BlobGet(Guid project, long table, long record, string field, int operationIndex = 0)
        {
            var localVarResponse = BlobGetWithHttpInfo(project, table, record, field);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Download a file from a blob field as a stream. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Stream</returns>
        public ApiResponse<Stream> BlobGetWithHttpInfo(Guid project, long table, long record, string field, int operationIndex = 0)
        {
            // verify the required parameter 'field' is set
            if (field == null)
            {
                throw new ApiException(400, "Missing required parameter 'field' when calling DataApi->BlobGet");
            }

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));

            localVarRequestOptions.Operation = "DataApi.BlobGet";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Stream>("/v1/data/table/blob", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("BlobGet", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Download a file from a blob field as a stream. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Stream</returns>
        public async System.Threading.Tasks.Task<Stream> BlobGetAsync(Guid project, long table, long record, string field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await BlobGetWithHttpInfoAsync(project, table, record, field, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Download a file from a blob field as a stream. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Stream)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Stream>> BlobGetWithHttpInfoAsync(Guid project, long table, long record, string field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            // verify the required parameter 'field' is set
            if (field == null)
            {
                throw new ApiException(400, "Missing required parameter 'field' when calling DataApi->BlobGet");
            }


            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));

            localVarRequestOptions.Operation = "DataApi.BlobGet";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Stream>("/v1/data/table/blob", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("BlobGet", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Upload a file to a blob field. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Stream</returns>
        public Stream BlobPost(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0)
        {
            var localVarResponse = BlobPostWithHttpInfo(project, table, record, field, data);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Upload a file to a blob field. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Stream</returns>
        public ApiResponse<Stream> BlobPostWithHttpInfo(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0)
        {
            // verify the required parameter 'field' is set
            if (field == null)
            {
                throw new ApiException(400, "Missing required parameter 'field' when calling DataApi->BlobPost");
            }

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "multipart/form-data"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            if (data != null)
            {
                localVarRequestOptions.FileParameters.Add("data", data);
            }

            localVarRequestOptions.Operation = "DataApi.BlobPost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Stream>("/v1/data/table/blob", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("BlobPost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Upload a file to a blob field. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Stream</returns>
        public async System.Threading.Tasks.Task<Stream> BlobPostAsync(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await BlobPostWithHttpInfoAsync(project, table, record, field, data, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Upload a file to a blob field. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the records.</param>
        /// <param name="record">The record to be queried.</param>
        /// <param name="field">The field of the record.</param>
        /// <param name="data"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Stream)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Stream>> BlobPostWithHttpInfoAsync(Guid project, long table, long record, string field, System.IO.Stream? data = default(System.IO.Stream?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            // verify the required parameter 'field' is set
            if (field == null)
            {
                throw new ApiException(400, "Missing required parameter 'field' when calling DataApi->BlobPost");
            }


            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "multipart/form-data"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            if (data != null)
            {
                localVarRequestOptions.FileParameters.Add("data", data);
            }

            localVarRequestOptions.Operation = "DataApi.BlobPost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Stream>("/v1/data/table/blob", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("BlobPost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes all records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void ClearDelete(Guid project, long table, int operationIndex = 0)
        {
            ClearDeleteWithHttpInfo(project, table);
        }

        /// <summary>
        /// Deletes all records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> ClearDeleteWithHttpInfo(Guid project, long table, int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));

            localVarRequestOptions.Operation = "DataApi.ClearDelete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/v1/data/table/clear", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("ClearDelete", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes all records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task ClearDeleteAsync(Guid project, long table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await ClearDeleteWithHttpInfoAsync(project, table, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes all records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> ClearDeleteWithHttpInfoAsync(Guid project, long table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));

            localVarRequestOptions.Operation = "DataApi.ClearDelete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/v1/data/table/clear", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("ClearDelete", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void Delete(Guid project, long table, int? record = default(int?), int operationIndex = 0)
        {
            DeleteWithHttpInfo(project, table, record);
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteWithHttpInfo(Guid project, long table, int? record = default(int?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (record != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            }

            localVarRequestOptions.Operation = "DataApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/v1/data/table", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Delete", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteAsync(Guid project, long table, int? record = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await DeleteWithHttpInfoAsync(project, table, record, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of table where you want to delete a record.</param>
        /// <param name="record">record id to delete. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, long table, int? record = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (record != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "record", record));
            }

            localVarRequestOptions.Operation = "DataApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/v1/data/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Delete", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeletePost(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0)
        {
            DeletePostWithHttpInfo(project, table, requestBody);
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeletePostWithHttpInfo(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.Data = requestBody;

            localVarRequestOptions.Operation = "DataApi.DeletePost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/v1/data/table/delete", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("DeletePost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeletePostAsync(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await DeletePostWithHttpInfoAsync(project, table, requestBody, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes records from the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to delete from.</param>
        /// <param name="requestBody">An array of ids (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeletePostWithHttpInfoAsync(Guid project, int table, List<string>? requestBody = default(List<string>?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.Data = requestBody;

            localVarRequestOptions.Operation = "DataApi.DeletePost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/v1/data/table/delete", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("DeletePost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Gets the records for a given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        public AbstractObjectQueryResult Get(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0)
        {
            var localVarResponse = GetWithHttpInfo(project, table, offset, take, joinQuery, whereQuery, sortQuery, distinct, select);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets the records for a given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        public ApiResponse<AbstractObjectQueryResult> GetWithHttpInfo(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (offset != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "Offset", offset));
            }
            if (take != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "Take", take));
            }
            if (joinQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "JoinQuery", joinQuery));
            }
            if (whereQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "WhereQuery", whereQuery));
            }
            if (sortQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "SortQuery", sortQuery));
            }
            if (distinct != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "distinct", distinct));
            }
            if (select != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "select", select));
            }

            localVarRequestOptions.Operation = "DataApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Get", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Gets the records for a given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        public async System.Threading.Tasks.Task<AbstractObjectQueryResult> GetAsync(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await GetWithHttpInfoAsync(project, table, offset, take, joinQuery, whereQuery, sortQuery, distinct, select, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets the records for a given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">Id of the table containing the records.</param>
        /// <param name="offset">The offset of the records. (optional)</param>
        /// <param name="take">The amount of records. (optional)</param>
        /// <param name="joinQuery">The join queries to apply. (optional)</param>
        /// <param name="whereQuery">The where queries to apply. (optional)</param>
        /// <param name="sortQuery">The sort queries to apply. (optional)</param>
        /// <param name="distinct"> (optional, default to false)</param>
        /// <param name="select"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> GetWithHttpInfoAsync(Guid project, long table, long? offset = default(long?), long? take = default(long?), string? joinQuery = default(string?), string? whereQuery = default(string?), string? sortQuery = default(string?), bool? distinct = default(bool?), string? select = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (offset != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "Offset", offset));
            }
            if (take != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "Take", take));
            }
            if (joinQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "JoinQuery", joinQuery));
            }
            if (whereQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "WhereQuery", whereQuery));
            }
            if (sortQuery != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "SortQuery", sortQuery));
            }
            if (distinct != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "distinct", distinct));
            }
            if (select != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "select", select));
            }

            localVarRequestOptions.Operation = "DataApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Get", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Adds records to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        public AbstractObjectQueryResult Post(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0)
        {
            var localVarResponse = PostWithHttpInfo(project, table, createRecordRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Adds records to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        public ApiResponse<AbstractObjectQueryResult> PostWithHttpInfo(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.Data = createRecordRequest;

            localVarRequestOptions.Operation = "DataApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Post", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Adds records to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        public async System.Threading.Tasks.Task<AbstractObjectQueryResult> PostAsync(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PostWithHttpInfoAsync(project, table, createRecordRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Adds records to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="createRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> PostWithHttpInfoAsync(Guid project, long table, CreateRecordRequest? createRecordRequest = default(CreateRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            localVarRequestOptions.Data = createRecordRequest;

            localVarRequestOptions.Operation = "DataApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Post", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Updates records in the given table based on the values in record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectQueryResult</returns>
        public AbstractObjectQueryResult Put(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0)
        {
            var localVarResponse = PutWithHttpInfo(project, table, updateRecordRequest, force);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates records in the given table based on the values in record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectQueryResult</returns>
        public ApiResponse<AbstractObjectQueryResult> PutWithHttpInfo(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (force)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "force", force));
            }
            localVarRequestOptions.Data = updateRecordRequest;

            localVarRequestOptions.Operation = "DataApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("Put", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Updates records in the given table based on the values in record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectQueryResult</returns>
        public async System.Threading.Tasks.Task<AbstractObjectQueryResult> PutAsync(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PutWithHttpInfoAsync(project, table, updateRecordRequest, force, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates records in the given table based on the values in record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated in.</param>
        /// <param name="force">If you are certain you want to update all your records in case no ids were included. (optional, default to false)</param>
        /// <param name="updateRecordRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectQueryResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AbstractObjectQueryResult>> PutWithHttpInfoAsync(Guid project, long table, UpdateRecordRequest? updateRecordRequest = default(UpdateRecordRequest?), bool force = false, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "application/json"
            };

            var localVarContentType = ClientUtils.SelectHeaderContentType(contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = ClientUtils.SelectHeaderAccept(accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "project", project));
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            if (force)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "force", force));
            }

            if (updateRecordRequest != null) localVarRequestOptions.Data = updateRecordRequest;

            localVarRequestOptions.Operation = "DataApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<AbstractObjectQueryResult>("/v1/data/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            {
                var exception = this.ExceptionFactory("Put", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

    }
}
