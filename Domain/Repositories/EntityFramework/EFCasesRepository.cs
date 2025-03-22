using MyWebApplication.Domain.Repositories.Abstract;
using MyWebApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace MyWebApplication.Domain.Repositories.EntityFramework
{
    public class EFCasesRepository : ICasesRepository  
    {
        private readonly AppDbContext _context;
        public EFCasesRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task<IEnumerable<Cases>> GetCasesAsync()
        {
            return await _context.Cases.Include(x=>x.GroupName).ToListAsync();
        }
        public async Task<Cases?> GetCaseByIdAsync(int id)
        {
            return await _context.Cases.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task SaveCaseAsync(Cases entity)
        {
            _context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCaseAsync(int id)
        {
            _context.Entry(new Cases() { Id = id}).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
