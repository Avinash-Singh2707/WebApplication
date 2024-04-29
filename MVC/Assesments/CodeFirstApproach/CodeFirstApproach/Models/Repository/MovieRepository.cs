using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CodeFirstApproach.Models;
using System.Data.Entity;


namespace CodeFirstApproach.Models.Repository
{
    public class MoviesRepository<T> : IMovieRepository<T> where T : class
    {
        MoviesContext db;
        DbSet<T> dbset;

        public void MovieRepository()
        {
            db = new MoviesContext();
            dbset = db.Set<T>();
        }
        public void Delete(object Id)
        {
            T getmodel = dbset.Find(Id);
            dbset.Remove(getmodel);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetById(object Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T obj)
        {
            dbset.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }

    
}