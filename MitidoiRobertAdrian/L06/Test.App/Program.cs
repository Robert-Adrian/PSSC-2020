using LanguageExt;
using LanguageExt.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Reply.Domain.CreateReplyWorkflow;
using static Reply.Domain.CreateReplyWorkflow.Reply;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var replyResult = UnvalidatedReply.Create("You can restart your compiler, in C# in some moments can didn't see the updates");


            replyResult.Match(
                    Succ: reply =>
                    {
                        SendValidityConfirmation(reply);

                        Console.WriteLine("Reply is valid. Reply posted");
                        return Unit.Default;
                    },
                    Fail: ex =>
                    {
                        Console.WriteLine($"Invalid reply. Reason: {ex.Message}");
                        return Unit.Default;
                    }
                );
            

            Console.ReadLine();
        }

        private static void SendValidityConfirmation(UnvalidatedReply reply)
        {
            var verifiedReply = new ValidateReplyService().VerifyReply(reply);
            verifiedReply.Match(
                    verifiedReply =>
                    {
                        new RestReplyOwnerService().SendRestReplyOwnerAck(verifiedReply).Wait();
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Reply could not be verified");
                        return Unit.Default;
                    }
                );
        }
    }
}
