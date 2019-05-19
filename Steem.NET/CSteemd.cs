using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace SteemAPI.CS
{
    public class CSteemd : CSteemAPI
    {
        #region Constructors
        public CSteemd(string strHostname = "127.0.0.1", EType type = EType.RPC, ushort nPort = 8090) : base(strHostname, type, nPort)
        {
        }
        #endregion

        #region public Methods

        public JObject get_config()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_dynamic_global_properties()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_chain_properties()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_current_median_history_price()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_feed_history()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_witness_schedule()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_hardfork_version()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JObject get_next_scheduled_hardfork()
        {
            return call_api(MethodBase.GetCurrentMethod().Name);
        }

        public JArray get_accounts(ArrayList arrAccounts)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(arrAccounts);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray lookup_account_names(ArrayList arrAccounts)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(arrAccounts);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray lookup_accounts(string strLowerbound, uint nLimit)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strLowerbound);
            arrParams.Add(nLimit);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JValue get_account_count()
        {
            return call_api_value(MethodBase.GetCurrentMethod().Name);
        }

        public JArray get_owner_history(string strAccount)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strAccount);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JObject get_recovery_request(string strAccount)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strAccount);
            return call_api(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JObject get_block_header(long lBlockID)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(lBlockID);
            return call_api(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JObject get_block(long lBlockID)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(lBlockID);
            return call_api(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray get_witnesses(ArrayList arrWitnesses)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(arrWitnesses);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }


        public JArray get_conversion_requests(string strAccount)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strAccount);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JObject get_witness_by_account(string strAccount)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strAccount);
            return call_api(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray get_witnesses_by_vote(string strFrom, int nLimit)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strFrom);
            arrParams.Add(nLimit);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JValue get_witness_count()
        {
            return call_api_value(MethodBase.GetCurrentMethod().Name);
        }

        // if permlink Is "" then it will return all votes for author
        public JArray get_active_votes(string author, string permlink)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(author);
            arrParams.Add(permlink);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JObject get_content(string strAuthor, string strPermlink)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(strAuthor);
            arrParams.Add(strPermlink);
            return call_api(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray get_content_replies(string parent, string parent_permlink)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(parent);
            arrParams.Add(parent_permlink);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray get_discussions_by_trending(string tag, int limit, ArrayList filterTags = null, ArrayList selectAuthors = null, ArrayList selectTags = null, bool? truncateBody = null)
        {
            var dictParams = new Dictionary<string, object>
            {
                { "tag", tag },
                { "limit", limit }
            };

            if (filterTags != null) dictParams.Add("filter_tags", filterTags);
            if (selectAuthors != null) dictParams.Add("select_authors", selectAuthors);
            if (selectTags != null) dictParams.Add("select_tags", selectTags);
            if (truncateBody != null) dictParams.Add("truncate_body", truncateBody);

            return call_api_array(MethodBase.GetCurrentMethod().Name, new ArrayList { dictParams });
        }

        public JArray get_discussions_by_active(string tag, int limit, ArrayList filterTags = null, ArrayList selectAuthors = null, ArrayList selectTags = null, bool? truncateBody = null)
        {
            var dictParams = new Dictionary<string, object>
            {
                { "tag", tag },
                { "limit", limit }
            };

            if (filterTags != null) dictParams.Add("filter_tags", filterTags);
            if (selectAuthors != null) dictParams.Add("select_authors", selectAuthors);
            if (selectTags != null) dictParams.Add("select_tags", selectTags);
            if (truncateBody != null) dictParams.Add("truncate_body", truncateBody);

            return call_api_array(MethodBase.GetCurrentMethod().Name, new ArrayList { dictParams });
        }
        
        public JArray get_discussions_by_hot(string tag, int limit, ArrayList filterTags = null, ArrayList selectAuthors = null, ArrayList selectTags = null, bool? truncateBody = null)
        {
            var dictParams = new Dictionary<string, object>
            {
                { "tag", tag },
                { "limit", limit }
            };

            if (filterTags != null) dictParams.Add("filter_tags", filterTags);
            if (selectAuthors != null) dictParams.Add("select_authors", selectAuthors);
            if (selectTags != null) dictParams.Add("select_tags", selectTags);
            if (truncateBody != null) dictParams.Add("truncate_body", truncateBody);

            return call_api_array(MethodBase.GetCurrentMethod().Name, new ArrayList { dictParams });
        }

        //vector<discussion> get_discussions_by_created( const discussion_query& query )const;
        //vector<discussion> get_discussions_by_cashout( const discussion_query& query )const;
        //vector<discussion> get_discussions_by_payout( const discussion_query& query )const;
        //vector<discussion> get_discussions_by_votes( const discussion_query& query )const;
        //vector<discussion> get_discussions_by_children( const discussion_query& query )const;

        //  return the active discussions with the highest cumulative pending payouts without respect to category, total
        //  pending payout means the pending payout of all children as well.
        public JArray get_replies_by_last_update(string start_author, string start_permlink, uint limit)
		{
			ArrayList arrParams = new ArrayList();
			arrParams.Add(start_author);
			arrParams.Add(start_permlink);
			arrParams.Add(limit);
			return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
		}

		// This method Is used to fetch all posts/comments by start_author that occur after before_date And start_permlink with up to limit being returned.
		//
		// If start_permlink Is empty then only before_date will be considered. If both are specified the eariler to the two metrics will be used. This
		// should allow easy pagination.
		public JArray get_discussions_by_author_before_date(string author, string start_permlink, DateTime before_date, uint limit)
		{
			ArrayList arrParams = new ArrayList();
			arrParams.Add(author);
			arrParams.Add(start_permlink);
			arrParams.Add(before_date);
			arrParams.Add(limit);
			return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
		}

		// Account operations have sequence numbers from 0 to N where N Is the most recent operation. This method
		// returns operations in the range [from-limit, from]
		//
		// from - the absolute sequence number, -1 means most recent, limit Is the number of operations before from.
		// limit - the maximum number of items that can be queried (0 to 1000], must be less than from
		public JToken get_account_history(string account, Int64 from , UInt32 limit)
		{
			ArrayList arrParams = new ArrayList();
			arrParams.Add(account);
			arrParams.Add(from);
			arrParams.Add(limit);
			return call_api_token(MethodBase.GetCurrentMethod().Name, arrParams);
		}

        public JArray get_followers(string account, string start = null, string type = "blog", uint limit = 1000)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(account);
            arrParams.Add(start);
            arrParams.Add(type);
            arrParams.Add(limit);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JArray get_following(string account, string start = null, string type = "blog", uint limit = 1000)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(account);
            arrParams.Add(start);
            arrParams.Add(type);
            arrParams.Add(limit);
            return call_api_array(MethodBase.GetCurrentMethod().Name, arrParams);
        }

        public JToken get_discussion(string author, string permlink, string api = "tags_api")
        {
            string method = MethodBase.GetCurrentMethod().Name;
            if(!String.IsNullOrEmpty(api))
                method = string.Format("{0}.{1}", api, method);

            ArrayList arrParams = new ArrayList();
            arrParams.Add(author);
            arrParams.Add(permlink);

            return call_api_token(method, arrParams);            
        }
        #endregion
    }
}
