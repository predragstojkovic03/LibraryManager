using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Server.Dtos;

namespace Server.Controllers
{
  public class CustomerController
  {
    private readonly CrudService<Customer> _customerService;

    public CustomerController(CrudService<Customer> customerService)
    {
      _customerService = customerService;
    }

    public async Task<List<Customer>> GetCustomerListAsync()
    {
      return await Task.Run(() => _customerService.GetAll());
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
    {
      return await Task.Run(() => _customerService.GetById(id));
    }

    public async Task<Customer> CreateCustomerAsync(CustomerDto customerDto)
    {
      var customer = new Customer
      {
        FirstName = customerDto.FirstName,
        LastName = customerDto.LastName,
        Email = customerDto.Email,
        Phone = customerDto.Phone,
        Library = new Library { Id = customerDto.LibraryId }
      };
      return await Task.Run(() => _customerService.Create(customer));
    }

    public async Task<Customer> UpdateCustomerAsync(CustomerDto customerDto)
    {
      var customer = new Customer
      {
        Id = customerDto.Id,
        FirstName = customerDto.FirstName,
        LastName = customerDto.LastName,
        Email = customerDto.Email,
        Phone = customerDto.Phone,
        Library = new Library { Id = customerDto.LibraryId }
      };
      return await Task.Run(() => _customerService.Update(customer));
    }
  }
}