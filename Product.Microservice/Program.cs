
using Common.Exceptions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Model;
using Newtonsoft.Json;
using static Common.Constants.Enums;
using Infrastructure.Configuration;
using Services.Configuration;
using Common.Constants;

namespace Product.Microservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            //Configure Core DI Services
            builder.Services.AddInfrastructure();
            builder.Services.AddLogicServices();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            //API Versioning
            builder.Services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = false;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });


            // Configure Model Validator
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            #region Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\Product.Microservice.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Customer Microservice API",
                });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Microservice API v1");
                });
            }

            // Configure Custom ApiException
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    ApiException? ex;
                    const string genericErrorMessage = "Oops something went wrong, Please try again!";
                    if (errorFeature != null && errorFeature.Error.GetType() == typeof(ApiException))
                    {
                        ex = errorFeature.Error as ApiException;
                    }
                    else
                    {
                        var errMsg = errorFeature == null ? "" : errorFeature.Error.Message;
                        ex = new ApiException(errMsg, errorFeature.Error);
                    }

                    if (ex != null && !ex.IsHandled && ex.StatusCode == NotificationTypes.INTERNALSERVERERROR)
                    {
                        // WriteToSeriLogs(ex, context)
                    }

                    ErrorResponseDTO errorResponseDTO = new();
                    errorResponseDTO.Title = Message.ERROR_TITLE;
                    errorResponseDTO.Instances = null;
                    errorResponseDTO.Status = (int)ex.StatusCode;

                    context.Response.StatusCode = (int)ex.StatusCode;

                    context.Response.ContentType = "application/json";
                    object response;

                    if (ex != null && ex.StatusCode == NotificationTypes.INTERNALSERVERERROR)
                    {
                        errorResponseDTO.Detail = genericErrorMessage;
                    }
                    else
                    {
                        errorResponseDTO.Detail = ex == null ? "" : ex.ErrorMessage;
                    }

                    response = new { errorResponse = errorResponseDTO };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                });
            });

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
