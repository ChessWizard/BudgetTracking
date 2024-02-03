﻿using BudgetTracking.Common.Entity.Interfaces;
using BudgetTracking.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Context
{
    public class BudgetTrackingDbContext : IdentityDbContext<User, Role, Guid>
    {
        public BudgetTrackingDbContext(DbContextOptions<BudgetTrackingDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        #region SaveChanges Interceptor

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EnsureEntityType();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void EnsureEntityType()
        {
            var trackingEntities = ChangeTracker.Entries();

            foreach (var entity in trackingEntities)
            {
                if (entity.State is EntityState.Added && entity.Entity is IAuditEntity addedEntity)
                    addedEntity.CreatedOn = DateTimeOffset.UtcNow;

                if (entity.State is EntityState.Modified && entity.Entity is IAuditEntity modifiedEntity)
                    modifiedEntity.ModifiedOn = DateTimeOffset.UtcNow;
            }
        }

        #endregion
    }
}
