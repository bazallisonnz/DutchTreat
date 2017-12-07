using System.Linq;
using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;

namespace DutchTreat.Controllers
{
    using DutchTreat.Services;
    using DutchTreat.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _repository = repository;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send the email
                _mailService.SendMessage("bazallison@hotmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }

            return View();
        }
        
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            ViewBag.Title = "Shop";

            var results = _repository.GetAllProducts();

            return View(results);
        }
    }
}
