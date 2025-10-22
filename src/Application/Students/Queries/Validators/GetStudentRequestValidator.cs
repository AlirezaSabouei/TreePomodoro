using FluentValidation;

namespace Application.Students.Queries.Validators;

public class GetStudentRequestValidator : AbstractValidator<GetStudentRequest>
{
    public GetStudentRequestValidator()
    {
        RuleFor(a => a.StudentId).NotEmpty();
    }
}