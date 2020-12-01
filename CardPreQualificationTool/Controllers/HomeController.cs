using CardPreQualificationTool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CardPreQualificationTool.Controllers
{
    public class HomeController : Controller
    {
        private static ICardApplicationLogger _logger;
        private static readonly DecisionRepository _decisions = new DecisionRepository();
        private static string _errorFilePath;

        public HomeController(IOptions<RequestLoggingConfig> config)
        {
            _errorFilePath = config.Value?.ErrorFilePath ?? "error.txt";

            try
            {
                string connectionString = config.Value?.ConnectionString;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    _logger = new DatabaseCardApplicationLogger(connectionString);
                }
                else
                {
                    _logger = new FileCardApplicationLogger(config.Value?.RequestFilePath ?? "log.txt");
                }
            }
            catch (Exception e)
            {
                // There shouldn't be an exception thrown in this constructor (we only open the database/file for logging when we actually
                // need to write to them). But if there is an exception, we log it and rethrow so that we don't present the application form to the user
                LogError(e);
                throw;
            }
        }

        // GET: /
        // This is the main application form for a credit card
        public IActionResult Index()
        {
            return View();
        }

        // POST: /
        // The Apply button has been clicked on the application form. 
        // Decide which card should be offered, if any.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("FirstName,LastName,DateOfBirth,AnnualIncome")] Applicant applicant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Decision decision;

                    if (applicant.DateOfBirth.AddYears(18) > DateTime.Now)
                    {
                        decision = new Rejection("You are under 18 years of age.");
                    }
                    else
                    {
                        CreditCard creditCard;

                        if (applicant.AnnualIncome > 30000)
                        {
                            creditCard = new CreditCard()
                            {
                                CardType = "Barclaycard",
                                ImageURL = "~/Images/barclaycard.png",
                                InterestRate = 9.9M,
                                Description = "Making payment simpler."
                            };
                        }
                        else
                        {
                            creditCard = new CreditCard()
                            {
                                CardType = "Vanquis",
                                ImageURL = "~/Images/vanquis.png",
                                InterestRate = 29.9M,
                                Description = "Stay in control of your budgeting."
                            };
                        }

                        decision = new Acceptance(creditCard);
                    }

                    _logger.Log(new LogEntry(applicant, decision));
                    Guid requestId = _decisions.Add(decision);

                    // Redirect to the Accepted or Declined page depending on the outcome of the decision
                    if (decision is Acceptance)
                    {
                        return RedirectToAction("Approved", new { requestId = requestId.ToString() });
                    }

                    return RedirectToAction("Declined", new { requestId = requestId.ToString() });
                }

                return View(applicant);
            }
            catch (Exception e)
            {
                // Redirect to Error page if an exception occurred (e.g. we couldn't write to the log file)
                LogError(e);
                return RedirectToAction("Error");
            }
        }

        // GET: /Accepted
        // This page is displayed if the user has been accepted for a credit card
        public IActionResult Approved([Bind("requestId")] string requestId)
        {
            Decision decision = _decisions.Get(new Guid(requestId));

            if (decision is Acceptance acceptance)
            {
                return View(acceptance.CreditCard);
            }

            // An invalid request ID was supplied - return 'page not found'
            return StatusCode(404);
        }

        // GET: /Declined
        // This page is displayed if the user has been refused a credit card
        public IActionResult Declined([Bind("RequestId")] string requestId)
        {
            Decision decision = _decisions.Get(new Guid(requestId));

            if (decision is Rejection rejection)
            {
                return View(rejection);
            }

            // An invalid request ID was supplied - return 'page not found'
            return StatusCode(404);
        }

        // GET: /Error
        // This page is displayed if an exception was thrown
        public IActionResult Error()
        {
            return View();
        }

        public void LogError(Exception e)
        {
            using StreamWriter writer = System.IO.File.AppendText(_errorFilePath);
            writer.WriteLine($"{DateTime.Now} {e}");
        }
    }
}
