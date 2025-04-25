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
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Studenci.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Studenci.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "@NowyStudentID",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddStudent @Imie, @Nazwisko, @GrupaID, @NowyStudentID OUTPUT",
                new SqlParameter("@Imie", student.Imie),
                new SqlParameter("@Nazwisko", student.Nazwisko),
                new SqlParameter("@GrupaID", (object?)student.GrupaID ?? DBNull.Value),
                idParam
            );

            int newId = (int)idParam.Value;
            var created = await _context.Studenci.FindAsync(newId);
            return created!;
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            if (!_context.Studenci.Any(s => s.ID == student.ID))
                return false;

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Studenci.FindAsync(id);
            if (student == null)
                return false;

            _context.Studenci.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
