using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon
{

    public class SozlukConstants
    {
        public const string RAbbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";


        #region Exchange Names
        public const string FavExchangeName = "FavExchangeName";
        public const string VoteExchangeName = "VoteExchangeName";
        public const string DeleteExchangeName = "DeleteExchangeName";
        public const string UserEmailExchangeName = "UserExchange";
        #endregion

        #region Queue Names

        public const string UserEmailChangedQueueName = "UserEmailChangedQueueName";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueueName";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueueName";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueueName";
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueueName";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueueName";
        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueueName";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueueName";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueueName";


        #endregion

    }
}

