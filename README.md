## Clean Architecture using Repository Design Pattern
## Fluent Validation On DTOModel
## Standard Success and Error API Responses

### Success Response
```json
{
  "response": {
    "success": true,
    "message": "Customer successfully created.",
    "data": {
      "customerId": 12
    }
  }
}
```
### Error catch by Model validator
```json
{
  "errorResponse": {
    "title": "One or more validation errors occurred.",
    "status": 400,
    "detail": "'Name' must not be empty.",
    "instances": "/api/1.0/customer"
  }
}
```

### Error catch by API Exception
```json
{
  "response": {
    "Title": "One or more validation errors occurred.",
    "Status": 400,
    "Detail": "Customer not found",
    "Instances": null
  }
}
```