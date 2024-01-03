using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.DAL.Data.Configrations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Id).UseIdentityColumn(1, 1);

            builder.Property(E => E.Name).
                IsRequired(true)
                .HasMaxLength(50);

            builder.Property(E => E.Salary)
                .HasColumnType("decimal(18, 2)");

        }
    }
}
