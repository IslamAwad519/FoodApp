using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.UsesrSpec
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(SpecParams spec)
        {
            if (!string.IsNullOrEmpty(spec.Search))
            {
                Criteria = u => u.UserName.ToLower().Contains(spec.Search.ToLower());
            }

            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort.ToLower())
                {
                    case "Name":
                        AddOrderBy(u => u.UserName);
                        break;
                    default:
                        AddOrderBy(p => p.DateCreated);
                        break;
                }
            }

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
    }
}
