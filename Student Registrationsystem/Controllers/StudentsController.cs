using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Registrationsystem.Models;
using Student_Registrationsystem.Repository;
using System;

namespace Student_Registrationsystem.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        public static void LogAndNotifyUser(Exception ex)
        {
            // Log the exception
            LogException(ex);

            // Notify the user with a custom error message
            // Note: This line might not work in ASP.NET Core as it uses HttpContextAccessor instead of HttpContext directly.
            // You may need to handle exception logging and user notification differently in ASP.NET Core.
            // HttpContextAccessor can be injected into the controller's constructor to access HttpContext.
            // For demonstration purposes, I'm using Console.WriteLine to print the error message.
            Console.WriteLine("An error occurred. Please try again later or contact support.");

            // Alternatively, you can use a logging framework like Serilog or NLog to log the exception to a file or a database.
        }

        private static void LogException(Exception ex)
        {
            // Your logging logic goes here
            // For demonstration purposes, we'll just log the exception details to the console
            Console.WriteLine("Exception Message: " + ex.Message);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
        }

        studentsRepository repo;

        public StudentsController()
        {
            repo = new studentsRepository();
        }

        public IActionResult Index()
        {
            try
            {
                var data = repo.getAll();
                return View(data);
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Index"); // Redirect to the same action or an error page
            }
        }

        public IActionResult Create()
        {
            try
            {
                /*if (HttpContext.Session.GetString("username") == null)
                {
                    return RedirectToAction("Login", "login");
                }*/
                return View();
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return Content("Something Is wrong please try again"); // Redirect to the same action or an error page
            }
        }

        [HttpPost]
        public IActionResult Create(Students model)
        {
            try
            {
                repo.create(model.std_Fullname, model.std_Phone, model.std_age.ToString());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Create"); // Redirect to the same action or an error page
            }
        }

        public IActionResult Edit(int id, string std_Fullname, string std_Phone, string std_age)
        {
            try
            {
                var found = repo.get_by_id(id);
                return View(found);
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Index"); // Redirect to the same action or an error page
            }
        }

        [HttpPost]
        public IActionResult Edit(Students model)
        {
            try
            {
                repo.update(model.Id, model.std_Fullname, model.std_Phone, model.std_age.ToString());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Index"); // Redirect to the same action or an error page
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var found = repo.get_by_id(id);
                return View(found);
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Index"); // Redirect to the same action or an error page
            }
        }

        [HttpPost]
        public IActionResult Delete(Students model)
        {
            try
            {
                repo.update(model.Id, model.std_Fullname, model.std_Phone, model.std_age.ToString());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogAndNotifyUser(ex);
                return RedirectToAction("Index"); // Redirect to the same action or an error page
            }
        }
    }
}
