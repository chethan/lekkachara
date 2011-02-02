using System.Collections.Generic;
using System.Linq;
using Service.Domain;

namespace Lekkachara.Models
{
    public class UserTranscationModel
    {
        public User User { get; private set;}

        public List<Transaction> Transactions { get; private set; }
        
        public int AverageAmount { get; private set; }

        public UserTranscationModel(User user, List<Transaction> transactions, int averageAmount)
        {
            User = user;
            Transactions = transactions;
            AverageAmount = averageAmount;
        }

        public int TotalAmount
        {
            get { return Transactions.Sum(transaction => transaction.HowMuch); }
        }

        public int AmountToBeReceived
        {
            get { 
                var temp = TotalAmount - AverageAmount;
                return temp > 0 ? temp : 0;
            }
        }

        public int AmountToBePaid
        {
            get
            {
                var temp = AverageAmount-TotalAmount;
                return temp > 0 ? temp : 0;
            }
        }
    }
}