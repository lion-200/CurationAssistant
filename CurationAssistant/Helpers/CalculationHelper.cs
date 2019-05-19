using CurationAssistant.Models.SteemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Helpers
{
    /// <summary>
    /// Helper class containing calculation values
    /// </summary>
    public static class CalculationHelper
    {
        public static int GetWordCount(string input)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            return input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int GetImageCount(string input)
        {            
            List<string> imgTags = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };

            //Convert the string into an array of words  
            string[] source = input.Split(new char[] { '?', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            int imgCount = source.Count(x => imgTags.Any(y => x.Contains(y)));

            return imgCount;
        }

        public static decimal CalculateReputation(string repString)
        {
            decimal repResult = 0;
            double rep = 0;
            double.TryParse(repString, out rep);

            rep = Math.Log10(rep);
            rep -= 9;
            rep *= 9;
            rep += 25;

            if (rep > 0)
            {
                repResult = (Decimal)Math.Round(rep, 2);
            }

            return repResult;
        }

        public static decimal CalculateSteemPower(string spString, decimal steemPerMVests)
        {
            var veryLargeNumber = 1e6;//Equals to 1*10^6

            double sp = 0;
            if (!String.IsNullOrEmpty(spString))
            {
                spString = spString.Replace("VESTS", "").Trim();
                double.TryParse(spString, out sp);

                sp = sp / veryLargeNumber * (double)steemPerMVests;
            }

            return Math.Round((decimal)sp, 2);
        }

        public static decimal CalculateVotingManaPercentage(GetAccountsModel accountDetails)
        {
            var secondsAgo = DateTime.UtcNow.Subtract(accountDetails.last_vote_time).TotalSeconds;
            var vpNow = accountDetails.voting_power + (10000 * secondsAgo / 432000);
            var vp = Math.Min(Math.Round(vpNow / 100, 2), 100);
            return (decimal)vp;
            //Int32 currentTime = (Int32)(dynamicProps.time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            //decimal vestingShares = 0;
            //Decimal.TryParse(accountDetails.vesting_shares.Replace("VESTS", "").Trim(), out vestingShares);

            //decimal receivedVestingShares = 0;
            //Decimal.TryParse(accountDetails.received_vesting_shares.Replace("VESTS", "").Trim(), out receivedVestingShares);

            //decimal delegatedVestingShares = 0;
            //Decimal.TryParse(accountDetails.delegated_vesting_shares.Replace("VESTS", "").Trim(), out delegatedVestingShares);

            //var totalShares = vestingShares + receivedVestingShares - delegatedVestingShares;

            //var elapsed = currentTime - accountDetails.voting_manabar.last_update_time;
            //var maxMana = totalShares * 1000000;

            //decimal currentMana = 0;
            //Decimal.TryParse(accountDetails.voting_manabar.current_mana, out currentMana);

            //var currentManaCalculated = currentMana + elapsed * maxMana / 432000;

            //if (currentManaCalculated > maxMana)
            //{
            //    currentManaCalculated = maxMana;
            //}
            ////determine percentage of available mana(RC)
            //var currentManaPerc = currentManaCalculated * 100 / maxMana;

            //return Math.Round(currentManaPerc, 2);
        }

        public static decimal CalculateSteemPerMVests(GetDynamicGlobalPropertiesModel props)
        {
            decimal steemPerMVests = 0;

            var totalVestingSteem = Double.Parse(props.total_vesting_fund_steem.Replace("STEEM", ""));
            var totalVestingShares = Double.Parse(props.total_vesting_shares.Replace("VESTS", ""));

            steemPerMVests = (Decimal)(totalVestingSteem / (totalVestingShares / 1e6));           

            return steemPerMVests;
        }
    }
}