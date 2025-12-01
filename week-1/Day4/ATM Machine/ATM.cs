using System;
using System.Collections.Generic;

class ATM
{
    public List<Account> Accounts;

    public ATM()
    {
        Accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        Accounts.Add(account);
    }

    public Account Authenticate()
    {
        Console.WriteLine("Welcome to the ATM Machine!");
        Console.Write("Please enter your account number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter your PIN: ");
        string pinInput = Console.ReadLine();
        
        int pin = 0;
        bool isPinValid = int.TryParse(pinInput, out pin);
        
        if (!isPinValid)
        {
            Console.WriteLine("Invalid PIN format.");
            return null;
        }

        // go through each account to find matching one
        int i = 0;
        while (i < Accounts.Count)
        {
            if (Accounts[i].AccountNumber == accountNumber && Accounts[i].PIN == pin)
            {
                Console.WriteLine("Authentication successful!");
                Console.WriteLine("");
                return Accounts[i];
            }
            i = i + 1;
        }

        Console.WriteLine("Invalid account number or PIN.");
        return null;
    }

    public void ShowMenu()
    {
        Console.WriteLine("ATM Menu:");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Deposit Money");
        Console.WriteLine("3. Withdraw Money");
        Console.WriteLine("4. Show Transactions");
        Console.WriteLine("5. Exit");
        Console.WriteLine("");
    }

    public bool PerformAction(Account account, string choice)
    {
        if (choice == "1")
        {
            account.CheckBalance();
            Console.WriteLine("");
            return true;
        }
        else if (choice == "2")
        {
            Console.Write("Enter deposit amount: ");
            string amountInput = Console.ReadLine();
            double amount = 0;
            bool isValid = double.TryParse(amountInput, out amount);
            
            if (isValid)
            {
                account.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            Console.WriteLine("");
            return true;
        }
        else if (choice == "3")
        {
            Console.Write("Enter withdrawal amount: ");
            string amountInput = Console.ReadLine();
            double amount = 0;
            bool isValid = double.TryParse(amountInput, out amount);
            
            if (isValid)
            {
                account.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            Console.WriteLine("");
            return true;
        }
        else if (choice == "4")
        {
            account.ShowTransactions();
            Console.WriteLine("");
            return true;
        }
        else if (choice == "5")
        {
            Console.WriteLine("Thank you for using our ATM. Here's a summary of your transactions:");
            account.ShowTransactions();
            return false;
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
            Console.WriteLine("");
            return true;
        }
    }
}
