namespace Enrollment.Api.Infrastructure.Security
{
    public static class Claims
    {
        public static class Permissions
        {
            public const string Type = "http://cttit.ru/ws/claims/permissions";

            public static class Trustee
            {
                public const string Create = nameof(Trustee) + "." + nameof(Create);

                public const string ReadAny = nameof(Trustee) + "." + nameof(ReadAny);
                public const string ReadOwned = nameof(Trustee) + "." + nameof(ReadOwned);

                public const string UpdateAny = nameof(Trustee) + "." + nameof(UpdateAny);
                public const string UpdateOwned = nameof(Trustee) + "." + nameof(UpdateOwned);

                public const string DeleteAny = nameof(Trustee) + "." + nameof(DeleteAny);
                public const string DeleteOwned = nameof(Trustee) + "." + nameof(DeleteOwned);
            }
        }
    }
}