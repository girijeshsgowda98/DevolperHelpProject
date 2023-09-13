namespace Dashboard.Repositories.IRepository
{
    public interface IinsertQueryInterface
    {
        public List<string> GenerateInsertQueries(string filePath);
    }
}
