using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.UsesrSpec
{
    public class CountUserWithSpec : BaseSpecification<User>
    {
        public CountUserWithSpec(SpecParams specParams)
           : base(p => !p.IsDeleted)
        {

        }
    }
}
