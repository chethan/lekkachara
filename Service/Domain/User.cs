using System.ComponentModel.DataAnnotations;
using NHibernate.Type;

namespace Service.Domain
{
    public class User : Entity
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual bool Inactive{ get; set; }

    }
}