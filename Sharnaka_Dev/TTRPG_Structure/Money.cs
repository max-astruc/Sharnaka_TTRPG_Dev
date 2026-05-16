using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;



   

    int amount = 0; // Amount in the smallest currency (Kato) 

public class Wallet
{
    int balance // Balance of the wallet in the smallest currency (Kato)

    public Wallet()
    {
        balance = 0; // Initialize balance to zero
    }
    public void AddMoney(int katoAmount)
    {
        if (katoAmount < 0)
            throw new ArgumentException("Amount added cannot be negative.");
        balance += katoAmount;
    }
    public void RemoveMoney(int katoAmount)
    {
        if (katoAmount < 0)
            throw new ArgumentException("Amount cannot be negative.");
        if (katoAmount > balance)
            throw new InvalidOperationException("Not enough funds, debt cannot be added.");
        balance -= katoAmount;
    }
    public string GetBalance()
    {
        int sharna = balance / (int)Currency.Sharna;
        int naka = (balance  % (int)Currency.Sharna) / (int)Currency.Naka;
        int kato = amount % (int)Currency.Naka;
        return $"{sharna} Sharna, {naka} Naka, {kato} Kato";
    }
}

//List<Currency> Wallet(int Kato_Amount, int Naka_Amount, int Sharna_Amount)
//{

//    var wallet = new List<Currency>(Kato_Amount + Naka_Amount + Sharna_Amount); // Pre-allocate list capacity

//    if (Kato_Amount > 100) //: If the amount of Kato equals to 100, convert it to 1 Naka (next higher currecny) and add it to the wallet 
//    {
//        Naka_Amount++;
//    }
//    if (Sharna_Amount > 100)
//    {
//        Sharna_Amount++;
//    }

//    return wallet;
//}



private enum Currency
{
    // Each currency has a 100:1 exchange rate with the next lower currency
    Kato = 1,             // smallest unit
    Naka = 100 * Kato,
    Sharna = 100 * Naka
}