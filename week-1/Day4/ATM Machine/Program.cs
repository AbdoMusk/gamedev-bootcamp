using System;

class Program
{
    static void Main()
    {
        // create the ATM
        ATM atm = new ATM();

        // create some test accounts
        Account account1 = new Account("12345", 6789, 1000);
        Account account2 = new Account("54321", 1234, 2500);
        Account account3 = new Account("99999", 5555, 500);

        // add accounts to ATM
        atm.AddAccount(account1);
        atm.AddAccount(account2);
        atm.AddAccount(account3);

        // try to login
        Account currentAccount = atm.Authenticate();

        if (currentAccount != null)
        {
            // keep showing menu until user exits
            bool keepGoing = true;
            while (keepGoing)
            {
                atm.ShowMenu();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                keepGoing = atm.PerformAction(currentAccount, choice);
            }
        }
        else
        {
            Console.WriteLine("Authentication failed. Exiting ATM.");
        }

        Console.WriteLine("");
        Console.WriteLine("Goodbye!");
    }
}
