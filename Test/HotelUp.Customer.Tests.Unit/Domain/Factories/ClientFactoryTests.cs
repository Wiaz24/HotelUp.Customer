using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Unit.Domain.Repositories;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class ClientFactoryTests
{
    [Fact]
    public async Task Create_IfClientDoesNotExist_ShouldCreateClient()
    {
        // Arrange
        var clientRepository = new TestClientRepository();
        var clientFactory = new ClientFactory(clientRepository);

        // Act
        var exception = await Record.ExceptionAsync(() => clientFactory
            .Create(Guid.NewGuid()));
        
        // Assert
        exception.ShouldBeNull();
    }
    
    [Fact]
    public async Task Create_IfClientExists_ThrowsClientAlreadyExistsException()
    {
        // Arrange
        var clientRepository = new TestClientRepository();
        var clientFactory = new ClientFactory(clientRepository);
        var firstClient = await clientFactory
            .Create(Guid.NewGuid());
        clientRepository.Clients.Add(firstClient.Id, firstClient);
            
        // Act
        var exception = await Record.ExceptionAsync(() => clientFactory
            .Create(firstClient.Id));
        
        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ClientAlreadyExistsException>();
    }
}