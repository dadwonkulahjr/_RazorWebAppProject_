using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorWebAppProject.Models;

namespace RazorWebAppProject.Repository.Data
{
    public class ExtendEntityTypeConfigurationForDb : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("tblEmployee");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .IsRequired();
            builder.Property(e => e.LastName)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .IsRequired(); ;
            builder.Property(e => e.Email)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .IsRequired(); ;

            builder.Property(e => e.Image)
                   .HasColumnType("varchar(255)")
                   .HasColumnName("Photo")
                   .HasMaxLength(255);
                   

            builder.Property(e => e.Gender)
                   .HasColumnType("int")
                   .HasColumnName("Sex")
                   .IsRequired();

            builder.Property(e => e.Dept)
                   .HasColumnType("int")
                   .HasColumnName("Department")
                   .IsRequired();
        }
    }
}
