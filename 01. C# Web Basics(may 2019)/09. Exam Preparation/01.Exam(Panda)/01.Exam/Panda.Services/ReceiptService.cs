using Panda.Data;
using Panda.Models;
using Panda.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly PandaDbContext context;

        public ReceiptService(PandaDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Receipt> All()
        {
            return this.context
                .Receipts;
        }
    }
}
