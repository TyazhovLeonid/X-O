using MyWebApplication.Domain.Repositories.Abstract;

namespace MyWebApplication.Domain
{
    public class DataManager
    {
        public ICasesRepository Cases {  get; set; }
        public DataManager(ICasesRepository casesRepository) 
        {
            Cases = casesRepository;
        }
    }
}
