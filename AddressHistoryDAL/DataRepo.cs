using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using AddressHistoryDAL.EF;
using AddressHistoryDAL.Models;

namespace AddressHistoryDAL
{
    public class DataRepo<T> : IDisposable, IRepo<T> where T : Address, new()
    {
        private readonly DbSet<T> _table;
        private readonly AddressHistoryContext _db;
        protected AddressHistoryContext Context => _db;

        public DataRepo() : this(new AddressHistoryContext()) {}
        public DataRepo(AddressHistoryContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public int Add(T addr)
        {
            _table.Add(addr);
            return SaveChanges();
        }

        public int Delete(T addr)
        {
            _db.Entry(addr).State = EntityState.Deleted;
            return SaveChanges();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public T GetAddress(DateTime date)
        {
            var address = from a in _table
                          where date >= a.StartDate && date <= a.EndDate
                          select a;
            return address.Single();
        }

        public List<T> GetAddresses() => _table.ToList();

        public List<T> GetAddresses(DateTime date)
        {
            var address = from a in _table
                          where date <= a.EndDate
                          orderby a.StartDate
                          select a;
            return address.ToList();
        }

        public int Update(T addr)
        {
            _table.Update(addr);
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                InputOutput IOManager = InputOutput.GetInstance();
                IOManager.WriteLogFile(ex);
            }
            catch (RetryLimitExceededException ex)
            {
                InputOutput IOManager = InputOutput.GetInstance();
                IOManager.WriteLogFile(ex);
            }
            catch (DbUpdateException ex)
            {
                InputOutput IOManager = InputOutput.GetInstance();
                IOManager.WriteLogFile(ex);
            }
            catch (Exception ex)
            {
                InputOutput IOManager = InputOutput.GetInstance();
                IOManager.WriteLogFile(ex);
            }
            return -100;
        }
    }
}
