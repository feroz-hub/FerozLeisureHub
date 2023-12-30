﻿using FerozLeisureHub.Application;
using FerozLeisureHub.Application.Common.Interfaces;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure.Data;

namespace FerozLeisureHub.Insfrastructure;

public class AmenityRepository(ApplicationDbContext dbContext) : Repository<Amenity>(dbContext), IAmenityRepository
{

    //private readonly ApplicationDbContext _dbContext = dbContext;

    public void Update(Amenity amenity )
    {
        dbContext.Amenities.Update(amenity);
    }
}
