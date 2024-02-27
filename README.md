# NET 8 Clean Architecture using Repository Design Pattern
## System Features :
### API Versioning
API versioning is the practice of managing changes to an API by assigning unique identifiers, typically version numbers, to different releases, ensuring compatibility and smooth transitions for developers and users.
```c#
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = false;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});
```

### Configure Core Dependency Injection Services
Configuring Core Dependency Injection Services involves centralizing the management of dependencies within the Program.cs file in ASP.NET Core applications. This allows for streamlined and organized handling of services, making it easier to manage dependencies and facilitate inversion of control throughout the application.
```c#
builder.Services.AddInfrastructure();
builder.Services.AddLogicServices();
```

### Fluent Validation On DTOModel
Fluent Validation on DTO Models refers to the practice of using the Fluent Validation library to define validation rules for Data Transfer Object (DTO) models in an application. This approach offers a declarative and fluent way to express validation logic, ensuring that data passed through DTOs meets specific criteria before processing, enhancing data integrity and reliability in the application.
```c#
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
AutoMapper is a popular object-to-object mapping library in .NET that simplifies the process of mapping one object's properties to another object's properties. It automates the mapping process, reducing the need for manual coding and increasing productivity. AutoMapper works by convention but also allows for customization, making it flexible and adaptable to various mapping scenarios. It's commonly used in applications to streamline data transfer between layers, such as mapping database entities to DTOs (Data Transfer Objects) or vice versa. Overall, AutoMapper simplifies complex mapping tasks and helps maintain cleaner, more concise code.
```c#
 CreateMap<Customer, CustomerDTO>().ReverseMap();
 _mapper.Map<List<CustomerDTO>>(customers);
```
### Standard Success and Error API Responses
Standard Success and Error API Responses ensure consistent formats for communicating the outcome of API requests. Success responses confirm successful operations with structured data, while error responses convey details about encountered issues, aiding in uniform handling and comprehension by client applications.
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