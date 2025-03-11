# Stellards.SDK.Api.OAuthApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**V1OauthRevokePost**](OAuthApi.md#v1oauthrevokepost) | **POST** /v1/oauth/revoke |  |
| [**V1OauthTokenPost**](OAuthApi.md#v1oauthtokenpost) | **POST** /v1/oauth/token |  |

<a id="v1oauthrevokepost"></a>
# **V1OauthRevokePost**
> void V1OauthRevokePost (RevokeTokenRequest revokeTokenRequest)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class V1OauthRevokePostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new OAuthApi(config);
            var revokeTokenRequest = new RevokeTokenRequest(); // RevokeTokenRequest | 

            try
            {
                apiInstance.V1OauthRevokePost(revokeTokenRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OAuthApi.V1OauthRevokePost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the V1OauthRevokePostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.V1OauthRevokePostWithHttpInfo(revokeTokenRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OAuthApi.V1OauthRevokePostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **revokeTokenRequest** | [**RevokeTokenRequest**](RevokeTokenRequest.md) |  |  |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="v1oauthtokenpost"></a>
# **V1OauthTokenPost**
> TokenResponse V1OauthTokenPost (string grantType, Guid clientId, string clientSecret, string? redirectUri = null, string? code = null, string? refreshToken = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class V1OauthTokenPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new OAuthApi(config);
            var grantType = "grantType_example";  // string | 
            var clientId = "clientId_example";  // Guid | 
            var clientSecret = "clientSecret_example";  // string | 
            var redirectUri = "\"\"";  // string? |  (optional)  (default to "")
            var code = "\"\"";  // string? |  (optional)  (default to "")
            var refreshToken = "\"\"";  // string? |  (optional)  (default to "")

            try
            {
                TokenResponse result = apiInstance.V1OauthTokenPost(grantType, clientId, clientSecret, redirectUri, code, refreshToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OAuthApi.V1OauthTokenPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the V1OauthTokenPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<TokenResponse> response = apiInstance.V1OauthTokenPostWithHttpInfo(grantType, clientId, clientSecret, redirectUri, code, refreshToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OAuthApi.V1OauthTokenPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **grantType** | **string** |  |  |
| **clientId** | **Guid** |  |  |
| **clientSecret** | **string** |  |  |
| **redirectUri** | **string?** |  | [optional] [default to &quot;&quot;] |
| **code** | **string?** |  | [optional] [default to &quot;&quot;] |
| **refreshToken** | **string?** |  | [optional] [default to &quot;&quot;] |

### Return type

[**TokenResponse**](TokenResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

