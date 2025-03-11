# Stellards.SDK.Api.FieldApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**Delete**](FieldApi.md#delete) | **DELETE** /v1/schema/table/field | Deletes the given field within the given table. |
| [**Get**](FieldApi.md#get) | **GET** /v1/schema/table/field | Gets field(s) from given table. |
| [**Post**](FieldApi.md#post) | **POST** /v1/schema/table/field | Adds a field to the given table. |
| [**Put**](FieldApi.md#put) | **PUT** /v1/schema/table/field | Updates the given field in the given table. |

<a id="delete"></a>
# **Delete**
> void Delete (Guid project, int table, int field)

Deletes the given field within the given table.

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

            var apiInstance = new FieldApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table containing the given field.
            var field = 56;  // int | The field to be deleted

            try
            {
                // Deletes the given field within the given table.
                apiInstance.Delete(project, table, field);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FieldApi.Delete: " + e.Message);
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
    // Deletes the given field within the given table.
    apiInstance.DeleteWithHttpInfo(project, table, field);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FieldApi.DeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table containing the given field. |  |
| **field** | **int** | The field to be deleted |  |

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
| **404** | If the given table or field could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **200** |  |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="get"></a>
# **Get**
> IServiceResult Get (Guid project, int table, int? field = null)

Gets field(s) from given table.

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

            var apiInstance = new FieldApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table containing the fields.
            var field = 56;  // int? | The optional field to get. (optional) 

            try
            {
                // Gets field(s) from given table.
                IServiceResult result = apiInstance.Get(project, table, field);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FieldApi.Get: " + e.Message);
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
    // Gets field(s) from given table.
    ApiResponse<IServiceResult> response = apiInstance.GetWithHttpInfo(project, table, field);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FieldApi.GetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table containing the fields. |  |
| **field** | **int?** | The optional field to get. | [optional]  |

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
| **200** | Returns a list of fields. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="post"></a>
# **Post**
> FieldResponseServiceResult Post (Guid project, int table, FieldRequest? fieldRequest = null)

Adds a field to the given table.

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

            var apiInstance = new FieldApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table to be added to.
            var fieldRequest = new FieldRequest?(); // FieldRequest? |  (optional) 

            try
            {
                // Adds a field to the given table.
                FieldResponseServiceResult result = apiInstance.Post(project, table, fieldRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FieldApi.Post: " + e.Message);
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
    // Adds a field to the given table.
    ApiResponse<FieldResponseServiceResult> response = apiInstance.PostWithHttpInfo(project, table, fieldRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FieldApi.PostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table to be added to. |  |
| **fieldRequest** | [**FieldRequest?**](FieldRequest?.md) |  | [optional]  |

### Return type

[**FieldResponseServiceResult**](FieldResponseServiceResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Returns the newly created field. |  -  |
| **400** | If the field name has already been defined in the database. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |
| **404** | If the given table or field type could not be found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="put"></a>
# **Put**
> FieldResponseServiceResult Put (Guid project, int table, int field, FieldRequest? fieldRequest = null)

Updates the given field in the given table.

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

            var apiInstance = new FieldApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table containing the field.
            var field = 56;  // int | The field to be updated.
            var fieldRequest = new FieldRequest?(); // FieldRequest? |  (optional) 

            try
            {
                // Updates the given field in the given table.
                FieldResponseServiceResult result = apiInstance.Put(project, table, field, fieldRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FieldApi.Put: " + e.Message);
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
    // Updates the given field in the given table.
    ApiResponse<FieldResponseServiceResult> response = apiInstance.PutWithHttpInfo(project, table, field, fieldRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FieldApi.PutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table containing the field. |  |
| **field** | **int** | The field to be updated. |  |
| **fieldRequest** | [**FieldRequest?**](FieldRequest?.md) |  | [optional]  |

### Return type

[**FieldResponseServiceResult**](FieldResponseServiceResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the updated field. |  -  |
| **400** | If the field name has already been defined in the database. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |
| **404** | If the given table, field or field type could not be found. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

