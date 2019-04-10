using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainA
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public IList<Transaction> Transactions { get; set; }

        public int nonce { get; set; }

        
        private SHA256 sha256 = SHA256.Create();

        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = CalculateHash();
            nonce = 0;
        }

        public Block(DateTime timeStamp, string previousHash)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Hash = CalculateHash();
            nonce = 0;
        }

        public string CalculateHash()
        {
            
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}{PreviousHash}{JsonConvert.SerializeObject(Transactions)}{nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            
            return Convert.ToBase64String(outputBytes);
        }
    }
}
