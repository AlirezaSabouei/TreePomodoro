using Microsoft.EntityFrameworkCore;
using Project.Business.Common;
using Project.Business.Common.Data;
using Project.Domain.Entities;

namespace Project.Business.Services;

public class StudentServices(IContext context) : BaseService<Student>(context)
{
    public async Task<List<Student>> GetStudentsByName(string name)
    {
        return await GetAll().Where(s => s.Name == name).ToListAsync();
    }
}
