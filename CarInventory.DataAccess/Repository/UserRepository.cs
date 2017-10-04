using CarInventory.DataAccess.Infrastructure;
using CarInventory.DataAccess.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInventory.DataAccess
{
   public class UserRepository : BaseRepository<User>
    {
     
        public UserRepository(IUnitOfWork unit)
            : base(unit)
        {
          
        }

    }
}
