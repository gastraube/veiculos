﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.Data.Repositories.Abstractions;
using Veiculos.Domain.Entity;
using Veiculos.Domain.Model;

namespace Veiculos.Data.Repositories
{
    public class RevisaoRepository : RepositoryBase<Revisao>, IRevisaoRepository
    {
        public RevisaoRepository(VeiculosDbContext context) : base(context)
        {
        }
    }
}