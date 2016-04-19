using Ronzl.Framework.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Framework.DAL
{
    public interface IDataRepository
    {
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update<T>(T entity) where T : ModelBase;
        #endregion

        #region 插入        
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert<T>(T entity) where T : ModelBase;
        #endregion

        #region 删除
        /// <summary>
        ///删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : ModelBase;
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find<T>(params object[] keyValues) where T : ModelBase;
        #endregion

        List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : ModelBase;
        PagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex) where T : ModelBase;
    }
}
