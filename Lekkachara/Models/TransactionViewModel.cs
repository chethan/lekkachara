using System;
using System.Collections.Generic;
using Service.Domain;
using System.Linq;

namespace Lekkachara.Models
{
    public class TransactionModel
    {
        private readonly List<User> users;

        public TransactionModel(List<Transaction> allTransactions, List<User> users, int pageIndex)
        {
            this.users = users;
            AllTransactions = allTransactions;
            Transactions = allTransactions.Skip(pageIndex * Constants.PAGE_SIZE).Take(Constants.PAGE_SIZE).ToList();
        }

        public List<Transaction> AllTransactions { get; set; }
        
        public List<Transaction> Transactions { get; set; }

        public int TotalAmount
        {
            get { return AllTransactions.Sum(transaction => transaction.HowMuch); }
        }

        public int PerHeadAmount
        {
            get { return TotalAmount/users.Count; }
        }

        public List<UserTranscationModel> UserTransactions
        {
            get
            {
                return users.Select(user => new UserTranscationModel(user, AllTransactions.Where(transaction => transaction.Who == user).ToList(), PerHeadAmount)).OrderByDescending(model => model.TotalAmount).ToList();
            }
        }

    }
}