using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class ClientFactoryTests
{
    [Fact]
    public async Task Create_IfClientDoesNotExist_ShouldCreateClient()
    {
        // Arrange
        var clientRepository = new InMemoryClientRepository();
        var clientFactory = new ClientFactory(clientRepository);
        var newClientId = Guid.NewGuid();

        // Act
        var client = await clientFactory.Create(newClientId);
        
        // Assert
        client.ShouldNotBeNull();
        client.Id.ShouldBe(newClientId);
    }
    
    [Fact]
    public async Task Create_IfClientExists_ThrowsClientAlreadyExistsException()
    {
        // Arrange
        var clientRepository = new InMemoryClientRepository();
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