using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarInventory.DataAccess.Infrastructure;
using CarInventory.DataAccess.Infrastructure.Contract;
namespace CarInventory.DataAccess
{
    public class CarRepository: BaseRepository<Car>
    {
        
        public CarRepository(IUnitOfWork unit):base(unit)
        {

        }

    }
}
