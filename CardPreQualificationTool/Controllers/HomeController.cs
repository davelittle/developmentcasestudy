using CardPreQualificationTool.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CardPreQualificationTool.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ICardApplicationLogger _logger = new FileCardApplicationLogger();

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
            if (ModelState.IsValid)
            {
                Boolean accepted = false;
                CreditCard creditCard = null;
                Rejection rejection = null;

                if (applicant.DateOfBirth.AddYears(18) > DateTime.Now)
                {
                    rejection = new Rejection() { Reason = "You are under 18 years of age." };
                }
                else
                {
                    accepted = true;

                    if (applicant.AnnualIncome > 30000)
                    {
                        // In real life we would use our own images of the credit cards, rather than pointing at images on the Barclaycard
                        // and Vanquis websites (which may change or move)
                        creditCard = new CreditCard() { 
                            CardType = "Barclaycard", 
                            ImageURL = "https://www.barclaycard.co.uk/content/dam/barclaycard/images/personal/credit-cards/hero-images/barclaycard-platinum-credit-card.xsmall.medium_quality.png", 
                            InterestRate = 9.9M, 
                            Description = "Making payment simpler." };
                    }
                    else
                    {
                        creditCard = new CreditCard() { 
                            CardType = "Vanquis",
                            ImageURL = "https://cdn-p.vanquis.co.uk/media/2104376/midnight-blue-flat.png",
                            InterestRate = 29.9M, 
                            Description = "Stay in control of your budgeting." };
                    }
                }

                _logger.Log(new LogEntry() { Applicant = applicant, CreditCard = creditCard, Rejection = rejection });

                // Redirect to the Accepted or Declined page depending on the outcome of the decision
                if (accepted)
                {
                    return RedirectToAction("Accepted", creditCard);
                }

                return RedirectToAction("Declined", rejection);
            }

            return View(applicant);
        }

        // GET: /Accepted
        // This page is displayed if the user has been accepted for a credit card
        public IActionResult Accepted([Bind("CardType,ImageURL,InterestRate,Description")] CreditCard creditCard)
        {
            return View(creditCard);
        }

        // GET: /Declined
        // This page is displayed if the user has been refused a credit card
        public IActionResult Declined([Bind("Reason")] Rejection rejection)
        {
            return View(rejection);
        }
    }
}
