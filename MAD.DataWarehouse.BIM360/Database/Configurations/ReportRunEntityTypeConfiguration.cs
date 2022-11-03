using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Database.Configurations
{
    internal class ReportRunEntityTypeConfiguration : IEntityTypeConfiguration<ReportRun>
    {
        public void Configure(EntityTypeBuilder<ReportRun> builder)
        {
            builder.HasKey(y => y.WorkItemId);

            builder.Property(y => y.ProjectId).IsRequired();
            builder.Property(y => y.FolderItemId).IsRequired();

            builder.Property(y => y.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(y => y.Stats).HasConversion(
                y => JsonConvert.SerializeObject(y),
                y => JsonConvert.DeserializeObject<Dictionary<string, object>>(y));
        }
    }
}
