using FluentNHibernate.Mapping;
using Service.Domain;

namespace Service.Mappings
{
    public class TransactionMap :ClassMap<Transaction> 
    {
        public TransactionMap()
        {
            Table("Transactions");
            LazyLoad();
            Id(x => x.Id, "id").GeneratedBy.Increment();
            References(x => x.Who, "userid").Not.Nullable();
            Map(x => x.What, "itemName").Not.Nullable();
            Map(x => x.When, "transactiondt").Not.Nullable();
            Map(x => x.HowMuch, "amount").Not.Nullable();
        }

    }
}