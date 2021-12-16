using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentHub.Repositories.Extensions
{
    public static class Extensions
    {
        public static PropertyBuilder<T> IsCreateDate<T>(this PropertyBuilder<T> builder)
        {
            return builder.HasDefaultValueSql("GETUTCDATE()");
        }
        public static PropertyBuilder<T> IsReadonly<T>(this PropertyBuilder<T> property)
        {
            property.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            return property;
        }
        internal static PropertyBuilder<Guid> IsSequential(this PropertyBuilder<Guid> property)
        {
            return property.HasDefaultValueSql("NEWSEQUENTIALID()").IsReadonly();
        }

    }
}
