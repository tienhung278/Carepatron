﻿using api.Data;
using api.Repositories.Contracts;

namespace api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}