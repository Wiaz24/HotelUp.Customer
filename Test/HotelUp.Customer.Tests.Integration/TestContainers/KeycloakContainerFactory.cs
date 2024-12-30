using DotNet.Testcontainers.Builders;
using Testcontainers.Keycloak;

namespace HotelUp.Customer.Tests.Integration.TestContainers;

internal static class KeycloakContainerFactory
{
    internal static KeycloakContainer Create()
    {
        return new KeycloakBuilder()
            .WithImage("quay.io/keycloak/keycloak:latest")
            .WithUsername("admin")
            .WithPassword("admin")
            .WithEnvironment("KC_SPI_THEME_FEATURE_SCRIPTS", "enabled")
            .WithPortBinding(8080, 8080)
            .WithResourceMapping("./Import/import.json", "/opt/keycloak/data/import")
            .WithCommand("--import-realm")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(8443))
            .Build();
    }
}