namespace FinalProjectOfUnittest.Data.DAL
{
    public interface IDAL<T> where T : class 
    {
        //Create
        void Add(T entity);
        //Read
      
        T Get(Func<T, bool> firstFuction);
        ICollection<T> GetAll();
        ICollection<T> GetList(Func<T, bool> whereFuction);

        //Update
        void Update(T entity);
        //Delete
        void Delete(T entity);


        void Save();
    }
    
    
}
