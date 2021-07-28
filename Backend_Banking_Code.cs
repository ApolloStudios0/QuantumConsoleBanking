using System;
using System.Threading;

namespace ConsoleBasedBanking
{
    public class Backend_Banking_Code
    {
        #region Declarations & Configuration
        // bool AllowCustomSortCodes = false;
        // bool AllowCustomAccountNumber = false;
        // bool DashesInSortCode = false; // Allows for ease of copy-paste
        // bool FlagOnExceedAllowance = false; // Flag an alert if a user exceeds the daily limit set. (Default $10,000 in one transaction)
        // bool UploadToDatabase = false; // Link the inputs to a database for re-execution remembrance
        private double ACCOUNT_NUMBER = 87654321; // Will be randomized later
        private double ACCOUNT_SORT_CODE = 120034; // Will be randomized later
        public string AccountOwner = "";

        public string PUBLIC_ACCOUNT_NUMBER { get { return Convert.ToString(ACCOUNT_NUMBER); } } // Public Account Number
        public string PUBLIC_SORT_CODE { get { return Convert.ToString(ACCOUNT_SORT_CODE); } } // Public Sort Code

        private double _Balance = 1000; // Real Balance Figure
        public double Balance { get { return _Balance; } } // Public Balance Display

        private double MAX_ALLOWED_LIMIT = 10000; // Default $10,000
        public bool EXCEEDED_DAILY_LIMIT = false; // Checks if the user has deposited over $10,000 in one transaction.
        public bool HAS_EXISTING_LOAN;

        public bool Account_Soft_Locked = true; // Soft-lock account used for flagging (Stops spending of large deposits)
        public bool Account_Hard_Locked = false; // Useful for locking the account entirely. (Suspicious transactions?)

        public string Bank_Contact_Information_Message = ("Please contact the bank @ 0123-234-5678 for more information");
        public string Emergency_Bank_Contact_Message = ("A serious error has occurred. Please contact the bank immediately @ 0123-234-5678.");
        #endregion

