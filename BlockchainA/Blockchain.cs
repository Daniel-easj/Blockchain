using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockchainA
{
    public class Blockchain
    {
        public IList<Block> Chain { set; get; }
        IList<Transaction> PendingTransactions = new List<Transaction>();

        private int minerReward = 1;

        public int Difficulty { get; set; }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }


        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null);
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public string PrintablePendingTransactions()
        {
            IEnumerable<string> transactionString = PendingTransactions.Select(t => t.ToString());
            return transactionString.Aggregate((val, item) => val + "," + item);
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            Console.WriteLine($"Pending transactions: {PrintablePendingTransactions()}");
            AddBlock(block);

            

            // Add new transaction as reward to miner for successfully mining a block and reset pending transactions
            IEnumerable<int> tFees = PendingTransactions.Select(f => f.TransactionFee);
            PendingTransactions = new List<Transaction>();
            CreateTransaction(new Transaction(null, minerAddress, minerReward + tFees.Aggregate((val,item) => val + item)));
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        private Block mineBlock(Block block)
        {
            string targetHash = "";
            for (int i = Difficulty; i > 0; i--)
            {
                targetHash += "0";
            }

            Console.WriteLine("Mining new block...");

            while (block.Hash.Substring(0, Difficulty) != targetHash)
            {
                block.Hash = block.CalculateHash();
                block.nonce++;
            }

            return block;
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;

            var starttime = DateTime.Now;

            block = mineBlock(block);

            Chain.Add(block);

            Console.WriteLine("Mined block: " + block.Hash + $" Attempts used with difficulty {Difficulty}: {block.nonce}");
            var endtime = DateTime.Now;
            Console.WriteLine("Time spend to mine block" + $" {endtime - starttime}");
            Console.WriteLine(IsValid());
            Console.WriteLine("-----------------------------");
        }
    }
}
