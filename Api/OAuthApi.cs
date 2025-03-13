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
    public interface IOAuthApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RevokePost(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RevokePostWithHttpInfo(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>TokenResponse</returns>
        TokenResponse TokenPost(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of TokenResponse</returns>
        ApiResponse<TokenResponse> TokenPostWithHttpInfo(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IOAuthApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RevokePostAsync(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RevokePostWithHttpInfoAsync(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TokenResponse</returns>
        System.Threading.Tasks.Task<TokenResponse> TokenPostAsync(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TokenResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<TokenResponse>> TokenPostWithHttpInfoAsync(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IOAuthApi : IOAuthApiSync, IOAuthApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class OAuthApi : IOAuthApi
    {
        private ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthApi"/> class.
        /// </summary>
        /// <returns></returns>
        public OAuthApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthApi"/> class.
        /// </summary>
        /// <returns></returns>
        public OAuthApi(string basePath)
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
        /// Initializes a new instance of the <see cref="OAuthApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public OAuthApi(Configuration configuration)
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
        /// Initializes a new instance of the <see cref="OAuthApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public OAuthApi(ISynchronousClient client, IAsynchronousClient asyncClient, IReadableConfiguration configuration)
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
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RevokePost(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0)
        {
            RevokePostWithHttpInfo(revokeTokenRequest);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public ApiResponse<Object> RevokePostWithHttpInfo(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0)
        {
            // verify the required parameter 'revokeTokenRequest' is set
            if (revokeTokenRequest == null)
            {
                throw new ApiException(400, "Missing required parameter 'revokeTokenRequest' when calling OAuthApi->RevokePost");
            }

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json",
                "text/json",
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
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

            localVarRequestOptions.Data = revokeTokenRequest;

            localVarRequestOptions.Operation = "OAuthApi.RevokePost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/v1/oauth/revoke", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("RevokePost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RevokePostAsync(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            await RevokePostWithHttpInfoAsync(revokeTokenRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="revokeTokenRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<Object>> RevokePostWithHttpInfoAsync(RevokeTokenRequest revokeTokenRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            // verify the required parameter 'revokeTokenRequest' is set
            if (revokeTokenRequest == null)
            {
                throw new ApiException(400, "Missing required parameter 'revokeTokenRequest' when calling OAuthApi->RevokePost");
            }


            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/json", 
                "text/json", 
                "application/*+json"
            };

            // to determine the Accept header
            var accepts = new string[] {
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

            localVarRequestOptions.Data = revokeTokenRequest;

            localVarRequestOptions.Operation = "OAuthApi.RevokePost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/v1/oauth/revoke", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("RevokePost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>TokenResponse</returns>
        public TokenResponse TokenPost(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0)
        {
            var localVarResponse = TokenPostWithHttpInfo(grantType, clientId, clientSecret, redirectUri, code, refreshToken);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of TokenResponse</returns>
        public ApiResponse<TokenResponse> TokenPostWithHttpInfo(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'grantType' is set
            if (grantType == null)
            {
                throw new ApiException(400, "Missing required parameter 'grantType' when calling OAuthApi->TokenPost");
            }

            // verify the required parameter 'clientSecret' is set
            if (clientSecret == null)
            {
                throw new ApiException(400, "Missing required parameter 'clientSecret' when calling OAuthApi->TokenPost");
            }

            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/x-www-form-urlencoded"
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

            localVarRequestOptions.FormParameters.Add("grant_type", ClientUtils.ParameterToString(grantType)); // form parameter
            localVarRequestOptions.FormParameters.Add("client_id", ClientUtils.ParameterToString(clientId)); // form parameter
            localVarRequestOptions.FormParameters.Add("client_secret", ClientUtils.ParameterToString(clientSecret)); // form parameter
            if (redirectUri != null)
            {
                localVarRequestOptions.FormParameters.Add("redirect_uri", ClientUtils.ParameterToString(redirectUri)); // form parameter
            }
            if (code != null)
            {
                localVarRequestOptions.FormParameters.Add("code", ClientUtils.ParameterToString(code)); // form parameter
            }
            if (refreshToken != null)
            {
                localVarRequestOptions.FormParameters.Add("refresh_token", ClientUtils.ParameterToString(refreshToken)); // form parameter
            }

            localVarRequestOptions.Operation = "OAuthApi.TokenPost";
            localVarRequestOptions.OperationIndex = operationIndex;
            

            // make the HTTP request
            var localVarResponse = this.Client.Post<TokenResponse>("/v1/oauth/token", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("TokenPost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TokenResponse</returns>
        public async System.Threading.Tasks.Task<TokenResponse> TokenPostAsync(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            var localVarResponse = await TokenPostWithHttpInfoAsync(grantType, clientId, clientSecret, redirectUri, code, refreshToken, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="grantType"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"> (optional, default to &quot;&quot;)</param>
        /// <param name="code"> (optional, default to &quot;&quot;)</param>
        /// <param name="refreshToken"> (optional, default to &quot;&quot;)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (TokenResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TokenResponse>> TokenPostWithHttpInfoAsync(string grantType, Guid clientId, string clientSecret, string? redirectUri = default(string?), string? code = default(string?), string? refreshToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            // verify the required parameter 'grantType' is set
            if (grantType == null)
            {
                throw new ApiException(400, "Missing required parameter 'grantType' when calling OAuthApi->TokenPost");
            }

            // verify the required parameter 'clientSecret' is set
            if (clientSecret == null)
            {
                throw new ApiException(400, "Missing required parameter 'clientSecret' when calling OAuthApi->TokenPost");
            }


            var localVarRequestOptions = new RequestOptions();

            var contentTypes = new string[] {
                "application/x-www-form-urlencoded"
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

            localVarRequestOptions.FormParameters.Add("grant_type", ClientUtils.ParameterToString(grantType)); // form parameter
            localVarRequestOptions.FormParameters.Add("client_id", ClientUtils.ParameterToString(clientId)); // form parameter
            localVarRequestOptions.FormParameters.Add("client_secret", ClientUtils.ParameterToString(clientSecret)); // form parameter
            if (redirectUri != null)
            {
                localVarRequestOptions.FormParameters.Add("redirect_uri", ClientUtils.ParameterToString(redirectUri)); // form parameter
            }
            if (code != null)
            {
                localVarRequestOptions.FormParameters.Add("code", ClientUtils.ParameterToString(code)); // form parameter
            }
            if (refreshToken != null)
            {
                localVarRequestOptions.FormParameters.Add("refresh_token", ClientUtils.ParameterToString(refreshToken)); // form parameter
            }

            localVarRequestOptions.Operation = "OAuthApi.TokenPost";
            localVarRequestOptions.OperationIndex = operationIndex;

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<TokenResponse>("/v1/oauth/token", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                var exception = this.ExceptionFactory("TokenPost", localVarResponse);
                if (exception != null)
                {
                    throw exception;
                }
            }

            return localVarResponse;
        }

    }
}
