using System;
using System.Collections.Generic;

namespace firstHomeWork
{
internal class Program
    {
        static void Main(string[] args)
        {
            Account Progamer228 = new Account(100);
            EMailBalanceChangedNotifyer first_msg = new EMailBalanceChangedNotifyer("adress");
            Progamer228.AddNotifyer(first_msg);
            Console.WriteLine("First test");
            Progamer228.ChangeBalance(90);
            SMSLowBalanceNotifyer second_msg = new SMSLowBalanceNotifyer("+7800553535", 0);
            Progamer228.AddNotifyer(second_msg);
            Console.WriteLine("\nsecond test");
            Progamer228.ChangeBalance(-10);
            Console.WriteLine("\nthird  test");
            Progamer228.ChangeBalance(10);
            Console.WriteLine("\ngetter test");
            decimal x = Progamer228.Balance;
            Console.WriteLine(x);
        }
}

class Account
{
       private decimal _balance;
       private List<INotifyer> _notifyers;

       public Account()
       {
            _balance = 0;
            _notifyers = new List<INotifyer>();
       }

       public Account(decimal _balance)
       {
            this._balance = _balance;
            _notifyers = new List<INotifyer>();
       }

       public void AddNotifyer(INotifyer notifyer)
       {
            _notifyers.Add(notifyer);
       }

       public void ChangeBalance(decimal value)
       {
            _balance = value;
            Notification();
       }

       public decimal Balance
       {
            get
            {
                return _balance;
            }
       }

       private void Notification()
       {
            foreach(INotifyer notifyer in _notifyers)
            {
                notifyer.Notify(_balance);
            }
       }
}

interface INotifyer
{
    void Notify(decimal balance);
}

public class SMSLowBalanceNotifyer : INotifyer
{
    private string _phone;
    private decimal _lowBalanceValue;

    public SMSLowBalanceNotifyer(string _phone, decimal _lowBalanceValue)
    {
        this._phone = _phone;
        this._lowBalanceValue = _lowBalanceValue;
    }
    public void Notify(decimal balance)
    {
        if(balance < _lowBalanceValue)
        {
            Console.WriteLine($"SMSLowBalanceNotifyer\n {balance}");
        }
    }
}

public class EMailBalanceChangedNotifyer : INotifyer
{
    private string _email;
    public EMailBalanceChangedNotifyer(string email)
    {
        _email = email;
    }

    public void Notify(decimal balance)
    {
        Console.WriteLine($"EMailBalanceChangedNotifyer\n {balance}");
    }
}  
}