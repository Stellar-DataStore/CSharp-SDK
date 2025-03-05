# StellarDS.SDK - the C# library for the Stellar DataStore

The Stellar DataStore service is your instantly available, secure and worry-free cloud data storage service. Sign-up now and minutes later, you can start putting your data in the cloud.


- API version: v1
- SDK version: 1.0.0
- Generator version: 7.11.0

<a id="frameworks-supported"></a>
## Frameworks supported

<a id="dependencies"></a>
## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 112.0.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 13.0.2 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.8.0 or later
- [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) - 5.0.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
```

<a id="installation"></a>
## Installation


<a id="usage"></a>
## Usage

To use the API client with a HTTP proxy, setup a `System.Net.WebProxy`
```csharp
Configuration c = new Configuration();
System.Net.WebProxy webProxy = new System.Net.WebProxy("http://myProxyUrl:80/");
webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
c.Proxy = webProxy;
```

<a id="getting-started"></a>
## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class Example
    {
        public static void Main()
        {

            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.ApiKey.Add("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.ApiKeyPrefix.Add("Authorization", "Bearer");

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
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DataApi.BlobGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }

        }
    }
}
```

<a id="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *http://localhost*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*DataApi* | [**BlobGet**](docs/DataApi.md#blobget) | **GET** /v1/data/table/blob | Download a file from a blob field as a stream.
*DataApi* | [**BlobPost**](docs/DataApi.md#blobpost) | **POST** /v1/data/table/blob | Upload a file to a blob field.
*DataApi* | [**ClearDelete**](docs/DataApi.md#cleardelete) | **DELETE** /v1/data/table/clear | Deletes all records from the given table.
*DataApi* | [**Delete**](docs/DataApi.md#delete) | **DELETE** /v1/data/table | Deletes records from the given table.
*DataApi* | [**DeletePost**](docs/DataApi.md#deletepost) | **POST** /v1/data/table/delete | Deletes records from the given table.
*DataApi* | [**Get**](docs/DataApi.md#get) | **GET** /v1/data/table | Gets the records for a given table.
*DataApi* | [**Post**](docs/DataApi.md#post) | **POST** /v1/data/table | Adds records to the given table.
*DataApi* | [**Put**](docs/DataApi.md#put) | **PUT** /v1/data/table | Updates records in the given table based on the values in record.
*FieldApi* | [**Delete**](docs/FieldApi.md#delete) | **DELETE** /v1/schema/table/field | Deletes the given field within the given table.
*FieldApi* | [**Get**](docs/FieldApi.md#get) | **GET** /v1/schema/table/field | Gets field(s) from given table.
*FieldApi* | [**Post**](docs/FieldApi.md#post) | **POST** /v1/schema/table/field | Adds a field to the given table.
*FieldApi* | [**Put**](docs/FieldApi.md#put) | **PUT** /v1/schema/table/field | Updates the given field in the given table.
*OAuthApi* | [**RevokePost**](docs/OAuthApi.md#revokepost) | **POST** /v1/oauth/revoke | 
*OAuthApi* | [**TokenPost**](docs/OAuthApi.md#tokenpost) | **POST** /v1/oauth/token | 
*ProjectApi* | [**ProjectGet**](docs/ProjectApi.md#get) | **GET** /v1/schema/project | Gets project(s) from the logged in user.
*ProjectApi* | [**ProjectPut**](docs/ProjectApi.md#put) | **PUT** /v1/schema/project | Updates a project.
*ProjectTierApi* | [**CurrentGet**](docs/ProjectTierApi.md#currentget) | **GET** /v1/project-tier/current | 
*ProjectTierApi* | [**Get**](docs/ProjectTierApi.md#get) | **GET** /v1/project-tier | 
*TableApi* | [**Delete**](docs/TableApi.md#delete) | **DELETE** /v1/schema/table | Deletes a table from the database.
*TableApi* | [**Get**](docs/TableApi.md#get) | **GET** /v1/schema/table | Gets table(s) from the logged in user.
*TableApi* | [**Post**](docs/TableApi.md#post) | **POST** /v1/schema/table | Creates a new table in the database.
*TableApi* | [**Put**](docs/TableApi.md#put) | **PUT** /v1/schema/table | Updates a table in the database.
*TestApi* | [**Get**](docs/TestApi.md#get) | **GET** /v1/ping | 
*UserApi* | [**Delete**](docs/UserApi.md#delete) | **DELETE** /v1/user | 
*UserApi* | [**Get**](docs/UserApi.md#get) | **GET** /v1/user | 
*UserApi* | [**PermissionsGet**](docs/UserApi.md#permissionsget) | **GET** /v1/user/permissions | 


<a id="documentation-for-models"></a>
## Documentation for Models

 - [Model.AbstractObjectQueryResult](docs/AbstractObjectQueryResult.md)
 - [Model.AbstractObjectServiceResult](docs/AbstractObjectServiceResult.md)
 - [Model.CreateRecordRequest](docs/CreateRecordRequest.md)
 - [Model.FieldRequest](docs/FieldRequest.md)
 - [Model.FieldResponse](docs/FieldResponse.md)
 - [Model.FieldResponseServiceResult](docs/FieldResponseServiceResult.md)
 - [Model.IServiceResult](docs/IServiceResult.md)
 - [Model.ProjectRequest](docs/ProjectRequest.md)
 - [Model.RevokeTokenRequest](docs/RevokeTokenRequest.md)
 - [Model.ServiceMessage](docs/ServiceMessage.md)
 - [Model.ServiceMessageType](docs/ServiceMessageType.md)
 - [Model.ServiceResult](docs/ServiceResult.md)
 - [Model.Stream](docs/Stream.md)
 - [Model.TableRequest](docs/TableRequest.md)
 - [Model.TableResponse](docs/TableResponse.md)
 - [Model.TableResponseIEnumerableServiceResult](docs/TableResponseIEnumerableServiceResult.md)
 - [Model.TokenResponse](docs/TokenResponse.md)
 - [Model.UpdateRecordRequest](docs/UpdateRecordRequest.md)


<a id="documentation-for-authorization"></a>
## Documentation for Authorization


Authentication schemes defined for the API:
<a id="Bearer"></a>
### Bearer

- **Type**: API key
- **API key parameter name**: Authorization
- **Location**: HTTP header

