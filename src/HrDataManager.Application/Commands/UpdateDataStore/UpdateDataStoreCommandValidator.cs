using FluentValidation;

namespace HrDataManager.Application.Commands.UpdateDataStore
{
    public class UpdateDataStoreCommandValidator : AbstractValidator<UpdateDataStoreCommand>
    {
        public UpdateDataStoreCommandValidator()
        {
            RuleForEach(x => x.ParsedCompanyEmployeeData).ChildRules(record =>
            {
                record.RuleFor(r => r.CompanyId).NotEmpty().WithMessage("Company ID is required.");
                record.RuleFor(r => r.CompanyCode).NotEmpty().WithMessage("Company code is required.");
                record.RuleFor(r => r.CompanyDescription).NotEmpty().WithMessage("Company description is required.");
                record.RuleFor(r => r.EmployeeNumber).NotEmpty().WithMessage("Employee number is required.");
                record.RuleFor(r => r.EmployeeFirstName).NotEmpty().WithMessage("Employee first name is required.");
                record.RuleFor(r => r.EmployeeLastName).NotEmpty().WithMessage("Employee last name is required.");
                record.RuleFor(r => r.EmployeeEmail).NotEmpty().EmailAddress().WithMessage("Valid employee email is required.");
                record.RuleFor(r => r.EmployeeDepartment).NotEmpty().WithMessage("Employee department is required.");
            });
        }
    }
}
