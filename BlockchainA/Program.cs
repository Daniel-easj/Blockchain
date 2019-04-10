using Newtonsoft.Json;
using System;

namespace BlockchainA
{
    public class Program
    {
        static void Main(string[] args)
        {
           Blockchain SuperCoin = new Blockchain();
            SuperCoin.Difficulty = 3;
            SuperCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 10, 1));
            SuperCoin.ProcessPendingTransactions("Bill");
            SuperCoin.ProcessPendingTransactions("Bill");
            SuperCoin.ProcessPendingTransactions("Bill");


            Console.ReadKey();
        }
    }
}
