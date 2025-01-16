using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Infrastructure.EntitiesConfigrations
{
    public class CatalogEntityConfigrations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
            {
                new Category
                {
                    Id = 1,
                    Name = "Phone"
                }
                ,
                new Category {
                    Id = 2,
                    Name = "Computer"
                }
                ,
                new Category {
                    Id = 3,
                    Name ="Tablet"
                } });
        }
    }
}
