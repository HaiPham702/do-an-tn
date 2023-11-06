using ElectronicLibrary.Core.Parameters;

namespace ElectronicLibrary.Core.Interface.Service
{
    public interface IBaseService<Entity>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi trong db
        /// </summary>
        /// <returns></returns>
        public Task<List<Entity>> GetAll();

        /// <summary>
        /// Lấy danh sách tất cả bản ghi trong db
        /// </summary>
        /// <returns></returns>
        public Task<List<Entity>> GetPaging(PagingParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Insert(Entity entity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Update(Entity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(Entity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CheckDuplicate(Entity entity);
    }
}
