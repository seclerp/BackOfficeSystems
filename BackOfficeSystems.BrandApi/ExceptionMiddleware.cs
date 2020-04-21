﻿using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BackOfficeSystems.BrandApi.Api.Models;
using BackOfficeSystems.BrandApi.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BackOfficeSystems.BrandApi
{
    /// <summary>
    /// Handles exceptions and creates unified error response
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly JsonSerializerOptions _responseSerializerOptions;
        private readonly XmlSerializer _xmlSerializer;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _responseSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            _xmlSerializer = new XmlSerializer(typeof(ErrorResponse));
            _next = next;
        }

        // ReSharper disable once UnusedMember.Global
        // Used implicitly by ASP.NET Core middleware pipeline
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = exception switch
            {
                var ex when ex is BrandNotFoundException brandNotFound =>
                    new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Brand with ID {brandNotFound.BrandId} not found"
                    },

                _ =>
                    new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error occured. see server logs"
                    }
            };

            // Trying to determine response content type using Accept header, fallback to JSON
            switch (context.Request.Headers["Accept"])
            {
                case "application/xml":
                    context.Response.ContentType = "application/xml";
                    using (var textWriter = new StringWriter())
                    {
                        _xmlSerializer.Serialize(textWriter, errorResponse);
                        return context.Response.WriteAsync(textWriter.ToString());
                    }

                default:
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, _responseSerializerOptions));
            }
        }
    }
}