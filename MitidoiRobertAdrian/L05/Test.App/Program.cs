using LanguageExt;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult.QuestionCreated;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var cmd = new CreateQuestionCmd("Some issue with System.out.println in JAVA"
                , "I have tried lots of JDK's and JRE's from Oracle website"
                + "\nCan anyone to help me ? "
                , new string[] { "JAVA" }, "I finally did, I install JDK 8 and JRE 8 and I restart the compiler :)");

            var result = CreateQuestion(cmd);

            result.Match(
                ProcessQuestionCreated,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
            );

            Console.ReadLine();

            if (result.form == true)
            {
                Console.WriteLine("Do you want to give a vote ?");
                Console.WriteLine("Yes or No ? Y or N");
                string decision = Console.ReadLine();
                if (decision.Equals("Y"))
                {
                    Console.WriteLine("You can give a positive or negative vote: P or N ?");
                    string vote = Console.ReadLine();
                    if (vote.Equals("P"))
                        result.getVotes(1);
                    else if (vote.Equals("N"))
                        result.getVotes(-1);
                }
            }
        }

        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Feedback}");
            return questionNotCreatedResult;
        }

        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }

        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Title is missing" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Title.Length < 0 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Title cannot be shorter than 8 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Title.Length > 1000)
            {
                var errors = new List<string>() { "Title cannot be longer than 180 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Body is missing" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length < 0 && !string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Body cannot be shorter than 10 characters." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Body.Length > 1000)
            {
                var errors = new List<string>() { "Body is limited to 10000 characters; you entered 10005." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Length < 1)
            {
                var errors = new List<string>() { "Please enter at least one tag; see a list of popular tags." };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Length > 3)
            {
                var errors = new List<string>() { "You entered to much tags, you need to delete some of these" };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionCreated(questionId, createQuestionCommand.Title, "robimitidoi@gmail.com", true);

            if (result.form)
            {
                Console.WriteLine(result.ToString());
            }
            else
            {
                QuestionNotCreated feedback = new QuestionNotCreated("Question was closed, can be created !");
            }

            return result;
        }
    }
}
