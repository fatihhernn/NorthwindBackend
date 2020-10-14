using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context=new TContext())
            {
                var addedEntity=context.Entry(entity);//yukarıda parametre olarak gönterilen entity, context e abone et
                addedEntity.State = EntityState.Added; //ne yapmak istediğimizi burada söylüyoruz..
                context.SaveChanges();  //veritabanına eklemek istediğimiz söylüyoruz
            }
        }

        public void Delete(TEntity entity)
        {
            //using(){} bloğu disposable patterndır ?nedir? nesnenin hayatını sonlandırmasını, garbage collectore bırakmıyor. context pahalı birşeydir.. oyüzden garbage bırakmıyoruz.. using bloğu bitince hafızadan siliniyor
            using (var context = new TContext()) 
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted; 
                context.SaveChanges(); 
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context=new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);//tekk bir nesne olacağı için singleordefault
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter=null)
        {
            //getlistte durumu farklı, filtre gönderilmemiş ise tamamını getirmek istiyoruz, filtre gönderilmişse filtreye göre
            using (var context=new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified; 
                context.SaveChanges();  
            }
        }
    }
}
