using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels
{
    public class GetDiscussionModel
    {
        public int id { get; set; }
        public int post_id { get; set; }
        public string author { get; set; }
        public string permlink { get; set; }
        public string category { get; set; }
        public string parent_author { get; set; }
        public string parent_permlink { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string json_metadata { get; set; }
        public DateTime last_update { get; set; }
        public DateTime created { get; set; }
        public DateTime active { get; set; }
        public DateTime last_payout { get; set; }
        public int depth { get; set; }
        public int children { get; set; }
        public ulong net_rshares { get; set; }
        public ulong abs_rshares { get; set; }
        public ulong vote_rshares { get; set; }
        public ulong children_abs_rshares { get; set; }
        public DateTime cashout_time { get; set; }
        public DateTime max_cashout_time { get; set; }
        public int total_vote_weight { get; set; }
        public int reward_weight { get; set; }
        public string total_payout_value { get; set; }
        public string curator_payout_value { get; set; }
        public int author_rewards { get; set; }
        public int net_votes { get; set; }
        public string root_author { get; set; }
        public string root_permlink { get; set; }
        public string max_accepted_payout { get; set; }
        public int percent_steem_dollars { get; set; }
        public bool allow_replies { get; set; }
        public bool allow_votes { get; set; }
        public bool allow_curation_rewards { get; set; }
        public List<object> beneficiaries { get; set; }
        public string url { get; set; }
        public string root_title { get; set; }
        public string pending_payout_value { get; set; }
        public string total_pending_payout_value { get; set; }
        public List<object> active_votes { get; set; }
        public List<object> replies { get; set; }
        public ulong author_reputation { get; set; }
        public string promoted { get; set; }
        public int body_length { get; set; }
        public List<object> reblogged_by { get; set; }
    }

    //public class TotalPayoutValue
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}

    //public class CuratorPayoutValue
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}

    //public class MaxAcceptedPayout
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}

    //public class PendingPayoutValue
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}

    //public class TotalPendingPayoutValue
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}

    //public class Promoted
    //{
    //    public string amount { get; set; }
    //    public int precision { get; set; }
    //    public string nai { get; set; }
    //}
}