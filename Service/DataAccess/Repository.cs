using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Service.Domain;

namespace Service.DataAccess
{
    public class Repository 
    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public Repository()
        {
            session = ConfigurationFactory.SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            if (session.IsOpen)
            {
                session.Close();
            }
        }

        public virtual void Delete<T>(int entityId) where T : Entity
        {
            var entity = Load<T>(entityId);
            session.Delete(entity);
            session.Flush();
        }

        public virtual IList ExecuteQuery(string sqlQuery)
        {
            IList data = session.CreateSQLQuery(sqlQuery).List();
            return data;
        }

        public virtual int ExecuteUpdate(string sqlQuery)
        {
            int rowsAffected = session.CreateSQLQuery(sqlQuery).ExecuteUpdate();
            return rowsAffected;
        }

        public virtual T Update<T>(T entity) where T : Entity
        {
            if (!Exists<T>(entity.Id)) throw new Exception("Entity Doesnot exist");
            object savedCopy = session.SaveOrUpdateCopy(entity);
            session.Flush();
            return (T)savedCopy;
        }

        public virtual T Get<T>(int entityId) where T : Entity
        {
            var entity = session.Get<T>(entityId);
            if (entity == null)
            {
                throw new Exception("Entity doesnot exist");
            }
            return entity;
        }

        public virtual T Load<T>(int entityId) where T :Entity
        {
            if (!Exists<T>(entityId)) throw new Exception("Entity doesnot exist");
            return session.Load<T>(entityId);
        }

        public virtual IList<T> List<T>() where T : Entity
        {
            return session.QueryOver<T>().List();
        }


        private bool Exists(Type entityType, object entityId)
        {
            var savedEntityCount = session.CreateCriteria(entityType).Add(Restrictions.Eq("Id", entityId)).SetProjection(
                Projections.Count("Id")).UniqueResult<Int32>();
            return savedEntityCount > 0;
        }

        public bool Exists<T>(object entityId) where T : Entity
        {
            return Exists(typeof(T), entityId);
        }

        public void Flush()
        {
            session.Flush();
        }

        public virtual T Save<T>(T entity) where T : Entity
        {
            var saveObject = (T)session.SaveOrUpdateCopy(entity);
            session.Flush();
            return saveObject;
        }

        public int Count<T>() where T:Entity
        {
            return session.QueryOver<T>().RowCount();
        }

    }
}