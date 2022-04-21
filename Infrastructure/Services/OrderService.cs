using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAgregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Order> CreateOrderAsync(string? buyerEmail, int deliveryMethodId, string? basketId, Adress shippingAddress)
        {
            // Get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId!);
            // Get items from the product 
            var items = new List<OrderItem>();
            foreach (var item in basket!.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //Get delivery from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            //create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            //save to db
            var result = await _unitOfWork.Complete();

            if(result <= 0) return default!;
            //delete basketId
            await _basketRepo.DeleteBasketAsync(basketId!); 
            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string? buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail!);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string? buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail!);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}