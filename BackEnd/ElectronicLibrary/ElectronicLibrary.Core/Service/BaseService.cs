using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary.Core.Service
{
    public class BaseService<Entity> : IBaseService<Entity>
    {
        #region fields
        IBaseRepo<Entity> _repo;
        #endregion

        public BaseService(IBaseRepo<Entity> repo)
        {
            _repo = repo;
        }

        public int Delete(Entity entity)
        {
            return _repo.Delete(entity);
        }

        public bool CheckDuplicate(Entity entity)
        {
            return _repo.CheckDuplicateV2(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<Entity>> GetAll()
        {
            var result = _repo.GetAll();

            return result;
        }

        public async Task<List<Entity>> GetPaging(PagingParameter parameter)
        {
            var result = await _repo.Get(parameter.Limt, parameter.Skip, parameter.Sort, parameter.Filter);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Insert(Entity entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Update(Entity entity)
        {
            return _repo.Update(entity);
        }
    }
}
