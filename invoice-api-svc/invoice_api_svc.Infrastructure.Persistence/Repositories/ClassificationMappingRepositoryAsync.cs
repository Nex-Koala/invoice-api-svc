using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Domain.Entities;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Infrastructure.Persistence.Repositories
{
    public class ClassificationMappingRepositoryAsync : GenericRepositoryAsync<ClassificationMapping>, IClassificationMappingRepositoryAsync
    {
        private readonly DbSet<ClassificationMapping> _classificationMappings;

        public ClassificationMappingRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _classificationMappings = dbContext.Set<ClassificationMapping>();
        }

        public async Task<IReadOnlyList<ClassificationMapping>> GetMappingsByClassificationIdAsync(int classificationId)
        {
            return await _classificationMappings
                .Where(mapping => mapping.ClassificationId == classificationId && !mapping.IsDeleted)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ClassificationMapping>> GetMappingsByUserIdAsync(Guid userId)
        {
            return await _classificationMappings
                .Where(um => um.Classification.UserId == userId && !um.IsDeleted)
                .ToListAsync();
        }
    }
}
