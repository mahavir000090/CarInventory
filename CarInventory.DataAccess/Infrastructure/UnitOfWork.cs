

using CarInventory.DataAccess.Infrastructure.Contract;
using System.Data.Entity;
using System.Data.Objects;
using System.Transactions;
using CarInventory.DataAccess;

namespace CarInventory.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        private readonly CarInventoryDbContext _db;


        public UnitOfWork()
        {
            _db = new CarInventoryDbContext();
          
        }

        public void Dispose()
        {
           
        }

        public void StartTransaction()
        {

            _transaction = new TransactionScope();

               
               

            
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }


        
    }
}
