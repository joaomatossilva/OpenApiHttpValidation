This library provide a way to perform validation of the calls to your api against an open api spec.

## Usage

you can embbed this library on your integration tests like this

```
OpenApiDocument document = // get the api document
HttpClient httpClient = new HttpClient(new OpenApiValidatorHandler(document));

var data = await httpClient.GetAsync<SomeData>(url);
```

After the request is done to the server, both the request and the response are validated against the spec.
If a match isnt't found an Exception is thrown.