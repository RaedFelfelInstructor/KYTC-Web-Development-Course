using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo_App.Controllers
{
    public class HomeController : Controller
    {
        // GET Request Example
        public IActionResult Index()
        {
            // Show information about the current request
            ViewBag.Method = Request.Method;
            ViewBag.UserAgent = Request.Headers["User-Agent"].ToString();
            ViewBag.Path = Request.Path;
            ViewBag.QueryString = Request.QueryString.Value;

            return View();
        }

        // Demonstrates different status codes
        public IActionResult StatusCodes(int code = 200)
        {
            switch (code)
            {
                case 200:
                    ViewBag.StatusMessage = "200 OK - Request succeeded normally";
                    return View();
                case 404:
                    ViewBag.StatusMessage = "404 Not Found - Resource doesn't exist";
                    return NotFound("Custom 404 message");
                case 400:
                    ViewBag.StatusMessage = "400 Bad Request - Invalid request from client";
                    return BadRequest("Bad request demonstration");
                case 500:
                    ViewBag.StatusMessage = "500 Internal Server Error - Server error occurred";
                    throw new Exception("Simulated server error for demonstration");
                default:
                    return View();
            }
        }

        // Demonstrates GET method with parameters
        public IActionResult GetDemo(string name, int? id)
        {
            ViewBag.Method = Request.Method;
            ViewBag.Name = name ?? "No name provided";
            ViewBag.Id = id.HasValue ? id.Value.ToString() : "No ID provided";

            return View();
        }

        // GET: Form Page for POST demonstration
        public IActionResult PostForm()
        {
            return View();
        }

        // POST: Receive data from form
        [HttpPost]
        public IActionResult PostForm(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Both username and email are required";
                return View();
            }

            ViewBag.Method = Request.Method;
            ViewBag.Username = username;
            ViewBag.Email = email;

            return View("PostResult");
        }

        // Demonstrates JSON response and content types
        public IActionResult JsonDemo()
        {
            var data = new
            {
                message = "This is a JSON response",
                timestamp = DateTime.Now,
                items = new[] { "Item 1", "Item 2", "Item 3" }
            };

            return Json(data);
        }

        // Demonstrates headers in both request and response
        public IActionResult HeadersDemo()
        {
            // Collect request headers
            var requestHeaders = new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders[header.Key] = header.Value.ToString();
            }

            // Set some custom response headers
            Response.Headers.Add("X-Demo-Header", "Hello from ASP.NET Core");
            Response.Headers.Add("X-Server-Time", DateTime.Now.ToString("o"));

            ViewBag.RequestHeaders = requestHeaders;

            return View();
        }

        // Demonstrates 302 redirect
        public IActionResult RedirectDemo()
        {
            // This will cause a 302 redirect
            return Redirect("/Home/Index?redirected=true");
        }
    }
}