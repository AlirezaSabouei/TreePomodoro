using FluentValidation;

namespace Application.Students.Commands.Validators;

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator()
    {
        RuleFor(a=>a.Student.Name).NotEmpty();
    }
}