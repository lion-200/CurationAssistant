using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels
{
    public class GetAccountsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public Active active { get; set; }
        public Posting posting { get; set; }
        public string memo_key { get; set; }
        public string json_metadata { get; set; }
        public string proxy { get; set; }
        public DateTime last_owner_update { get; set; }
        public DateTime last_account_update { get; set; }
        public DateTime created { get; set; }
        public bool mined { get; set; }
        public string recovery_account { get; set; }
        public DateTime last_account_recovery { get; set; }
        public string reset_account { get; set; }
        public int comment_count { get; set; }
        public int lifetime_vote_count { get; set; }
        public int post_count { get; set; }
        public bool can_vote { get; set; }
        public VotingManabar voting_manabar { get; set; }
        public int voting_power { get; set; }
        public string balance { get; set; }
        public string savings_balance { get; set; }
        public string sbd_balance { get; set; }
        public string sbd_seconds { get; set; }
        public DateTime sbd_seconds_last_update { get; set; }
        public DateTime sbd_last_interest_payment { get; set; }
        public string savings_sbd_balance { get; set; }
        public string savings_sbd_seconds { get; set; }
        public DateTime savings_sbd_seconds_last_update { get; set; }
        public DateTime savings_sbd_last_interest_payment { get; set; }
        public int savings_withdraw_requests { get; set; }
        public string reward_sbd_balance { get; set; }
        public string reward_steem_balance { get; set; }
        public string reward_vesting_balance { get; set; }
        public string reward_vesting_steem { get; set; }
        public string vesting_shares { get; set; }
        public string delegated_vesting_shares { get; set; }
        public string received_vesting_shares { get; set; }
        public string vesting_withdraw_rate { get; set; }
        public DateTime next_vesting_withdrawal { get; set; }
        public ulong withdrawn { get; set; }
        public ulong to_withdraw { get; set; }
        public ulong withdraw_routes { get; set; }
        public ulong curation_rewards { get; set; }
        public ulong posting_rewards { get; set; }
        public List<ulong> proxied_vsf_votes { get; set; }
        public int witnesses_voted_for { get; set; }
        public DateTime last_post { get; set; }
        public DateTime last_root_post { get; set; }
        public DateTime last_vote_time { get; set; }
        public int post_bandwidth { get; set; }
        public int pending_claimed_accounts { get; set; }
        public string vesting_balance { get; set; }
        public string reputation { get; set; }
        public List<object> transfer_history { get; set; }
        public List<object> market_history { get; set; }
        public List<object> post_history { get; set; }
        public List<object> vote_history { get; set; }
        public List<object> other_history { get; set; }
        public List<string> witness_votes { get; set; }
        public List<object> tags_usage { get; set; }
        public List<object> guest_bloggers { get; set; }
    }

    public class Owner
    {
        public int weight_threshold { get; set; }
        public List<object> account_auths { get; set; }
        public List<List<object>> key_auths { get; set; }
    }

    public class Active
    {
        public int weight_threshold { get; set; }
        public List<object> account_auths { get; set; }
        public List<List<object>> key_auths { get; set; }
    }

    public class Posting
    {
        public int weight_threshold { get; set; }
        public List<List<object>> account_auths { get; set; }
        public List<List<object>> key_auths { get; set; }
    }

    public class VotingManabar
    {
        public string current_mana { get; set; }
        public int last_update_time { get; set; }
    }
}