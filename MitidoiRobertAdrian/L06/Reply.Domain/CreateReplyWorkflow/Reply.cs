using CSharp.Choices;
using LanguageExt.Common;
using Reply.Domain.CreateReplyWorkflow;
using System;
using System.Threading.Tasks;

namespace Reply.Domain.CreateReplyWorkflow
{
    [AsChoice]
    public static partial class Reply
    {

        public interface IReply { }

        public class UnvalidatedReply : IReply
        {
            public string Reply { get; private set; }

            private UnvalidatedReply(string reply)
            {
                Reply = reply;
            }

            public static Result<UnvalidatedReply> Create(string reply)
            {
                if (!IsReplyValid(reply))
                {
                    return new Result<UnvalidatedReply>(new ValidationError(reply));
                } else
                {
                    return new UnvalidatedReply(reply);
                }
            }

            private static bool IsReplyValid(string reply)
            {
                //TODO: validate reply based on RegEx

                //validate 
                if (reply.Length >= 10 && reply.Length <= 500)
                {
                    return true;
                }
                return false;
            }
        }

        public class ValidatedReply: IReply
        {
            public string Reply { get; private set; }
            public bool isPublished { get; private set; }

            internal ValidatedReply(string reply)
            {
                Reply = reply;
                ReplyPublished();
                ReplyReceivedAcknowledgementSentToQuestionOwner(reply);
                ReplyPublishedAcknowledgmentSentToReplyAuthor(reply);
                
            }

            private void ReplyPublished()
            {
                isPublished = true;
            }

            private static Result<RestReplyOwnerService> ReplyPublishedAcknowledgmentSentToReplyAuthor(string reply)
            {
                RestReplyOwnerService restReplyOwnerService = new RestReplyOwnerService();
                Task task = restReplyOwnerService.SendRestReplyOwnerAck(new ValidatedReply(reply));
                if (task.IsCompleted)
                    restReplyOwnerService.ackReceived = "Acknowledgement Received";
                else
                    restReplyOwnerService.ackReceived = "Acknowledgement didn't received";
                return restReplyOwnerService;
            }

            private static  Result<RestQuestOwnerService> ReplyReceivedAcknowledgementSentToQuestionOwner(String reply)
            {
                RestQuestOwnerService restQuestOwnerService = new RestQuestOwnerService();
                Task task = restQuestOwnerService.SendRestQuestOwnerAck(new ValidatedReply(reply));
                if (task.IsCompleted)
                    restQuestOwnerService.ackReceived = "Acknowledgement Received";
                else
                    restQuestOwnerService.ackReceived = "Acknowledgement didn't received";
                return restQuestOwnerService;
            }
        }
    }
}
