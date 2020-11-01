using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Choices;

namespace Reply.Domain.CreateReplyWorkflow
{
    [AsChoice]
    public static partial class CreateReplyResult
    {
        public interface ICreateReplyResult { }

        public class ReplyCreated: ICreateReplyResult
        {
            public Guid ReplyId { get; private set; }
            public string Reply { get; private set; }
            public string OwnerQuestion { get; private set; }
            public int VoteCount { get; private set; }
            public IReadOnlyCollection<Reply.Domain.CreateReplyWorkflow.VoteEnum> AllVotes { get; private set; }

            public ReplyCreated(Guid replyId, string reply, string ownerQuestion)
            {
                ReplyId = replyId;
                Reply = reply;
                OwnerQuestion = ownerQuestion;
            }
        }

        public class ReplyNotCreated: ICreateReplyResult
        {
            public string Reason { get; set; }

            public ReplyNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class ReplyValidationFailed: ICreateReplyResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public ReplyValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}
