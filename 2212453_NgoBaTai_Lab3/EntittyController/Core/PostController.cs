using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Entity = EntityModel.Post;

namespace EntityController.Core
{
    public partial class PostController : Base
    {
        public DbSet<Entity> Execute;
        public PostController()
        {
            Execute = db.Posts;
        }
        #region Common execution
        /// <summary>
        /// Execute query string (if select always use * - select *) for this object only
        /// </summary>
        /// <param name="query">query</param>
        /// <returns>bool</r
        /// eturns>
        public List<Entity> ExecuteQuery(string query)
        {
            return Execute.SqlQuery(query).ToList();
        }
        #endregion
        #region Insert
        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>bool</returns>
        public bool Insert(Entity e)
        {
            Execute.Add(e);
            return (db.SaveChanges() > 0);
        }
        /// <summary>
        /// Insert entity list
        /// </summary>
        /// <param name="list">Entity list</param>
        /// <returns>bool</returns>
        public bool Insert(List<Entity> list)
        {
            foreach (Entity e in list)
            {
                Execute.Add(e);
            }
            try { db.SaveChanges(); }
            catch { return false; }
            return true;
        }
        #endregion
        #region Delete queries
        /// <summary>
        /// Delete by conditions, not use for Like operator
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public bool DeleteWhere(string conditions)
        {
            Execute.RemoveRange(Execute.AsQueryable().Where(conditions).ToList());
            try { db.SaveChanges(); }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Delete class object
        /// </summary>
        /// <param name="l">Class object</param>
        /// <returns>bool</returns>
        public bool Delete(Entity e)
        {
            Execute.Attach(e);
            Execute.Remove(e);
            try { db.SaveChanges(); }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Delete object list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Delete(List<Entity> list)
        {
            foreach (Entity e in list)
            {
                Execute.Attach(e);
                Execute.Remove(e);
            }
            try { db.SaveChanges(); }
            catch { return false; }
            return true;
        }
        #endregion

        #region Select queries
        /// <summary>
        /// Select by conditions, not use for Like operator
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public List<Entity> SelectWhere(string conditions)
        {
            return Execute.AsQueryable().Where(conditions).ToList();
        }
        /// <summary>
        /// Select by conditions and sort by orders, not use for Like operator
        /// </summary>
        /// <param name="conditions">conditions</param>
        /// <param name="orders">orders</param>
        /// <returns></returns>
        public List<Entity> SelectOrderWhere(string conditions, string orders)
        {
            return Execute.AsQueryable().Where(conditions).OrderBy(orders).ToList();
        }
        /// <summary>
        /// Select all records
        /// </summary>
        /// <returns>List</returns>
        public List<Entity> SelectAll()
        {
            return Execute.ToList();
        }
        /// <summary>
        /// Select all by orders (Order = "LinkID ASC, LinkURL DESC")
        /// Use Linq.Dynamic library
        /// </summary>
        /// <param name="Order">Orders string</param>
        /// <returns>List</returns>
        public List<Entity> SelectAll(string orders)
        {
            return Execute.AsQueryable().OrderBy(orders).ToList();
        }
        /// <summary>
        /// Select top
        /// </summary>
        /// <param name="number">the number of records</param>
        /// <returns>List</returns>
        public List<Entity> SelectTop(int number)
        {
            return Execute.Take(number).ToList();
        }
        /// <summary>
        /// Select top by orders
        /// </summary>
        /// <param name="number">the number of records</param>
        /// <param name="orders">orders' string - example: LinkID ASC, LinkName DESC</param>
        /// <returns>List</returns>
        public List<Entity> SelectTop(int number, string orders)
        {
            return Execute.AsQueryable().OrderBy(orders).Take(number).ToList();
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="e">Entity</param>
        /// <returns>bool</returns>
        public bool Update(Entity e)
        {
            Execute.Attach(e);
            db.Entry(e).State = EntityState.Modified;
            return (db.SaveChanges() > 0);
        }
        /// <summary>
        /// Update entity list
        /// </summary>
        /// <param name="list">Entity list</param>
        /// <returns>bool</returns>
        public bool Update(List<Entity> list)
        {
            foreach (Entity e in list)
            {
                Execute.Attach(e);
                db.Entry(e).State = EntityState.Modified;
            }
            try { db.SaveChanges(); }
            catch { return false; }
            return true;
        }
        #endregion
    }
}
