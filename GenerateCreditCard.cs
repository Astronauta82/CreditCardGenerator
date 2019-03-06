using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CreditCardGenerator.Extensions;
using CreditCardGenerator.Models;
using CreditCardGenerator.Services;

namespace CreditCardGenerator
{
    public static class GenerateCreditCard
    {
        [FunctionName("GenerateCreditCard")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Generating Credit Card");
            ActionResult response;
            try
            {
                CreditCardPost creditCardSelected = await req.ReadAsAsync<CreditCardPost>();
                if (creditCardSelected != null)
                {
                    
                    CreditCardService service = new CreditCardService();
                    log.LogInformation("Getting CreditCardType");
                    
                    CreditCardType creditCardType = service.GetCreditCardTypeById(creditCardSelected.TypeId);
                    if (creditCardType != null){
                        creditCardType.CustomPrefix =creditCardSelected.CustomPrefix;
                        CreditCard creditCard = service.GenerateCreditCard(creditCardType);

                        response = new OkObjectResult(creditCard);
                    }else{

                        response = new NotFoundObjectResult("CreditCardType doesn't exists");
                    }
                }
                else
                {
                    var errorMessage = "Failed to parse model";
                    log.LogError(errorMessage);
                    response = new BadRequestObjectResult(errorMessage);
                }
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                response =  new BadRequestObjectResult("Ineternal Server Error");
            }
            return response;
        }
    }
}
