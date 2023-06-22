using System.Security.Cryptography;
using LightCV.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LightCV.Controllers;

public class ProfileController : Controller
{
    [HttpGet]
    [Route("/profile")]
    public IActionResult Index()
    {
        return View(new ProfileViewModel());
    }
    
    [HttpPost]
    [Route("/profile")]
    public async Task<IActionResult> IndexSave()
    {
        string fileName = "";
        var imageDate = Request.Form.Files[0];

        if (imageDate != null)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageDate.FileName);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            var dir = "./wwwroot/images/" 
                      + hash.Substring(0, 2) 
                      + "/" + hash.Substring(0, 4);

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            fileName = dir + "/" + imageDate.FileName;

            using (var stream = System.IO.File.Create(fileName))
            {
                await imageDate.CopyToAsync(stream);
            }
        }
        
        return View("Index", new ProfileViewModel());
    }
}