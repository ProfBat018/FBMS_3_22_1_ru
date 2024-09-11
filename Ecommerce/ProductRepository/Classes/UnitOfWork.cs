﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductData.Contexts;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductRepository.Classes;

public class UnitOfWork : IUnitOfWork
{
    public IAttributeRepository AttributeRepository { get; private set; }
    public IAttributeValueRepository AttributeValueRepository { get; private set; }
    public ICategoryRepository CategoryRepository { get; private set; }
    public IOrderItemRepository OrderItemRepository { get; private set; }
    public IOrderStatusRepository OrderStatusRepository { get; private set; }
    public IOrderRepository OrderRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }
    public IWarehouseRepository WarehouseRepository { get; private set; }

    private ProductsContext _context;

    public UnitOfWork(ProductsContext context)
    {
        _context = context!;

        AttributeRepository = new AttributeRepository(_context);
        AttributeValueRepository = new AttributeValueRepository(_context);
        CategoryRepository = new CategoryRepository(_context);
        OrderItemRepository = new OrderItemRepository(_context);
        OrderStatusRepository = new OrderStatusRepository(_context);
        OrderRepository = new OrderRepository(_context);
        ProductRepository = new ProductRepository(_context);
        WarehouseRepository = new WarehouseRepository(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}