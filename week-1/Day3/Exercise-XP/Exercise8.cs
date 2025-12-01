using System;
using System.Collections.Generic;

class Exercise8
{
    static void Main()
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        
        Dictionary<string, string> q1 = new Dictionary<string, string>();
        q1["question"] = "What is Baby Yoda's real name?";
        q1["answer"] = "Grogu";
        data.Add(q1);
        
        Dictionary<string, string> q2 = new Dictionary<string, string>();
        q2["question"] = "Where did Obi-Wan take Luke after his birth?";
        q2["answer"] = "Tatooine";
        data.Add(q2);
        
        Dictionary<string, string> q3 = new Dictionary<string, string>();
        q3["question"] = "What year did the first Star Wars movie come out?";
        q3["answer"] = "1977";
        data.Add(q3);
        
        Dictionary<string, string> q4 = new Dictionary<string, string>();
        q4["question"] = "Who built C-3PO?";
        q4["answer"] = "Anakin Skywalker";
        data.Add(q4);
        
        Dictionary<string, string> q5 = new Dictionary<string, string>();
        q5["question"] = "Anakin Skywalker grew up to be who?";
        q5["answer"] = "Darth Vader";
        data.Add(q5);
        
        Dictionary<string, string> q6 = new Dictionary<string, string>();
        q6["question"] = "What species is Chewbacca?";
        q6["answer"] = "Wookiee";
        data.Add(q6);
        
        int correct = 0;
        int wrong = 0;
        List<Dictionary<string, string>> wrongList = new List<Dictionary<string, string>>();
        
        for (int i = 0; i < data.Count; i++)
        {
            Console.WriteLine(data[i]["question"]);
            Console.Write("Answer: ");
            string userAnswer = Console.ReadLine();
            
            if (userAnswer.ToLower() == data[i]["answer"].ToLower())
            {
                Console.WriteLine("Correct!");
                correct++;
            }
            else
            {
                Console.WriteLine("Wrong! Answer: " + data[i]["answer"]);
                wrong++;
                
                Dictionary<string, string> wrongItem = new Dictionary<string, string>();
                wrongItem["question"] = data[i]["question"];
                wrongItem["your_answer"] = userAnswer;
                wrongItem["correct_answer"] = data[i]["answer"];
                wrongList.Add(wrongItem);
            }
        }
        
        Console.WriteLine("\nCorrect: " + correct);
        Console.WriteLine("Wrong: " + wrong);
        
        if (wrongList.Count > 0)
        {
            Console.WriteLine("\nWrong answers:");
            foreach (var item in wrongList)
            {
                Console.WriteLine("Q: " + item["question"]);
                Console.WriteLine("You said: " + item["your_answer"]);
                Console.WriteLine("Correct: " + item["correct_answer"]);
            }
        }
        
        if (wrong > 3)
        {
            Console.Write("Play again? (yes/no): ");
            string again = Console.ReadLine();
            if (again == "yes")
            {
                Main();
            }
        }
    }
}
