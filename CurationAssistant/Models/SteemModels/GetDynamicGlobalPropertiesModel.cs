using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels
{
    public class GetDynamicGlobalPropertiesModel
    {
        public int head_block_number { get; set; }
        public string head_block_id { get; set; }
        public DateTime time { get; set; }
        public string current_witness { get; set; }
        public string total_pow { get; set; }
        public int num_pow_witnesses { get; set; }
        public string virtual_supply { get; set; }
        public string current_supply { get; set; }
        public string confidential_supply { get; set; }
        public string current_sbd_supply { get; set; }
        public string confidential_sbd_supply { get; set; }
        public string total_vesting_fund_steem { get; set; }
        public string total_vesting_shares { get; set; }
        public string total_reward_fund_steem { get; set; }
        public string total_reward_shares2 { get; set; }
        public string pending_rewarded_vesting_shares { get; set; }
        public string pending_rewarded_vesting_steem { get; set; }
        public int sbd_interest_rate { get; set; }
        public int sbd_print_rate { get; set; }
        public int maximum_block_size { get; set; }
        public int current_aslot { get; set; }
        public string recent_slots_filled { get; set; }
        public int participation_count { get; set; }
        public int last_irreversible_block_num { get; set; }
        public int vote_power_reserve_rate { get; set; }
    }
}