using Microsoft.EntityFrameworkCore;
using ProjectTest.Model;
using ProjectTest.Model.Base;
using ProjectTest.Model.Context;
using System;

namespace ProjectTest.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected SqlContext _context;
        private DbSet<T> dataset;
        public GenericRepository(SqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataset.Where(x => x.Ativo == true).ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id) && p.Ativo == true);
        }

        public T Create(T item)
        {
            try
            {
                item.Ativo = true;

                dataset.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
                return null;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    result.Ativo = false;

                    //dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "default";

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }

                return int.Parse(result);
            }
        }
    }
}
