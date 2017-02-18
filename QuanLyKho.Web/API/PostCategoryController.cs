using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKho.Model.Models;
using QuanLyKho.Service;
using QuanLyKho.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [Route("api/postcategory")]
    public class PostCategoryController : APIControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var listCategory = _postCategoryService.GetAll();

            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);

            return response;
        }

        public HttpResponseMessage Post(HttpRequestMessage request, PostCategory postCategory)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                _postCategoryService.Add(postCategory);
                _postCategoryService.Save();

                response = request.CreateResponse(HttpStatusCode.Created, postCategory);
            }
            return response;
            
        }

        public HttpResponseMessage Put(HttpRequestMessage request, PostCategory postCategory)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                _postCategoryService.Update(postCategory);
                _postCategoryService.Save();

                response = request.CreateResponse(HttpStatusCode.OK);
            }
            return response;

        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
        }
    }
}