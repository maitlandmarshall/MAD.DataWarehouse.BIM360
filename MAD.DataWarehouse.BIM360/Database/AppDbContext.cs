using MAD.DataWarehouse.BIM360.Api.Accounts;
using MAD.DataWarehouse.BIM360.Api.Data;
using MAD.DataWarehouse.BIM360.Api.Project;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Hub>(cfg =>
            {
                cfg.HasKey(y => y.Id);
            });

            modelBuilder.Entity<Project>(cfg =>
            {
                cfg.HasKey(y => y.Id);
                cfg.HasIndex(y => y.AccountId);
            });

            modelBuilder.Entity<FolderItem>(cfg =>
            {
                cfg.HasKey(y => new { y.Id, y.ProjectId});
                cfg.OwnsOne(y => y.Attributes, cfg =>
                {
                    cfg.OwnsOne(y => y.Extension, cfg =>
                    {
                        cfg.Ignore(y => y.Schema);

                        cfg.OwnsOne(y => y.Data, cfg =>
                        {
                            cfg.Property(y => y.VisibleTypes).HasConversion(
                                y => JsonConvert.SerializeObject(y),
                                y => JsonConvert.DeserializeObject<List<string>>(y));

                            cfg.Property(y => y.Actions).HasConversion(
                                y => JsonConvert.SerializeObject(y),
                                y => JsonConvert.DeserializeObject<List<string>>(y));

                            cfg.Property(y => y.AllowedTypes).HasConversion(
                                y => JsonConvert.SerializeObject(y),
                                y => JsonConvert.DeserializeObject<List<string>>(y));

                            cfg.Property(y => y.NamingStandardIds).HasConversion(
                                y => JsonConvert.SerializeObject(y),
                                y => JsonConvert.DeserializeObject<List<object>>(y));
                        });
                    });

                    cfg.Navigation(y => y.Extension).IsRequired();
                });

                cfg.Navigation(y => y.Attributes).IsRequired();

                cfg.OwnsOne(y => y.Relationships, cfg =>
                {
                    cfg.OwnsOne(y => y.Parent, cfg =>
                    {
                        cfg.OwnsOne(y => y.Data, cfg =>
                        {
                            cfg.HasIndex(y => y.Id);
                        });

                        cfg.OwnsOne(y => y.Meta, cfg =>
                        {
                            cfg.OwnsOne(y => y.Link);
                        });

                        cfg.Navigation(y => y.Meta).IsRequired();
                    });

                    cfg.OwnsOne(y => y.Tip, cfg =>
                    {
                        cfg.OwnsOne(y => y.Data, cfg =>
                        {
                            cfg.HasIndex(y => y.Id);
                        });

                        cfg.OwnsOne(y => y.Meta, cfg =>
                        {
                            cfg.OwnsOne(y => y.Link);
                        });

                        cfg.Navigation(y => y.Meta).IsRequired();
                    });

                    cfg.OwnsOne(y => y.Storage, cfg =>
                    {
                        cfg.OwnsOne(y => y.Data, cfg =>
                        {
                            cfg.HasIndex(y => y.Id);
                        });

                        cfg.OwnsOne(y => y.Meta, cfg =>
                        {
                            cfg.OwnsOne(y => y.Link);
                        });

                        cfg.Navigation(y => y.Meta).IsRequired();
                    });

                    cfg.OwnsOne(y => y.Item, cfg =>
                    {
                        cfg.OwnsOne(y => y.Data, cfg =>
                        {
                            cfg.HasIndex(y => y.Id);
                        });

                        cfg.OwnsOne(y => y.Meta, cfg =>
                        {
                            cfg.OwnsOne(y => y.Link);
                        });

                        cfg.Navigation(y => y.Meta).IsRequired();
                    });

                    cfg.Navigation(y => y.Parent).IsRequired();
                    cfg.Navigation(y => y.Tip).IsRequired();
                    cfg.Navigation(y => y.Storage).IsRequired();
                    cfg.Navigation(y => y.Item).IsRequired();
                });

                cfg.Navigation(y => y.Relationships).IsRequired();
            });
        }
    }
}
