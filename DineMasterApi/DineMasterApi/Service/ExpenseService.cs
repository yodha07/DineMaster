using AutoMapper;
using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class ExpenseService : IExpense
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> AddExpenseAsync(ExpenseDto dto)
        {
            var entity = _mapper.Map<Expense>(dto);
            await _context.Expenses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExpenseDto>(entity);
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync()
        {
            var data = await _context.Expenses.ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseDto>>(data);
        }

        public async Task<ExpenseDto> GetExpenseByIdAsync(int id)
        {
            var item = await _context.Expenses.FindAsync(id);
            return _mapper.Map<ExpenseDto>(item);
        }

        public async Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto dto)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity == null) return null;

            entity.Category = dto.Category;
            entity.Amount = dto.Amount;
            entity.ExpenseDate = dto.ExpenseDate;

            await _context.SaveChangesAsync();
            return _mapper.Map<ExpenseDto>(entity);
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity == null) return false;

            _context.Expenses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

