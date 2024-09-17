namespace ApiPontuacao.Repository
{
    public interface IRepository<T,t>
    {
        IEnumerable<T> GetAll();
        T Get(t id);

        T Save(T entity);
        T Update(T entity);
        void Delete(t entity);

    }
}
