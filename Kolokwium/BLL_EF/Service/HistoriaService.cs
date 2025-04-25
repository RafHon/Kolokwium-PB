using BLL.Interface;
using DAL;
using Microsoft.Data.SqlClient;
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

        public async Task<IEnumerable<Historia>> GetPagedFromProcedureAsync(int pageNumber, int pageSize)
        {
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);

            var result = await _context.Historie
                .FromSqlRaw("EXEC GetPagedHistoria @PageNumber, @PageSize", pageNumberParam, pageSizeParam)
                .ToListAsync();

            return result;
        }
    }
}
