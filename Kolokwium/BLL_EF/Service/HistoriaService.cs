using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF.Service
{
    public class HistoriaService : IHistoriaService
    {
        private readonly AppDbContext _context;

        public HistoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Historia>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Historie
                .OrderByDescending(h => h.Data)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
