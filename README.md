# Clean Architecture using Repository Design Pattern
## Features :
### Fluent Validation On DTOModel
```
 RuleFor(x => x.Name)
     .NotNull().WithMessage("Customer Name is empty")
     .NotEmpty().WithMessage("Customer Name is empty");
 
 RuleFor(x => x.Contact)
     .NotNull().WithMessage("Mobile Number is empty.")
     .NotEmpty().WithMessage("Mobile Number is empty.")
     .Matches(@"^[0-9]{10}$").WithMessage("Mobile Number is invalid.");

 RuleFor(x => x.Email)
     .NotNull().WithMessage("Customer Email is empty")
     .NotEmpty().WithMessage("Customer Email is empty")
     .EmailAddress().WithMessage("Email Address is invalid");
```
### AutoMapper  
```
 CreateMap<Customer, CustomerDTO>().ReverseMap();
 _mapper.Map<List<CustomerDTO>>(customers);
```
### Standard Success and Error API Responses

#### Success Response
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
#### Error catch by Model validator
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

#### Error catch by API Exception
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