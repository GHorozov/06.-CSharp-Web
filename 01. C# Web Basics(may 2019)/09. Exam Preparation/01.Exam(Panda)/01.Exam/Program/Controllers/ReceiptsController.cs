using Panda.App.ViewModels.Receipts;
using Panda.Models;
using Panda.Services.Contracts;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Mapping;
using SIS.MvcFramework.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.App.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptService receiptService;

        public ReceiptsController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var allReceipts = this.receiptService.All();

            var list = allReceipts
                .Select(x => new ReceiptViewModel()
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    IssuedOn = x.IssuedOn,
                    RecipientName = x.Recipient.Username
                })
                .ToList();

            return this.View(list);
        }
    }
}
