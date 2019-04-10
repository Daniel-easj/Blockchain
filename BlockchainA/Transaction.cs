using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainA
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        public int TransactionFee { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount, int tFee)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            TransactionFee = tFee;
        }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
