using System;
using System.Collections.Generic;
using System.Linq;
using myservice.mvc.Application.Models.Identity;
using myservice.mvc.Application.Services;
using Moq;

namespace myservice.mvc.test.TestUtility.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static void Configure(this Mock<IIdentityService> service, TestContext testContext)
        {

            service.Setup(m => m.GetUsers(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> aliases) =>
                {
                    var emailAddresses = testContext.EmailAddreses
                        .Where(e => aliases.Contains(e.Key, StringComparer.OrdinalIgnoreCase))
                        .ToDictionary(k => k.Key, v => v.Value);

                    return new IdentityServiceUserResponse
                    {
                        EmailAddresses = emailAddresses
                    };
                });
        }

        public static void WithIdentity(this TestCompositionRoot root,
            string alias,
            string emailAddress)
        {
            root.TestContext.EmailAddreses.Add(alias,emailAddress);
        }
    }
}