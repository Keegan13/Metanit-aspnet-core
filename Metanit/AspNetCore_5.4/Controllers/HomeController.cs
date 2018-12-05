using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_5._4.Models;
using IActionResultImplementation;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.IO.IsolatedStorage;
using AspNetCore_5._4.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using AspNetCore_5._4.Controllers;

namespace AspNetCore_5._4.Controllers
{

    namespace Extensions
    {
        public static partial class ControllerExtensions
        {

            public static string GetMimeTypeByWindowsRegistry(string fileNameOrExtension)
            {
                string mimeType = "application/unknown";
                string ext = (fileNameOrExtension.Contains(".")) ? System.IO.Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
                return mimeType;
            }
        }
    }
    public class DownloadController : Controller
    {
        public IActionResult GetFile(string name)
        {
            if (String.IsNullOrEmpty(name)&&
                String.IsNullOrEmpty(name=HttpContext.GetRouteValue("file") as string))
                    goto NotFound;

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory()).GetDirectories("*",SearchOption.AllDirectories).FirstOrDefault(x => x.Name == "downloads");

            if (dir == null)
                goto NotFound;

            var file = dir.EnumerateFiles(name + "*", searchOption: SearchOption.AllDirectories).FirstOrDefault();

            if (file != null)
            {

                string ContentType = ControllerExtensions.GetMimeTypeByWindowsRegistry(file.FullName);

                byte[] output = System.IO.File.ReadAllBytes(System.IO.Path.Combine(file.DirectoryName, file.FullName));
                return File(output, ContentType,file.Name);
            }

        NotFound:
            return new NotFoundResult();
        }

    }

    [Controller]
    public class HomeController : AbstractController
    {
        
        
        public HomeController()
        {
            this.ControllerId = new Random().Next(0,100);
            
        }

        #region hide
        public class MyClass
        {
            public int MyProperty1 { get; set; }
            public int MyProperty2 { get; set; }
            public TaskStatus Status { get; set; }

            public AnotherClass Classss { get; set; }
        }
    public class AnotherClass
    {
        public int MyProperty { get; set; }
    }


        //AcceptedAtActionResult
        //AcceptedAtRouteResult
        //AcceptedResult
        //ActionResult
        //BadRequestObjectResult
        //BadRequestResult
        //ChallengeResult
        //ConflictObjectResult
        //ConflictResult
        //ContentResult
        //CreatedAtActionResult
        //CreatedAtRouteResult
        //CreatedResult
        //EmptyResult
        //FileContentResult
        //FileResult
        //FileStreamResult
        //ForbidResult
        //LocalRedirectResult
        //NoContentResult
        //NotFoundObjectResult
        //NotFoundResult
        //ObjectResult
        //OkObjectResult
        //OkResult
        //PhysicalFileResult
        //RedirectResult
        //RedirectToActionResult
        //RedirectToPageResult
        //RedirectToRouteResult
        //SignInResult
        //SignOutResult
        //StatusCodeResult
        //UnauthorizedResult
        //UnprocessableEntityObjectResult
        //UnprocessableEntityResult
        //UnsupportedMediaTypeResult
        //VirtualFileResult
        //ViewFeatures
        //RazorPages
        //IActionResult
        //PartialViewResult
        //ViewComponentResult
        //ViewResult
        //JsonResult

#endregion
        
        public MyClass GetMyClass()
        {
            return new MyClass {Classss=new AnotherClass { MyProperty=4} };
        }
        
        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult GetControllerContext()
        {
            return Json(this.ControllerContext);
        }



        public LocalRedirectResult LocalRedirectResult()
        {
            return new LocalRedirectResult("~/home/index");
        }
        public IActionResult GetRedirects()
        {
            List<string> Redirects = new List<string>();

            Redirects.Add("RedirectResult");
            Redirects.Add("LocalRedirect");
            Redirects.Add("RedirectToRoute");

            return View(Redirects);
            //
        }

        #region redirects

        public RedirectResult RedirectResult()
        {
            return Redirect("https://hotline.ua");//status code 302
        }
        public IActionResult LocalRedirect()
        {
            return LocalRedirect("~/home/index");
        }

        public RedirectToRouteResult RedirectToRoute()
        {
            return RedirectToRoute("download", new { file = "site.css" });
        }

        #endregion

        public ViewResult GetCode()
        {
            return View(viewName:"Code");
        }

        public UnauthorizedResult UnauthorizedResult()
        {
            return new UnauthorizedResult();
        }

        //custom IActionResult
        public IActionResult GetActionResult()
        {
            
            return new MyActionResultImplementatation();
        }

        public IActionResult GetImplementations()
        {
            var type = typeof(IActionResult);
            var domains = AppDomain.CurrentDomain.GetAssemblies();
            var types = domains
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));


            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(types, Newtonsoft.Json.Formatting.Indented)+Environment.NewLine
                +types.Count()+Environment.NewLine+domains.Count());
        }

        //json result
        public JsonResult GetJsonResult()
        {
            return Json(HttpContext.GetRouteData());
        }

        //Content result
        public ContentResult GetContentResult()
        {
            return Content("This is content result");
        }

        //get file result
        public FileResult GetFileResult()
        {
            return File(virtualPath: "~\\css\\site.min.css", contentType: "text/css", fileDownloadName: "upload.css");
        }

        public PhysicalFileResult GetPhysicalFileResult()
        {
            return new PhysicalFileResult(fileName: System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\css\\site.min.css", contentType: "text/css");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as HomeController;
            context.HttpContext.Items.Add(5, controller.ControllerId.ToString());
            base.OnActionExecuted(context);
        }


    }
}
