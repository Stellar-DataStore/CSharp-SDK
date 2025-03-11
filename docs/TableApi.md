# Stellards.SDK.Api.TableApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**Delete**](TableApi.md#delete) | **DELETE** /v1/schema/table | Deletes a table from the database. |
| [**Get**](TableApi.md#get) | **GET** /v1/schema/table | Gets table(s) from the logged in user. |
| [**Post**](TableApi.md#post) | **POST** /v1/schema/table | Creates a new table in the database. |
| [**Put**](TableApi.md#put) | **PUT** /v1/schema/table | Updates a table in the database. |

<a id="delete"></a>
# **Delete**
> void Delete (Guid project, int table)

Deletes a table from the database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class DeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TableApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table to be deleted.

            try
            {
                // Deletes a table from the database.
                apiInstance.Delete(project, table);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TableApi.Delete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes a table from the database.
    apiInstance.DeleteWithHttpInfo(project, table);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TableApi.DeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table to be deleted. |  |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **404** | If the given table could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **200** |  |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="get"></a>
# **Get**
> IServiceResult Get (Guid project, string? table = null)

Gets table(s) from the logged in user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class GetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TableApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = "table_example";  // string? | name or id of the table to fetch (optional) (optional) 

            try
            {
                // Gets table(s) from the logged in user.
                IServiceResult result = apiInstance.Get(project, table);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TableApi.Get: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Gets table(s) from the logged in user.
    ApiResponse<IServiceResult> response = apiInstance.GetWithHttpInfo(project, table);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TableApi.GetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **string?** | name or id of the table to fetch (optional) | [optional]  |

### Return type

[**IServiceResult**](IServiceResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns a list of tables. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="post"></a>
# **Post**
> AbstractObjectServiceResult Post (Guid project, TableRequest? tableRequest = null)

Creates a new table in the database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class PostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TableApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var tableRequest = new TableRequest?(); // TableRequest? |  (optional) 

            try
            {
                // Creates a new table in the database.
                AbstractObjectServiceResult result = apiInstance.Post(project, tableRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TableApi.Post: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Creates a new table in the database.
    ApiResponse<AbstractObjectServiceResult> response = apiInstance.PostWithHttpInfo(project, tableRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TableApi.PostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **tableRequest** | [**TableRequest?**](TableRequest?.md) |  | [optional]  |

### Return type

[**AbstractObjectServiceResult**](AbstractObjectServiceResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Returns the newly created table. |  -  |
| **400** | If the table name has already been defined in the database. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="put"></a>
# **Put**
> TableResponseIEnumerableServiceResult Put (Guid project, int? table = null, TableRequest? tableRequest = null)

Updates a table in the database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Stellards.SDK.Api;
using Stellards.SDK.Client;
using Stellards.SDK.Model;

namespace Example
{
    public class PutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TableApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int? | The table to be updated. (optional) 
            var tableRequest = new TableRequest?(); // TableRequest? |  (optional) 

            try
            {
                // Updates a table in the database.
                TableResponseIEnumerableServiceResult result = apiInstance.Put(project, table, tableRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TableApi.Put: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Updates a table in the database.
    ApiResponse<TableResponseIEnumerableServiceResult> response = apiInstance.PutWithHttpInfo(project, table, tableRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TableApi.PutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int?** | The table to be updated. | [optional]  |
| **tableRequest** | [**TableRequest?**](TableRequest?.md) |  | [optional]  |

### Return type

[**TableResponseIEnumerableServiceResult**](TableResponseIEnumerableServiceResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the updated table. |  -  |
| **404** | If the given table could not be found. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

