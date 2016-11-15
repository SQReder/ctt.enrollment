using Microsoft.AspNetCore.Authorization;

namespace Enrollment.Api.Infrastructure.Security
{
    public static class Policies
    {
        public static class Trustee
        {
            public static class ManageOwned
            {
                public const string Name = "Trustee.Manage.Owned";

                public static void Configure(AuthorizationOptions options)
                {
                    options.AddPolicy(Name, builder =>
                    {
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.Create);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.ReadOwned);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.UpdateOwned);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.DeleteOwned);
                    });
                }
            }

            public static class ManageAny
            {
                public const string Name = "Trustee.Manage.Any";

                public static void Configure(AuthorizationOptions options)
                {
                    options.AddPolicy(Name, builder =>
                    {
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.UpdateOwned);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.UpdateOwned);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.UpdateOwned);
                        builder.RequireClaim(Claims.Permissions.Type,
                            Claims.Permissions.Trustee.UpdateOwned);
                    });
                }
            }
        }
    }
}