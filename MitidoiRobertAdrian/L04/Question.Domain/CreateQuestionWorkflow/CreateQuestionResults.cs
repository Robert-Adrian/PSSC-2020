using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult
        {
            void Match(Func<QuestionCreated, ICreateQuestionResult> processQuestionCreated, Func<QuestionCreated.QuestionNotCreated, ICreateQuestionResult> processQuestionNotCreated, Func<QuestionCreated.QuestionValidationFailed, ICreateQuestionResult> processInvalidQuestion);
        }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string User { get; private set; }
            public string Question { get; private set; }
            public bool form { get; set; }


            public QuestionCreated(Guid questionId, string question, string user, bool form)
            {
                QuestionId = questionId;
                Question = question;
                User = user;
                this.form = form;
            }


            public class QuestionNotCreated : ICreateQuestionResult
            {
                public string Feedback { get; set; }

                public QuestionNotCreated(string feedback)
                {
                    Feedback = feedback;
                }
            }

            public class QuestionValidationFailed : ICreateQuestionResult
            {
                public IEnumerable<string> ValidationErrors { get; private set; }

                public QuestionValidationFailed(IEnumerable<string> errors)
                {
                    ValidationErrors = errors.AsEnumerable();
                }
            }

            void ICreateQuestionResult.Match(Func<QuestionCreated, ICreateQuestionResult> processQuestionCreated, Func<QuestionNotCreated, ICreateQuestionResult> processQuestionNotCreated, Func<QuestionValidationFailed, ICreateQuestionResult> processInvalidQuestion)
            {
                throw new NotImplementedException();
            }
        }
    }
}
