using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.DAL.Data.Configrations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIs

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Code).IsRequired(true);
            builder.Property(D => D.Name).IsRequired(true);

            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(D => D.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
