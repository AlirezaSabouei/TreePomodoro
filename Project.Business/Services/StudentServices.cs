using Microsoft.EntityFrameworkCore;
using Project.Business.Common;
using Project.Domain.Entities;

namespace Project.Business.Services;

public class StudentServices(DbContext context) : BaseService<Student>(context)
{
    public IQueryable<Student> GetStudentsByName(string name)
    {
        return GetAll().Where(s => s.Name == name);
    }
}
