using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reply.Domain.CreateReplyWorkflow
{
    public struct CreateReplyCmd
    {
        [Required]
        public string FirstName { get; private set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; private set; }
        [Required]
        [Reply]
        public string Reply { get; private set; }

        public CreateReplyCmd(string firstName, string middleName, string lastName, string reply)
        {
            FirstName = firstName;
            LastName = lastName;
            Reply = reply;
            MiddleName = middleName;
        }
    }
}
