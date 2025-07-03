using AutoMapper;
using DineMasterApi.Data;
using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class TableService : ITableRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public TableService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddTableAsync(TableDTO1 table)
        {
            var data = mapper.Map<Table>(table);
            data.CreatedAt = DateTime.Now;

            await db.Tables.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task<List<TableDTO2>> GetAllAsync()
        {
            var data = await db.Tables.ToListAsync();
            var table = mapper.Map<List<TableDTO2>>(data);
            return table;
        }

        public async Task<TableDTO3> GetTableByIdAsync(int id)
        {
            var data = await db.Tables.FindAsync(id);
            return mapper.Map<TableDTO3>(data);
        }

        public async Task UpdateTableAsync(TableDTO3 table)
        {
            var existing = await db.Tables.FirstOrDefaultAsync(x => x.TableId == table.TableId);
            if (existing != null)
            {
                existing.Name = table.Name;
                existing.Capacity = table.Capacity;
                existing.Status = table.Status;
                existing.ModifiedAt = DateTime.Now;
                existing.ModifiedBy = table.ModifiedBy;

                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteTableAsync(int id)
        {
            var table = await db.Tables.Include(x => x.Reservations).FirstOrDefaultAsync(x => x.TableId == id);
            if (table.Reservations.Count > 0)
            {
                return 0;
            }

            db.Tables.Remove(table);
            await db.SaveChangesAsync();
            return 1;
        }
    }
}
