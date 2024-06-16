using HrDataManager.Application.Dtos;
using MediatR;

namespace HrDataManager.Application.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public int Id { get; set; }
    }
}
