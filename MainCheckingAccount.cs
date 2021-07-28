using System;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleBasedBanking
{
    class CheckingAccount
    {
        static void Main()
        {
            /* This area is where you can call your functions
             * Main() is where you can execute all of your commands
             * But this legacy type Console Based Banking application has pre-built methods
             * As a reminder, all of this information is falsified and is not real.
            */
            #region Variables & Declarations
            var CheckingAccount = new MenuCode();
            bool Pass_Main_Menu = false;
            bool NewUser = true;
            bool Are_Loans_Enabled = true;
            bool Are_BankMessages_Enabled = true;
            bool InSettingsPage = true;
            #endregion

            #region Check if the user is new or not
            while (NewUser)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[ Welcome To The Quantum Banking Application! ]");
                    Console.WriteLine("[ ------------------------------------------- ]");
                    Console.Write("[ Please enter your full name: ");
                    Console.ResetColor();
                    CheckingAccount.AccountOwner = Console.ReadLine();
                    double Account_Length_String = CheckingAccount.AccountOwner.Length;

                    // Username length checking ( Ensuring the user types in both names, or at least most of it )
                    if (Account_Length_String < 8)
                    {
                        throw new NotImplementedException(); // Loops back
                    }
                    if (Account_Length_String > 20)
                    {
                        throw new NotImplementedException(); // Loops back
                    }

                    Thread.Sleep(250);
                    Welcome_The_First_Time_User(); // Running local variables & Configs
                    NewUser = false; // Makes it so that this wont run again...
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine($"Please ensure your name was entered correctly.");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }
            #endregion

            #region Main Menu Code
            Console.Clear();
            while (Pass_Main_Menu == false)
            {
                try
                {
                    // Console Based Main Menu
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[ Current Balance : {CheckingAccount.Balance}]");
                    Console.WriteLine($"[ Account Owner : {CheckingAccount.AccountOwner}]");
                    Console.WriteLine("-------------------------------");
                    Console.ResetColor();
                    Console.WriteLine("[*] Please select an option:");
                    Console.WriteLine();
                    Console.WriteLine("[1] Deposit Money");
                    Console.WriteLine("[2] Withdraw Money");
                    Console.WriteLine("[3] Loans");
                    Console.WriteLine("[4] View Settings");
                    Console.WriteLine("[5] Exit Application");
                    Console.WriteLine();
                    Console.Write("Enter an option: ");
                    #region Option Conversion
                    string form1 = Console.ReadLine();
                    double MenuChoice = Convert.ToDouble(form1); // Legacy way of doing it..
                    #endregion

                    switch (MenuChoice)
                    {
                        case 1:
                            DepositMoneyMenu();
                            break;
                        case 2:
                            WithdrawMoneyMenu();
                            break;
                        case 3:
                            LoansMenu();
                            break;
                        case 4:
                            SettingsMenu();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine($"Please enter a valid option.");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }
            #endregion

            void Welcome_The_First_Time_User()
            {
                Console.Clear();
                Console.WriteLine($"Hello, {CheckingAccount.AccountOwner}. Lets setup your account.");
                Thread.Sleep(1);

                Console.ForegroundColor = ConsoleColor.Green; // Green for good!
                Console.WriteLine($"Setting user account level....");
                //User_Account_Level = "Default"; // (ADMIN, MODERATOR, DEFAULT)
                Thread.Sleep(1);
                Console.WriteLine($"Grabbing Global Permissions..."); // For use later

                CheckingAccount.Account_Hard_Locked = false;
                CheckingAccount.Account_Soft_Locked = false;
                CheckingAccount.EXCEEDED_DAILY_LIMIT = false;

                Thread.Sleep(1);
                Console.WriteLine($"Checking Global Connection String...");
                //CheckingAccount.UploadToDatabase = true;
                Thread.Sleep(1);
                Console.WriteLine($"Finishing up.");
                Thread.Sleep(1);
                Console.ResetColor();
            }

            void Check_The_Transaction()
            {
                Console.Clear();
                Console.WriteLine($"Processing your transaction...");
                Thread.Sleep(1500);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green; // Green for good!
                Console.WriteLine($"Checking Transaction...");
                Thread.Sleep(1500);
                Console.ResetColor();
                Console.Clear();
            }

            void SettingsMenu()
            {
                while (InSettingsPage)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[ Current Balance : {CheckingAccount.Balance} ]");
                    Console.WriteLine($"[ Account Owner : {CheckingAccount.AccountOwner} ]");
                    Console.ResetColor();
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine($"1. Enable/Disable loan screen. [Current: {Are_Loans_Enabled}]");
                    Console.WriteLine($"2. Show Contact Messages [Current: {Are_BankMessages_Enabled}]");
                    Console.WriteLine($"3. Exit Settings page.");
                    Console.WriteLine("");
                    Console.Write("Enter choice: ");
                    string SettingsPageChoice = Console.ReadLine();
                    double tt = Convert.ToInt32(SettingsPageChoice);

                    switch (tt)
                    {
                        case 1:
                            if (Are_Loans_Enabled)
                            {
                                Are_Loans_Enabled = false;
                                Console.Clear();
                                Console.WriteLine("Disabling Loans Section...");
                                Thread.Sleep(2000);
                            }
                            else if (!Are_Loans_Enabled)
                            {
                                Are_Loans_Enabled = true;
                                Console.Clear();
                                Console.WriteLine("Enabling Loans Section...");
                                Thread.Sleep(3000);
                            }
                            break;
                        case 2:
                            if (Are_BankMessages_Enabled)
                            {
                                Console.Clear();
                                Are_BankMessages_Enabled = false;
                                Console.WriteLine("Disabling Bank Contact Messages...");
                                Thread.Sleep(1500);
                            }
                            else if (!Are_BankMessages_Enabled)
                            {
                                Console.Clear();
                                Are_BankMessages_Enabled = true;
                                Console.WriteLine("Enabling Bank Contact Messages...");
                                Thread.Sleep(1500);
                            }
                            break;
                        case 3:
                            InSettingsPage = false;
                            break;
                        default:
                            Console.WriteLine("Please enter a valid option.");
                            break;
                    }
                }
            }

            void LoansMenu()
            {
                CheckingAccount.LoanMoney(1);
                Thread.Sleep(4000);
                Console.Clear();
            }

            void DepositMoneyMenu()
            {
                Console.Clear();
                Console.WriteLine($"[ Current Balance : {CheckingAccount.Balance} ]");
                Console.WriteLine($"[ Account Owner : {CheckingAccount.AccountOwner} ]");

                Console.Write($"Please enter a deposit amount: ");

                string Users_Deposit_Amount = Console.ReadLine();
                double Int_Users_Deposit_Amount = Convert.ToDouble(Users_Deposit_Amount);

                Check_The_Transaction();

                CheckingAccount.DepositMoney(Int_Users_Deposit_Amount);
            }

            void WithdrawMoneyMenu()
            {
                Console.Clear();
                Console.WriteLine($"[ Current Balance : {CheckingAccount.Balance} ]");
                Console.WriteLine($"[ Account Owner : {CheckingAccount.AccountOwner} ]");

                Console.Write($"Please enter a withdrawal amount: ");

                string Users_Withdraw_Amount = Console.ReadLine();
                double Int_Users_Withdraw_Amount = Convert.ToDouble(Users_Withdraw_Amount);

                Check_The_Transaction();

                CheckingAccount.WithdrawMoney(Int_Users_Withdraw_Amount);
            }
        }
    }
}
/*
 * -- Bank Questions ( Edited on tha fly )
 * How long did it take to develop Quantum Bank?
 * What is the purpose of Quantum Console Based Banking?
 * Who is the developer behind this Quantum Console Bank?
 * Information about custom loan amounts?
 * Whats planned for the future?
 * Will this project be maintained?
 */