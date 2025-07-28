using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags, int age)
        {
            if(!string.IsNullOrEmpty(email))
            {
                user.SetEmail(email);
            }
            user.SetName(name);
            user.SetType(type);
            user.SetAge(age);

            // only perform this operation if user has an annual salary value
            if (annualSalary.HasValue)
            {
                user.SetMonthlySalary(annualSalary.Value / 12);
            }
            user.SetTags(tags);
        }
    }
}