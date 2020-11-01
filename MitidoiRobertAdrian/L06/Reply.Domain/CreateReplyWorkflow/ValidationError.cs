using System;

namespace Profile.Domain.CreateReplyWorkflow
{
    [Serializable]
    public class ValidationError : Exception
    {
        public ValidationError()
        {
        }

        public ValidationError(string reply) : base($"The value \"{reply}\" is an invalid reply format.")
        {
        }

    }
}
