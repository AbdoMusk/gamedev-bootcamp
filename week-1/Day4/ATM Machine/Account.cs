using System;
using System.Collections.Generic;

class Account
{
    public string AccountNumber;
    public int PIN;
    public double Balance;
    public List<string> Transactions;

    public Account(string accountNumber, int pin, double balance)
    {
        AccountNumber = accountNumber;
        PIN = pin;
        Balance = balance;
        Transactions = new List<string>();
    }

    public void CheckBalance()
    {
        Console.WriteLine("Your current balance is: $" + Balance);
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance = Balance + amount;
            string transaction = "Deposit: $" + amount;
            Transactions.Add(transaction);
            Console.WriteLine("Deposit successful! New balance: $" + Balance);
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }

    public void Withdraw(double amount)
    {
        if (amount > 0)
        {
            if (amount <= Balance)
            {
                Balance = Balance - amount;
                string transaction = "Withdraw: $" + amount;
                Transactions.Add(transaction);
                Console.WriteLine("Withdrawal successful! New balance: $" + Balance);
            }
            else
            {
                Console.WriteLine("Insufficient funds. Your balance is: $" + Balance);
            }
        }
        else
        {
            Console.WriteLine("Withdrawal amount must be positive.");
        }
    }

    public void ShowTransactions()
    {
        if (Transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet.");
        }
        else
        {
            Console.WriteLine("Transaction History:");
            int i = 0;
            while (i < Transactions.Count)
            {
                Console.WriteLine(Transactions[i]);
                i = i + 1;
            }
        }
    }
}