        public void DepositMoney(double amount)
        {
            // Calls : Fraud Checks, Loan Checks, Max Deposit Amounts (Check Config For Options)
            // Check deposit amount not over $10,000 (flag or alert, check config)
            if (amount <= 10000 && EXCEEDED_DAILY_LIMIT == false && amount != 0 && Account_Hard_Locked == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                _Balance += amount;
                Console.WriteLine($"You deposited {amount} successfully. Current Balance : {_Balance}");
                //Console.WriteLine($"{AccountOwner}, Your new balance is {amount}.");
                //Console.WriteLine($"{amount} Deposited Successfully.");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount == MAX_ALLOWED_LIMIT) // Equals
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // *** This puts the transaction through but ghosts (LOCKS) the money so it cant be spent or withdrawn. (Account Locked If Suspicious?)
                Console.WriteLine($"You deposited {amount} successfully. Current Balance : {_Balance}");
                Account_Soft_Locked = true; // Soft-Locks the account.
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount >= MAX_ALLOWED_LIMIT) // Greater Than
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"${amount} could not be processed. Please deposit ${MAX_ALLOWED_LIMIT} or less.");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (EXCEEDED_DAILY_LIMIT == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Daily deposit amount exceeded. {Bank_Contact_Information_Message}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You cannot deposit $0. Please increase the amount.");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (_Balance == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You cannot withdraw more than your balance. Please edit the amount.");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Emergency_Bank_Contact_Message);
                Thread.Sleep(10000);
                Console.ResetColor();
            }
        } // Deposit Money Method (With Multiple Checks)

        public void WithdrawMoney(double amount)
        {
            // Calls : Fraud Checks, Balance Levels, Current Loans, Max Withdraw (Check config for options)

            // Cant withdraw is balance 0
            // Cant withdraw is account is soft / hard locked
            // make sure not pulling more than 10,000 then stop transaction and fail it

            if (_Balance != 0 && amount > 0 && amount < 10000 && Account_Hard_Locked == false && Account_Soft_Locked == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                _Balance -= amount;
                Console.WriteLine($"You withdrew {amount}. Your current balance is : {_Balance}");
                //Console.WriteLine($"{AccountOwner}, you withdrew {amount}. Your current balance is : {_Balance}");
                //Console.WriteLine($"Withdraw Successful. Current Balance : {_Balance}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount >= _Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You cannot withdraw more than your available balance. {Bank_Contact_Information_Message}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (Account_Soft_Locked == true || Account_Hard_Locked == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There was an unknown error while processing your withdrawal. {Bank_Contact_Information_Message}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You cannot withdraw $0. {Bank_Contact_Information_Message}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if (amount >= MAX_ALLOWED_LIMIT) // Greater Than
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{AccountOwner}, you cannot withdraw more than {MAX_ALLOWED_LIMIT}. {Bank_Contact_Information_Message}");
                Thread.Sleep(2000);
                Console.ResetColor();
            }
            else if(Balance < 0)
            {
                Console.WriteLine(Emergency_Bank_Contact_Message);
                Thread.Sleep(2000);
            }
        } // Withdraw Money Method (With Multiple Checks)

        public void LoanMoney(double amount)
        {
            double LOAN_INTEREST_AMOUNT = 9.40; // Default 9.4% APR ( Industry Standard )
            double TotalLoanAmount = 0;
            double NEW_LOAN_AMOUNT_TO_PAY = (TotalLoanAmount * LOAN_INTEREST_AMOUNT);

            if (HAS_EXISTING_LOAN == false && Account_Hard_Locked == false && Account_Soft_Locked == false)
            {
                Console.Clear();
                Console.WriteLine($"[ Current Balance : {Balance}]");
                Console.WriteLine($"[ Account Owner : {AccountOwner}]");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("[*] Please select a loan amount:");
                Console.WriteLine();
                Console.WriteLine("[1] $100");
                Console.WriteLine("[2] $250");
                Console.WriteLine("[3] $500");
                Console.WriteLine("[4] $1000");
                Console.WriteLine("[5] Back");
                Console.WriteLine();
                Console.Write($"Please enter a choice: ");

                string Loan_Menu_Choice = Console.ReadLine();
                //double Int_Loan_Menu_Choice = Convert.ToDouble(Loan_Menu_Choice);

                if (Loan_Menu_Choice == "1")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You took a loan of $100. With {LOAN_INTEREST_AMOUNT}% interest, you must payback {NEW_LOAN_AMOUNT_TO_PAY}");
                    Console.WriteLine($"");
                    _Balance += 100; // $100
                    Console.ResetColor();
                }

                else if (Loan_Menu_Choice == "2")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You took a loan of $250. With {LOAN_INTEREST_AMOUNT}% interest, you must payback {NEW_LOAN_AMOUNT_TO_PAY}");
                    Console.WriteLine($"");
                    _Balance += 250; // $250
                    Console.ResetColor();
                }

                else if (Loan_Menu_Choice == "3")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You took a loan of $500. With {LOAN_INTEREST_AMOUNT}% interest, you must payback {NEW_LOAN_AMOUNT_TO_PAY}");
                    Console.WriteLine($"");
                    _Balance += 500; // $500
                    Console.ResetColor();
                }

                else if (Loan_Menu_Choice == "4")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You took a loan of $1000. With {LOAN_INTEREST_AMOUNT}% interest, you must payback {NEW_LOAN_AMOUNT_TO_PAY}");
                    Console.WriteLine($"");
                    _Balance += 1000; // $1000
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Thank you for browsing our loans, {AccountOwner}");
                    Thread.Sleep(2000);
                    Console.WriteLine("You can find our offers anytime under the 'Loans' section of the app");
                    Thread.Sleep(2000);
                    Console.WriteLine("Returning you to the main screen...");
                }
            }
        } // Loan money (Anything under $10,000 with configurable interest)

        public void Overdraft(double amount) {} // Arrange an overdraft with Quantum Bank (NOT IMPLEMENTED)

        public void BankQuestions() {} // Answer any general questions the user might have (NOT IMPLEMENTED)
    }
}
