using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Reply.Domain.CreateReplyWorkflow.Reply;

namespace Profile.Domain.CreateProfileWorkflow
{
    public class RestQuestOwnerService
    {
        public string ackReceived { get; set; }
        public Task SendRestQuestOwnerAck(ValidatedReply reply)
        {
            //ensure the reply was verified 

            //invoke the send logic

            return Task.CompletedTask;
        }
    }
}
