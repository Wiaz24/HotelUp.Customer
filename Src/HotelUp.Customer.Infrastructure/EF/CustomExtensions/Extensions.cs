using System.Linq.Expressions;
using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Customer.Infrastructure.EF.CustomExtensions;

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
        if (properties.Length > 1)
        {
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
        entityBuilder.Property(propertyExpression)
            .HasConversion(TValueObject.GetStringValueConverter());
        return entityBuilder.Property(propertyExpression);
    }
    
    internal static PropertyBuilder<TValueObject>? HasValueObject<TOwnerEntity, TDependentEntity, TValueObject>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedEntityBuilder,
        Expression<Func<TDependentEntity, TValueObject>> propertyExpression)
        where TOwnerEntity : class
        where TDependentEntity : class
        where TValueObject : class, IValueObject
    {
        var properties = typeof(TValueObject).GetProperties();
        if (properties.Length > 1)
        {
            ownedEntityBuilder.ToJson();
            return null;
        }
        ownedEntityBuilder.Property(propertyExpression)
            .HasConversion(TValueObject.GetStringValueConverter());
        return ownedEntityBuilder.Property(propertyExpression);
    }

}