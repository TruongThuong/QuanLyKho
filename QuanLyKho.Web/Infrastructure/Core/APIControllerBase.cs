using System;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuanLyKho.Model.Models;
using QuanLyKho.Service;

namespace QuanLyKho.Web.Infrastructure.Core
{    
    //[Route("api/[controller]")]
    public class APIControllerBase : Controller, IExceptionFilter
    {
        private IErrorService _errorService;
        private ExceptionContext _context;

        public APIControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = _context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(Error))
            {
                message = _context.Exception.ToString();
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = _context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }
            HttpResponse response = _context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message + " " + _context.Exception.StackTrace;
            _context = context;
            LogError(_context.Exception);
            response.WriteAsync(err);
        }

        //protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        //{
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        response = function.Invoke();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var eve in ex.EntityValidationErrors)
        //        {
        //            Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
        //            }
        //        }
        //        LogError(ex);
        //        response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        LogError(dbEx);
        //        response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //        response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //    return response;
        //}

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.stackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }
    }
}