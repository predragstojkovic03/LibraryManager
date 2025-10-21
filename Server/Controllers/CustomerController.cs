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

    public List<Customer> GetCustomerList()
    {
      return _customerService.GetAll();
    }

    public Customer GetCustomerById(Guid id)
    {
      return _customerService.GetById(id);
    }

    public Customer CreateCustomer(CustomerDto customerDto)
    {
      var customer = new Customer
      {
        FirstName = customerDto.FirstName,
        LastName = customerDto.LastName,
        Email = customerDto.Email,
        Phone = customerDto.Phone,
        Library = new Library { Id = customerDto.LibraryId }
      };
      return _customerService.Create(customer);
    }

    public Customer UpdateCustomer(CustomerDto customerDto)
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
      return _customerService.Update(customer);
    }
  }
}