using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Entities;
using RentalPoint.Data.Enums;
using RentalPoint.Data.Models;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Order
{
    public class CreateOrderHandler : IRequestHandler<CreateCommand<Data.Entities.CreateOrder>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateOrderHandler(RentalPointContext context)
        {
            _context = context;
        }
        
        public async Task<Guid> Handle(CreateCommand<CreateOrder> request, CancellationToken cancellationToken)
        {
            var inventoryList = new List<CartItem>();
            var inventorySum = 0.0m;
            foreach (var inventory in request.Entity.Inventory)
            {
                var item = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == inventory.Id && x.Count >= inventory.Count, cancellationToken);
                if (item == null) 
                    continue;
                
                inventoryList.Add(new CartItem
                {
                    Inventory = item,
                    Count = inventory.Count
                });
                inventorySum += item.Price * inventory.Count;
            }

            var promo = await _context.Promo.FirstOrDefaultAsync(x => x.Id == request.Entity.PromoId && x.Count > 0 && x.BestBefore >= DateTime.Now, cancellationToken);
            if (promo != null)
            {
                inventorySum -= inventorySum / 100 * promo.Discount;
                promo.Count -= 1;
                _context.Promo.Update(promo);
            }

            var orderId = Guid.NewGuid();
            var order = new Data.Entities.Order
            {
                Id = orderId,
                UserId = request.Entity.UserId,
                Date = DateTime.Now,
                Status = OrderStatus.Created,
                Price = inventorySum
            };

            var inventoryOrder = new List<InventoryOrder>();
            foreach (var item in inventoryList)
            {
                var count = item.Count;
                while (count > 0)
                {
                    inventoryOrder.Add(new InventoryOrder
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = item.Inventory.Id,
                        OrderId = orderId
                    });

                    item.Inventory.Count -= 1;
                    count--;
                    _context.Inventory.Update(item.Inventory);
                }
            }
            
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.InventoryOrder.AddRangeAsync(inventoryOrder, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}