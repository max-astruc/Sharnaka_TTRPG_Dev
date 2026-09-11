using Sharnaka_Dev.TTRPG_Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

//<sumary>
// Represents a wallet that can hold multiple currencies (Kato, Naka, Sharna) and allows for adding and removing money.
// The wallet maintains a balance in the smallest currency (Kato) and provides methods to get the balance in different formats if needed 
// A wallet can only have a positive balance, and attempts to remove more money than available will throw an exception (for now, we will not allow negative balances or debt unless planned otherwise).
// A wallet can be associated with a character, but for simplicity, we will not implement that association in this class. It can be added as part of an object such as a Character class that contains a Wallet property.
//
//  NTS : In the future, we may want to implement features such as transaction history, support for multiple wallets per character, or the ability to transfer money between wallets. 
//  For now, this class focuses on basic wallet functionality.
//  Additionally, we may consider higher value currencies and werther we should only cast them as a new amount of Sharna or add them to the enumeraiton as needed. For now, we will stick to the three defined currencies and their exchange rates.


internal class Wallet : Item
{
    int balance; // Balance of the wallet in the smallest currency (Kato)

    public Wallet() : base("Wallet")
    {
        balance = 0; // Initialize balance to zero

        //Adding a D20 roll for the amount of money in Naka

        //Adding a D100 roll for the amount of money in Kato
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

    public string GetBalanceFull()
    {
        int sharna = balance / (int)Currency.Sharna;
        int naka = (balance  % (int)Currency.Sharna) / (int)Currency.Naka;
        int kato = balance % (int)Currency.Naka;
        return $"{sharna} Sharna, {naka} Naka, {kato} Kato";
    }

    public string BalanceKatoToString()
    {
        return $"{balance} Kato";
    }

    public string BalanceNakaToString()
    {
        double nakaBalance = (double)balance / (int)Currency.Naka;
        return $"{nakaBalance} Naka";
    }

    public string BalanceSharnaToString()
    {
        double sharnaBalance = (double)balance / (int)Currency.Sharna;
        return $"{sharnaBalance} Sharna";
    }

    public int GetBalanceInKato()
    {
        return balance;
    }

    public double GetBalanceInNaka()
    {
        return (double)balance / (int)Currency.Naka;
    }

    public double GetBalanceInSharna()
    {
        return (double)balance / (int)Currency.Sharna;
    }
}


public enum Currency
{
    // Each currency has a 100:1 exchange rate with the next lower currency
    Kato = 1,             // smallest unit
    Naka = 100 * Kato,
    Sharna = 100 * Naka
}