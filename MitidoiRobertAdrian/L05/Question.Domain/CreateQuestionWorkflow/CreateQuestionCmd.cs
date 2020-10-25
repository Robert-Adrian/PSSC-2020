using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public struct CreateQuestionCmd
    {

        [Required(ErrorMessage = "Title is missing")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Title is not valid.")]
        [MinLength(0), MaxLength(1000)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body is missing")]
        [MinLength(0), MaxLength(1000)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        [MinLength(1), MaxLength(3)]
        public string[] Tags { get; set; }

        public string AnswerToYourQuest { get; set; }

        public CreateQuestionCmd(string title, string body, string[] tags, string AnswerToYourQuest)
        {
            this.Title = title;
            this.Body = body;
            this.Tags = tags;
            this.AnswerToYourQuest = AnswerToYourQuest;
        }
    }
}
