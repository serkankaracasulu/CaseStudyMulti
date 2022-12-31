﻿using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using ProductAPI.Common.DAL;
using ProductAPI.Common.Model;
using ProductAPI.Entities.ProductImages;
using Redis.OM;

namespace ProductAPI.Applicaitons.Images
{
    public class ImageRepository : BaseRepository<ProductImage, string>, IProductImageRepository
    {
        public ImageRepository(ApplicationDbContext applicationDbContext, RedisConnectionProvider provider) : base(applicationDbContext, provider)
        {
        }

        public Task<PaginatedList<CategoryDto>> List<CategoryDto>(AutoMapper.IConfigurationProvider configuration, string productId, int pageNumber, int pageSize)
        {
            return PaginatedList<CategoryDto>.CreateAsync(_db.Where(d => d.ProductId == productId).ProjectTo<CategoryDto>(configuration), pageNumber, pageSize);
        }
    }
}
