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
    public interface ITableApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void Delete(Guid project, int table, int operationIndex = 0);

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteWithHttpInfo(Guid project, int table, int operationIndex = 0);
        /// <summary>
        /// Gets table(s) from the logged in user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>IServiceResult</returns>
        ServiceResult Get(Guid project, string? table = default(string?), int operationIndex = 0);

        /// <summary>
        /// Gets table(s) from the logged in user.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of IServiceResult</returns>
        ApiResponse<ServiceResult> GetWithHttpInfo(Guid project, string? table = default(string?), int operationIndex = 0);
        /// <summary>
        /// Creates a new table in the database.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectServiceResult</returns>
        AbstractObjectServiceResult Post(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0);

        /// <summary>
        /// Creates a new table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectServiceResult</returns>
        ApiResponse<AbstractObjectServiceResult> PostWithHttpInfo(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0);
        /// <summary>
        /// Updates a table in the database.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>TableResponseIEnumerableServiceResult</returns>
        TableResponseIEnumerableServiceResult Put(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0);

        /// <summary>
        /// Updates a table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of TableResponseIEnumerableServiceResult</returns>
        ApiResponse<TableResponseIEnumerableServiceResult> PutWithHttpInfo(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITableApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteAsync(Guid project, int table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Deletes a table from the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, int table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Gets table(s) from the logged in user.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of IServiceResult</returns>
        System.Threading.Tasks.Task<ServiceResult> GetAsync(Guid project, string? table = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Gets table(s) from the logged in user.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (IServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceResult>> GetWithHttpInfoAsync(Guid project, string? table = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Creates a new table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectServiceResult</returns>
        System.Threading.Tasks.Task<AbstractObjectServiceResult> PostAsync(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Creates a new table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<AbstractObjectServiceResult>> PostWithHttpInfoAsync(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// Updates a table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TableResponseIEnumerableServiceResult</returns>
        System.Threading.Tasks.Task<TableResponseIEnumerableServiceResult> PutAsync(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// Updates a table in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TableResponseIEnumerableServiceResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<TableResponseIEnumerableServiceResult>> PutWithHttpInfoAsync(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITableApi : ITableApiSync, ITableApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class TableApi : ITableApi
    {
        private ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TableApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TableApi(string basePath)
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
        /// Initializes a new instance of the <see cref="TableApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public TableApi(Configuration configuration)
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
        /// Initializes a new instance of the <see cref="TableApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public TableApi(ISynchronousClient client, IAsynchronousClient asyncClient, IReadableConfiguration configuration)
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
        /// Deletes a table from the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void Delete(Guid project, int table, int operationIndex = 0)
        {
            DeleteWithHttpInfo(project, table);
        }

        /// <summary>
        /// Deletes a table from the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> DeleteWithHttpInfo(Guid project, int table, int operationIndex = 0)
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

            localVarRequestOptions.Operation = "TableApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/v1/schema/table", localVarRequestOptions, this.Configuration);
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
        /// Deletes a table from the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteAsync(Guid project, int table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await DeleteWithHttpInfoAsync(project, table, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a table from the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be deleted.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteWithHttpInfoAsync(Guid project, int table, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "TableApi.Delete";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/v1/schema/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Gets table(s) from the logged in user. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>IServiceResult</returns>
        public ServiceResult Get(Guid project, string? table = default(string?), int operationIndex = 0)
        {
            var localVarResponse = GetWithHttpInfo(project, table);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets table(s) from the logged in user. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of IServiceResult</returns>
        public ApiResponse<ServiceResult> GetWithHttpInfo(Guid project, string? table = default(string?), int operationIndex = 0)
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
            if (table != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            }

            localVarRequestOptions.Operation = "TableApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration);
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
        /// Gets table(s) from the logged in user. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of IServiceResult</returns>
        public async System.Threading.Tasks.Task<ServiceResult> GetAsync(Guid project, string? table = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await GetWithHttpInfoAsync(project, table, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Gets table(s) from the logged in user. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">name or id of the table to fetch (optional) (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (IServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ServiceResult>> GetWithHttpInfoAsync(Guid project, string? table = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
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
            if (table != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            }

            localVarRequestOptions.Operation = "TableApi.Get";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Creates a new table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AbstractObjectServiceResult</returns>
        public AbstractObjectServiceResult Post(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0)
        {
            var localVarResponse = PostWithHttpInfo(project, tableRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Creates a new table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AbstractObjectServiceResult</returns>
        public ApiResponse<AbstractObjectServiceResult> PostWithHttpInfo(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0)
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
            localVarRequestOptions.Data = tableRequest;

            localVarRequestOptions.Operation = "TableApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<AbstractObjectServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration);
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
        /// Creates a new table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AbstractObjectServiceResult</returns>
        public async System.Threading.Tasks.Task<AbstractObjectServiceResult> PostAsync(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PostWithHttpInfoAsync(project, tableRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Creates a new table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AbstractObjectServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<AbstractObjectServiceResult>> PostWithHttpInfoAsync(Guid project, TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
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
            localVarRequestOptions.Data = tableRequest;

            localVarRequestOptions.Operation = "TableApi.Post";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<AbstractObjectServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
        /// Updates a table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>TableResponseIEnumerableServiceResult</returns>
        public TableResponseIEnumerableServiceResult Put(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0)
        {
            var localVarResponse = PutWithHttpInfo(project, table, tableRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates a table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of TableResponseIEnumerableServiceResult</returns>
        public ApiResponse<TableResponseIEnumerableServiceResult> PutWithHttpInfo(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0)
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
            if (table != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            }
            localVarRequestOptions.Data = tableRequest;

            localVarRequestOptions.Operation = "TableApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<TableResponseIEnumerableServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration);
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
        /// Updates a table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TableResponseIEnumerableServiceResult</returns>
        public async System.Threading.Tasks.Task<TableResponseIEnumerableServiceResult> PutAsync(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await PutWithHttpInfoAsync(project, table, tableRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Updates a table in the database. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="project">The project containing the table.</param>
        /// <param name="table">The table to be updated. (optional)</param>
        /// <param name="tableRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TableResponseIEnumerableServiceResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TableResponseIEnumerableServiceResult>> PutWithHttpInfoAsync(Guid project, int? table = default(int?), TableRequest? tableRequest = default(TableRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
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
            if (table != null)
            {
                localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "table", table));
            }
            localVarRequestOptions.Data = tableRequest;

            localVarRequestOptions.Operation = "TableApi.Put";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (Bearer) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", this.Configuration.GetApiKeyWithPrefix("Authorization"));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<TableResponseIEnumerableServiceResult>("/v1/schema/table", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

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
