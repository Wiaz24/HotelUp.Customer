using System.Linq.Expressions;

using HotelUp.Customer.Domain.ValueObjects.Abstractions;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Customer.Infrastructure.EFCore.CustomExtensions;

internal static class Extensions
{
    internal static PropertyBuilder<TValueObject>? HasValueObject<TEntity, TValueObject>(
        this EntityTypeBuilder<TEntity> entityBuilder, 
        Expression<Func<TEntity, TValueObject>> propertyExpression,
        Action<ComplexPropertyBuilder<TValueObject>>? buildAction = null) 
        where TEntity : class 
        where TValueObject : class, IValueObject
    {
        var properties = typeof(TValueObject).GetProperties();
        if (properties.Length == 1)
        {
            return entityBuilder.Property(propertyExpression)
                .HasConversion(TValueObject.GetValueConverter());
        }
        if (buildAction is null)
        {
            entityBuilder.ComplexProperty(propertyExpression);
        }
        else
        {
            entityBuilder.ComplexProperty(propertyExpression, buildAction);
        }
        return null;
    }
    
    internal static PropertyBuilder<TValueObject?>? HasValueObject<TOwnerEntity, TDependentEntity, TValueObject>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedEntityBuilder,
        Expression<Func<TDependentEntity, TValueObject?>> propertyExpression,
        Action<OwnedNavigationBuilder<TDependentEntity, TValueObject>>? buildAction = null)
        where TOwnerEntity : class
        where TDependentEntity : class
        where TValueObject : class, IValueObject
    {
        var properties = typeof(TValueObject).GetProperties();
        if (properties.Length == 1)
        {
            return ownedEntityBuilder.Property(propertyExpression)
                .HasConversion(TValueObject.GetValueConverter());
        }
        if (buildAction is null)
        {
            ownedEntityBuilder.OwnsOne(propertyExpression);
        }
        else
        {
            ownedEntityBuilder.OwnsOne(propertyExpression, buildAction);
        }
        return null;
    }
}