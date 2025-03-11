/*
 * Stellar DataStore
 *
 * The Stellar DataStore service is your instantly available, secure and worry-free cloud data storage service. Sign-up now and minutes later, you can start putting your data in the cloud.
 *

 */


using System;
using StellarDs.SDK.Client;
using StellarDs.SDK.Model;

namespace StellarDs.SDK.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IFieldApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Deletes the given field within the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void Delete(Guid project, int table, int field, int operationIndex = 0);

        /// <summary>
        /// Deletes the given field within the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteWithHttpInfo(Guid project, int table, int field, int operationIndex = 0);
        /// <summary>
        /// Gets field(s) from given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>IServiceResult</returns>
        ServiceResult Get(Guid project, int table, int? field = default(int?), int operationIndex = 0);

        /// <summary>
        /// Gets field(s) from given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of IServiceResult</returns>
        ApiResponse<ServiceResult> GetWithHttpInfo(Guid project, int table, int? field = default(int?), int operationIndex = 0);
        /// <summary>
        /// Adds a field to the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>FieldResponseServiceResult</returns>
        FieldResponseServiceResult Post(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0);

        /// <summary>
        /// Adds a field to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of FieldResponseServiceResult</returns>
        ApiResponse<FieldResponseServiceResult> PostWithHttpInfo(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0);
        /// <summary>
        /// Updates the given field in the given table.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>FieldResponseServiceResult</returns>
        FieldResponseServiceResult Put(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0);

        /// <summary>
        /// Updates the given field in the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of FieldResponseServiceResult</returns>
        ApiResponse<FieldResponseServiceResult> PutWithHttpInfo(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IFieldApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Deletes the given field within the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteAsync(Guid project, int table, int field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Deletes the given field within the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, int table, int field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Gets field(s) from given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of IServiceResult</returns>
        System.Threading.Tasks.Task<ServiceResult> GetAsync(Guid project, int table, int? field = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Gets field(s) from given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (IServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceResult>> GetWithHttpInfoAsync(Guid project, int table, int? field = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Adds a field to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of FieldResponseServiceResult</returns>
        System.Threading.Tasks.Task<FieldResponseServiceResult> PostAsync(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Adds a field to the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (FieldResponseServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<FieldResponseServiceResult>> PostWithHttpInfoAsync(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Updates the given field in the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of FieldResponseServiceResult</returns>
        System.Threading.Tasks.Task<FieldResponseServiceResult> PutAsync(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Updates the given field in the given table.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (FieldResponseServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<FieldResponseServiceResult>> PutWithHttpInfoAsync(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IFieldApi : IFieldApiSync, IFieldApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class FieldApi : IFieldApi
    {
        private ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldApi"/> class.
        /// </summary>
        /// <returns></returns>
        public FieldApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldApi"/> class.
        /// </summary>
        /// <returns></returns>
        public FieldApi(string basePath)
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
        /// Initializes a new instance of the <see cref="FieldApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public FieldApi(Configuration configuration)
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
        /// Initializes a new instance of the <see cref="FieldApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public FieldApi(ISynchronousClient client, IAsynchronousClient asyncClient, IReadableConfiguration configuration)
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
        /// Deletes the given field within the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void Delete(Guid project, int table, int field, int operationIndex = 0)
        {
            DeleteWithHttpInfo(project, table, field);
        }

        /// <summary>
        /// Deletes the given field within the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteWithHttpInfo(Guid project, int table, int field, int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));

            localVarRequestOptions.Operation = "FieldApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/v1/schema/table/field", localVarRequestOptions, this.Configuration);
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
        /// Deletes the given field within the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteAsync(Guid project, int table, int field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await DeleteWithHttpInfoAsync(project, table, field, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the given field within the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the given field.</param>
        /// <param name="field">The field to be deleted</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, int table, int field, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));

            localVarRequestOptions.Operation = "FieldApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/v1/schema/table/field", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Gets field(s) from given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>IServiceResult</returns>
        public ServiceResult Get(Guid project, int table, int? field = default(int?), int operationIndex = 0)
        {
            var localVarResponse = GetWithHttpInfo(project, table, field);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets field(s) from given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of IServiceResult</returns>
        public ApiResponse<ServiceResult> GetWithHttpInfo(Guid project, int table, int? field = default(int?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            if (field != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            }

            localVarRequestOptions.Operation = "FieldApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration);
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
        /// Gets field(s) from given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of IServiceResult</returns>
        public async System.Threading.Tasks.Task<ServiceResult> GetAsync(Guid project, int table, int? field = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await GetWithHttpInfoAsync(project, table, field, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets field(s) from given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the fields.</param>
        /// <param name="field">The optional field to get. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (IServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ServiceResult>> GetWithHttpInfoAsync(Guid project, int table, int? field = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            if (field != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            }

            localVarRequestOptions.Operation = "FieldApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Adds a field to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>FieldResponseServiceResult</returns>
        public FieldResponseServiceResult Post(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0)
        {
            var localVarResponse = PostWithHttpInfo(project, table, fieldRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Adds a field to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of FieldResponseServiceResult</returns>
        public ApiResponse<FieldResponseServiceResult> PostWithHttpInfo(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.Data = fieldRequest;

            localVarRequestOptions.Operation = "FieldApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<FieldResponseServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration);
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
        /// Adds a field to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of FieldResponseServiceResult</returns>
        public async System.Threading.Tasks.Task<FieldResponseServiceResult> PostAsync(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PostWithHttpInfoAsync(project, table, fieldRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Adds a field to the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be added to.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (FieldResponseServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<FieldResponseServiceResult>> PostWithHttpInfoAsync(Guid project, int table, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.Data = fieldRequest;

            localVarRequestOptions.Operation = "FieldApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<FieldResponseServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Updates the given field in the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>FieldResponseServiceResult</returns>
        public FieldResponseServiceResult Put(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0)
        {
            var localVarResponse = PutWithHttpInfo(project, table, field, fieldRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates the given field in the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of FieldResponseServiceResult</returns>
        public ApiResponse<FieldResponseServiceResult> PutWithHttpInfo(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0)
        {
            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            localVarRequestOptions.Data = fieldRequest;

            localVarRequestOptions.Operation = "FieldApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<FieldResponseServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration);
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
        /// Updates the given field in the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of FieldResponseServiceResult</returns>
        public async System.Threading.Tasks.Task<FieldResponseServiceResult> PutAsync(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PutWithHttpInfoAsync(project, table, field, fieldRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates the given field in the given table. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table containing the field.</param>
        /// <param name="field">The field to be updated.</param>
        /// <param name="fieldRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (FieldResponseServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<FieldResponseServiceResult>> PutWithHttpInfoAsync(Guid project, int table, int field, FieldRequest? fieldRequest = default(FieldRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
                "text/plain",
                "application/json",
                "text/json"
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
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "field", field));
            localVarRequestOptions.Data = fieldRequest;

            localVarRequestOptions.Operation = "FieldApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<FieldResponseServiceResult>("/v1/schema/table/field", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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

    }
}
