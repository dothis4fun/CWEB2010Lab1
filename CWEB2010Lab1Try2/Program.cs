using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWEB2010Lab1Try2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldContinue = false;
            Console.Title = "Grade Drivers Exam";
            WelcomeUser();
            do
            {
                GetAnswers(out Exam testExam, ref shouldContinue);
            } while (shouldContinue);
            EndProgram();
            Console.ReadKey(true);
        }
        static void WelcomeUser()
        {
            Console.WriteLine("Greetings, this program will aid in grading the scores of a driver exam");
            Console.WriteLine("Please enter the testers answer to each question");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
        }
        static void EndProgram()
        {
            Console.WriteLine("Thank you for using this program, have a nice day");
        }
        static void ColorLine(int i, bool isRight, string testAnswer)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            if (isRight)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            WriteQuestion(i);
            Console.WriteLine(testAnswer.ToUpper());
            Console.ResetColor();
        }
        static void WriteQuestion(int i)
        {
            Console.Write("Question {0,-2}: ",i+1);
        }
        static void WriteQuestion(int i, string tryAgain)
        {
            Console.WriteLine("Enter either 'A' or 'B' or 'C' or 'D'");
            WriteQuestion(i);
        }
        static string GetTestersName()
        {
            string tempAnswer;
            Console.WriteLine("Please enter the testers name");
            tempAnswer = Console.ReadLine();
            while (tempAnswer.Length < 1)
            {
                Console.WriteLine("Please enter a valid testers name");
                tempAnswer = Console.ReadLine();
            }
            return tempAnswer;
        }
        static void GetAnswers(out Exam testExam, ref bool shouldContinue)
        {
            string tempAnswer;
            bool isRight;
            testExam = new Exam();
            testExam.TestersName = GetTestersName();
            for (int i = 0; i < Exam.CORRECT_ANSWERS.Length; i++)
            {
                WriteQuestion(i);
                testExam.TestersAnswers[i] = Console.ReadLine().ToLower();
                while (testExam.TestersAnswers[i] != "a" && testExam.TestersAnswers[i] != "b" && 
                    testExam.TestersAnswers[i] != "c" && testExam.TestersAnswers[i] != "d")
                {
                    WriteQuestion(i, "tryAgain");
                    testExam.TestersAnswers[i] = Console.ReadLine().ToLower();
                }
                if (testExam.TestersAnswers[i] == Exam.CORRECT_ANSWERS[i])
                {
                    ++testExam.RightAnswers;
                    isRight = true;
                }
                else
                {
                    ++testExam.WrongAnswers;
                    isRight = false;
                    testExam.SetQuestionWrongList(i+1);
                }
                ColorLine(i, isRight, testExam.TestersAnswers[i]);
            }
            Console.WriteLine(testExam.ToString());
            Console.WriteLine("Do you want to grade another test? (Y/N)");
            tempAnswer = Console.ReadLine().ToLower();
            while (tempAnswer != "n" && tempAnswer != "y")
            {
                Console.WriteLine("Please enter either 'Y' or 'N'");
                tempAnswer = Console.ReadLine().ToLower();
            }
            if (tempAnswer == "y")
            {
                shouldContinue = true;
                Console.Clear();
            }
            else
            {
                shouldContinue = false;
                Console.Clear();
            }
        }
    }
    class Exam
    {
        public static readonly string[] CORRECT_ANSWERS = { "a", "b", "a", "c", "d", "b", "c", "d",
            "a", "d", "b", "c", "d", "a", "d", "c", "c", "b", "d", "a" };
        private string[] testersAnswers = new String[CORRECT_ANSWERS.Length];
        private string result;
        private List<int> questionWrongList = new List<int>();
        public List<int> QuestionWrongList
        {
            get { return questionWrongList; }
        }
        public void SetQuestionWrongList(int value)
        {
            questionWrongList.Add(value);
        }
        public string[] TestersAnswers
        {
            get { return testersAnswers; }
            set { testersAnswers = value; }
        }
        public int RightAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public string TestersName { get; set; }
        private void SetResult()
        {
            this.result = RightAnswers / CORRECT_ANSWERS.Length > 0.75 ? "Pass" : "Fail";
        }
        public override string ToString()
        {
            SetResult();
            return String.Format("Results for {0}: {1}, with {2} questions right and {3} questions wrong.\nThe questions answered incorrectly were {4}", char.ToUpper(TestersName[0]) + TestersName.Substring(1), result, RightAnswers, WrongAnswers, string.Join(", ", QuestionWrongList));
        }
    }
}
