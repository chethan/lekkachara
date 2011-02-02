using FluentNHibernate.Mapping;
using NHibernate.Type;
using Service.Domain;

namespace Service.Mappings
{
    public class UserMap :ClassMap<User> 
    {
        public UserMap()
        {
            Table("Users");
            LazyLoad();
            Id(x => x.Id, "id").GeneratedBy.Increment();
            Map(x => x.Name, "name").Not.Nullable();
            Map(x => x.Inactive, "inactive").Not.Nullable().CustomType(typeof(YesNoType));
        }

    }
}