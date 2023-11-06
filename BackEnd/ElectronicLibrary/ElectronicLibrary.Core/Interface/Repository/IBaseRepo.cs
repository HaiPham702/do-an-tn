namespace ElectronicLibrary.Core.Interface.Repository
{
    public interface IBaseRepo<Entity>
    {
        /// <summary>
        /// Lấy danh sách bản ghi có phân trang
        /// </summary>
        /// <returns></returns>
        public Task<List<Entity>> Get(int limt, int skip, string sort, string filter);

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns></returns>
        public Task<List<Entity>> GetAll();

        /// <summary>
        /// insert bản ghi vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Insert(Entity entity);

        /// <summary>
        /// Update bản ghi vào db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Update(Entity entity);

        /// <summary>
        /// Kiểm tra tồn tại 
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public bool CheckDuplicateV2(Entity entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(Entity entity);
    }
}
