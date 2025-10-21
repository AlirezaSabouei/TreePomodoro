using Microsoft.EntityFrameworkCore;
using Project.Business.Common;
using Project.Business.Common.Data;
using Project.Domain.Entities;

namespace Project.Business.Services;

public class StudentServices(Context context)
{
    public async Task<List<Student>> GetStudentsByName(string name)
    {
        return await context.Students.Where(s => s.Name == name).ToListAsync();
    }
}
