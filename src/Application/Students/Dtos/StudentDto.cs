using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Students.Dtos;

public class StudentDto : IMapFrom<Student>
{
    public string Name { get; set; } = string.Empty;
}