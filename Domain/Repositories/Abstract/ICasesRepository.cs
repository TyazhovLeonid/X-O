using MyWebApplication.Domain.Entities;
namespace MyWebApplication.Domain.Repositories.Abstract

{
    public interface ICasesRepository
    {

        Task<IEnumerable<Cases>> GetCasesAsync();
        Task<Cases?> GetCaseByIdAsync(int id);
        Task SaveCaseAsync(Cases entity);
        Task DeleteCaseAsync(int id);
    }
}
