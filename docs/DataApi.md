# Org.OpenAPITools.Api.DataApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BlobGet**](DataApi.md#blobget) | **GET** /v1/data/table/blob | Download a file from a blob field as a stream. |
| [**BlobPost**](DataApi.md#blobpost) | **POST** /v1/data/table/blob | Upload a file to a blob field. |
| [**ClearDelete**](DataApi.md#cleardelete) | **DELETE** /v1/data/table/clear | Deletes all records from the given table. |
| [**Delete**](DataApi.md#delete) | **DELETE** /v1/data/table | Deletes records from the given table. |
| [**DeletePost**](DataApi.md#deletepost) | **POST** /v1/data/table/delete | Deletes records from the given table. |
| [**Get**](DataApi.md#get) | **GET** /v1/data/table | Gets the records for a given table. |
| [**Post**](DataApi.md#post) | **POST** /v1/data/table | Adds records to the given table. |
| [**Put**](DataApi.md#put) | **PUT** /v1/data/table | Updates records in the given table based on the values in record. |

<a id="blobget"></a>
# **BlobGet**
> Stream BlobGet (Guid project, long table, long record, string field)

Download a file from a blob field as a stream.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class BlobGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | The table containing the records.
            var record = 789L;  // long | The record to be queried.
            var field = "field_example";  // string | The field of the record.

            try
            {
                // Download a file from a blob field as a stream.
                Stream result = apiInstance.BlobGet(project, table, record, field);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.BlobGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BlobGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download a file from a blob field as a stream.
    ApiResponse<Stream> response = apiInstance.BlobGetWithHttpInfo(project, table, record, field);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.BlobGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | The table containing the records. |  |
| **record** | **long** | The record to be queried. |  |
| **field** | **string** | The field of the record. |  |

### Return type

[**Stream**](Stream.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the queried records. |  -  |
| **400** | If the queries provided could not be parsed. |  -  |
| **404** | If the given tables,  could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="blobpost"></a>
# **BlobPost**
> Stream BlobPost (Guid project, long table, long record, string field, System.IO.Stream? data = null)

Upload a file to a blob field.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class BlobPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | The table containing the records.
            var record = 789L;  // long | The record to be queried.
            var field = "field_example";  // string | The field of the record.
            var data = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // System.IO.Stream? |  (optional) 

            try
            {
                // Upload a file to a blob field.
                Stream result = apiInstance.BlobPost(project, table, record, field, data);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.BlobPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BlobPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload a file to a blob field.
    ApiResponse<Stream> response = apiInstance.BlobPostWithHttpInfo(project, table, record, field, data);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.BlobPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | The table containing the records. |  |
| **record** | **long** | The record to be queried. |  |
| **field** | **string** | The field of the record. |  |
| **data** | **System.IO.Stream?****System.IO.Stream?** |  | [optional]  |

### Return type

[**Stream**](Stream.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the queried records. |  -  |
| **400** | If the queries provided could not be parsed. |  -  |
| **404** | If the given tables,  could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="cleardelete"></a>
# **ClearDelete**
> void ClearDelete (Guid project, long table)

Deletes all records from the given table.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ClearDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | The table to delete from.

            try
            {
                // Deletes all records from the given table.
                apiInstance.ClearDelete(project, table);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.ClearDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ClearDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes all records from the given table.
    apiInstance.ClearDeleteWithHttpInfo(project, table);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.ClearDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | The table to delete from. |  |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **404** | If the given table could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **200** |  |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="delete"></a>
# **Delete**
> void Delete (Guid project, long table, int? record = null)

Deletes records from the given table.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

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

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | Id of table where you want to delete a record.
            var record = 56;  // int? | record id to delete. (optional) 

            try
            {
                // Deletes records from the given table.
                apiInstance.Delete(project, table, record);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.Delete: " + e.Message);
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
    // Deletes records from the given table.
    apiInstance.DeleteWithHttpInfo(project, table, record);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.DeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | Id of table where you want to delete a record. |  |
| **record** | **int?** | record id to delete. | [optional]  |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **404** | If the given table could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **200** |  |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletepost"></a>
# **DeletePost**
> void DeletePost (Guid project, int table, List<string>? requestBody = null)

Deletes records from the given table.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeletePostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 56;  // int | The table to delete from.
            var requestBody = new List<string>?(); // List<string>? | An array of ids (optional) 

            try
            {
                // Deletes records from the given table.
                apiInstance.DeletePost(project, table, requestBody);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.DeletePost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeletePostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deletes records from the given table.
    apiInstance.DeletePostWithHttpInfo(project, table, requestBody);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.DeletePostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **int** | The table to delete from. |  |
| **requestBody** | [**List&lt;string&gt;?**](string.md) | An array of ids | [optional]  |

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json


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
> AbstractObjectQueryResult Get (Guid project, long table, long? offset = null, long? take = null, string? joinQuery = null, string? whereQuery = null, string? sortQuery = null, bool? distinct = null, string? select = null)

Gets the records for a given table.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

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

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | Id of the table containing the records.
            var offset = 789L;  // long? | The offset of the records. (optional) 
            var take = 789L;  // long? | The amount of records. (optional) 
            var joinQuery = "joinQuery_example";  // string? | The join queries to apply. (optional) 
            var whereQuery = "whereQuery_example";  // string? | The where queries to apply. (optional) 
            var sortQuery = "sortQuery_example";  // string? | The sort queries to apply. (optional) 
            var distinct = false;  // bool? |  (optional)  (default to false)
            var select = "select_example";  // string? |  (optional) 

            try
            {
                // Gets the records for a given table.
                AbstractObjectQueryResult result = apiInstance.Get(project, table, offset, take, joinQuery, whereQuery, sortQuery, distinct, select);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.Get: " + e.Message);
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
    // Gets the records for a given table.
    ApiResponse<AbstractObjectQueryResult> response = apiInstance.GetWithHttpInfo(project, table, offset, take, joinQuery, whereQuery, sortQuery, distinct, select);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.GetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | Id of the table containing the records. |  |
| **offset** | **long?** | The offset of the records. | [optional]  |
| **take** | **long?** | The amount of records. | [optional]  |
| **joinQuery** | **string?** | The join queries to apply. | [optional]  |
| **whereQuery** | **string?** | The where queries to apply. | [optional]  |
| **sortQuery** | **string?** | The sort queries to apply. | [optional]  |
| **distinct** | **bool?** |  | [optional] [default to false] |
| **select** | **string?** |  | [optional]  |

### Return type

[**AbstractObjectQueryResult**](AbstractObjectQueryResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the queried records. |  -  |
| **400** | If the queries provided could not be parsed. |  -  |
| **404** | If the given tables or fields could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="post"></a>
# **Post**
> AbstractObjectQueryResult Post (Guid project, long table, CreateRecordRequest? createRecordRequest = null)

Adds records to the given table.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

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

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | The table to be added to.
            var createRecordRequest = new CreateRecordRequest?(); // CreateRecordRequest? |  (optional) 

            try
            {
                // Adds records to the given table.
                AbstractObjectQueryResult result = apiInstance.Post(project, table, createRecordRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.Post: " + e.Message);
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
    // Adds records to the given table.
    ApiResponse<AbstractObjectQueryResult> response = apiInstance.PostWithHttpInfo(project, table, createRecordRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.PostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | The table to be added to. |  |
| **createRecordRequest** | [**CreateRecordRequest?**](CreateRecordRequest?.md) |  | [optional]  |

### Return type

[**AbstractObjectQueryResult**](AbstractObjectQueryResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Returns the newly created records. |  -  |
| **404** | If the given tables or fields could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="put"></a>
# **Put**
> AbstractObjectQueryResult Put (Guid project, long table, bool? force = null, UpdateRecordRequest? updateRecordRequest = null)

Updates records in the given table based on the values in record.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

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

            var apiInstance = new DataApi(config);
            var project = "project_example";  // Guid | The project containing the table.
            var table = 789L;  // long | The table to be updated in.
            var force = false;  // bool? | If you are certain you want to update all your records in case no ids were included. (optional)  (default to false)
            var updateRecordRequest = new UpdateRecordRequest?(); // UpdateRecordRequest? |  (optional) 

            try
            {
                // Updates records in the given table based on the values in record.
                AbstractObjectQueryResult result = apiInstance.Put(project, table, force, updateRecordRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DataApi.Put: " + e.Message);
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
    // Updates records in the given table based on the values in record.
    ApiResponse<AbstractObjectQueryResult> response = apiInstance.PutWithHttpInfo(project, table, force, updateRecordRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DataApi.PutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **project** | **Guid** | The project containing the table. |  |
| **table** | **long** | The table to be updated in. |  |
| **force** | **bool?** | If you are certain you want to update all your records in case no ids were included. | [optional] [default to false] |
| **updateRecordRequest** | [**UpdateRecordRequest?**](UpdateRecordRequest?.md) |  | [optional]  |

### Return type

[**AbstractObjectQueryResult**](AbstractObjectQueryResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the updated records. |  -  |
| **404** | If the given tables or fields could not be found. |  -  |
| **500** | If something went wrong in the database. |  -  |
| **401** | If the user is not signed in. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

