using CurationAssistant.Helpers;
using CurationAssistant.Models;
using CurationAssistant.Models.SteemModels;
using SteemAPI.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurationAssistant.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ValidatePost(HomeViewModel model)
        {
            var result = new CurationDetailsViewModel();

            try
            {
                var linkItems = model.PostLink.Split('/');
                var accountName = linkItems.FirstOrDefault(x => x.Contains("@"));
                var permlink = linkItems.LastOrDefault();

                accountName = accountName.Replace("@", "");
                result.Author = GetAuthor(accountName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return Json(result);
        }

        private AuthorViewModel GetAuthor(string accountName)
        {
            var author = new AuthorViewModel();
            try
            {
                var globalProperties = GetDynamicGlobalProperties();
                var steemPerMVests = CalculationHelper.CalculateSteemPerMVests(globalProperties);
                author.Details = GetAccountDetails(accountName);
                author.ReputationCalculated = CalculationHelper.CalculateReputation(author.Details.reputation);
                author.SteemPowerCalculated = CalculationHelper.CalculateSteemPower(author.Details.vesting_shares, steemPerMVests);
                author.VotingManaPercentageCalculated = CalculationHelper.CalculateVotingManaPercentage(author.Details);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return author;
        }

        private GetDynamicGlobalPropertiesModel GetDynamicGlobalProperties()
        {
            var model = new GetDynamicGlobalPropertiesModel();

            try
            {
                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    var response = csteemd.get_dynamic_global_properties();
                    if (response != null)
                    {
                        var result = response.ToObject<GetDynamicGlobalPropertiesModel>();
                        if (result != null)
                        {
                            model = response.ToObject<GetDynamicGlobalPropertiesModel>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return model;
        }

        private GetAccountsModel GetAccountDetails(string accountName)
        {
            var account = new GetAccountsModel();

            try
            {
                if (!String.IsNullOrEmpty(accountName))
                {
                    using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                    {
                        // account details
                        var response = csteemd.get_accounts(new ArrayList() { accountName });
                        if (response != null)
                        {
                            var accountResult = response.ToObject<List<GetAccountsModel>>();
                            account = accountResult.FirstOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return account;
        }
    }
}