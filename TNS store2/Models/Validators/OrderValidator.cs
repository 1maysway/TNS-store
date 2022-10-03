using FluentValidation;
using TNS_store2.Models;

namespace TNS_store2.Models.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            {
                RuleFor(x => x.FullName).Length(0, 50);
                RuleFor(x => x.Address).Length(0, 100);
                RuleFor(x => x.City).Length(0, 50);
                RuleFor(x => x.State).Length(0, 10);
                RuleFor(x => x.ZipCode).Length(0, 10);
                RuleFor(x => x.Country).Length(0, 50);
                RuleFor(x => x.Phone).Length(0, 25);
                RuleFor(x => x.Email).Length(0, 50);
            }
        }
    }
}
