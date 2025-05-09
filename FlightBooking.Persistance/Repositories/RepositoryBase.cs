// <copyright file="RepositoryBase.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Domain.Entities.Base;
using FlightBooking.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Persistance.Repositories;

/// <summary>
/// Base repository class for data access.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class RepositoryBase<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
    /// </summary>
    /// <param name="dbContext">The dbContext instance.</param>
    public RepositoryBase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>()
            .Where(e => !e.IsDeleted)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>()
            .Where(e => !e.IsDeleted)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(true);

        if (entity is null)
        {
            throw new InvalidOperationException("Entity cannot be null");
        }

        return entity.Id;
    }

    /// <inheritdoc/>
    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    /// <inheritdoc/>
    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.IsDeleted = true;
        entity.ModifiedOn = DateTime.UtcNow;
        entity.ModifiedBy = "SYSTEM";

        _dbContext.Set<T>().Update(entity);
    }
}