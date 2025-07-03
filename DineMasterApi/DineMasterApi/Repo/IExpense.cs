using DineMasterApi.DTO;

namespace DineMasterApi.Repo
{
    public interface IExpense
    {
        Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync();
        Task<ExpenseDto> GetExpenseByIdAsync(int id);
        Task<ExpenseDto> AddExpenseAsync(ExpenseDto dto);
        Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseDto dto);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
