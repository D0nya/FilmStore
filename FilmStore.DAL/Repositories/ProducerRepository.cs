﻿using FilmStore.DAL.EF;
using FilmStore.DAL.Entities;
using FilmStore.DAL.Interfaces;

namespace FilmStore.DAL.Repositories
{
  class ProducerRepository : GenericRepository<Producer>
  {
    public ProducerRepository(FilmStoreContext context) : base(context)
    {
    }
  }
}
