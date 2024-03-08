using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Student_Registrationsystem.Models;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace Student_Registrationsystem.Controllers
{
    public class loginController : Controller
    {
        public IActionResult Login()
        {
            ViewData["error"] = "";
            return View();

        }
        [HttpPost]
        public IActionResult Login(userLogin Model)
        {
            //check from db
           // try
           // {

                string connString = "server=MUSTAFEALI\\SQLEXPRESS; database=studentRegistration; Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string query = $"select count (*) total from users where username='{Model.Username}' and password='{Model.Password}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        //checks if user valid
                        //if the user is true create session
                        HttpContext.Session.SetString("username", Model.Username);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, Model.Username),
                        new Claim(ClaimTypes.Role, "Admin")

                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var princible = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(princible);




                    return RedirectToAction("Index", "Home");


                }
                else
                    {
                        // if not the user is valid print this
                        ViewData["error"] = "Invalid Credentials";
                        return View(Model);
                    }
                   
                }
            }
        //  catch (ArgumentException e)
        //  {
        //       Console.WriteLine($"Processing failed: {e.Message}");
        //   }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("LoginSession");
            HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }

    }
}
    
