using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAD.DataWarehouse.BIM360.Database.Configurations
{
    internal class ReportRunCheckEntityTypeConfiguration : IEntityTypeConfiguration<ReportRunCheck>
    {
        public void Configure(EntityTypeBuilder<ReportRunCheck> builder)
        {
            builder.HasKey(y => new { y.WorkItemId, y.CheckId });
        }
    }
}
