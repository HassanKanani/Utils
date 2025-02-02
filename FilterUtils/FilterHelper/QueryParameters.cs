

namespace Utils.FilterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Utils.Models;

// در صورت استفاده از Newtonsoft.Json:
// using Newtonsoft.Json;

public class QueryParameters<T>
{
    public List<FilterCriteria> Filters { get; set; } = new List<FilterCriteria>();
    // [JsonIgnore]
    public Expression<Func<T, bool>> FilterExpression { get; private set; }
    public string OrderBy { get; set; }
    public bool OrderDescending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public void ApplyFilters()
    {
        if (Filters == null || !Filters.Any())
        {
            FilterExpression = x => true;
            return;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression body = Expression.Constant(true);

        foreach (var filter in Filters)
        {
            Expression property = GetPropertyExpression(parameter, filter.PropertyName);
            object convertedValue = Convert.ChangeType(filter.Value, property.Type);
            Expression valueExpression = Expression.Constant(convertedValue);

            Expression comparison = filter.Operator switch
            {
                FilterOperator.Equals => Expression.Equal(property, valueExpression),
                FilterOperator.NotEquals => Expression.NotEqual(property, valueExpression),
                FilterOperator.GreaterThan => Expression.GreaterThan(property, valueExpression),
                FilterOperator.GreaterOrEqual => Expression.GreaterThanOrEqual(property, valueExpression),
                FilterOperator.LessThan => Expression.LessThan(property, valueExpression),
                FilterOperator.LessOrEqual => Expression.LessThanOrEqual(property, valueExpression),
                FilterOperator.Contains => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), valueExpression),
                _ => throw new ArgumentException("Operator نامعتبر است")
            };

            body = Expression.AndAlso(body, comparison);
        }

        FilterExpression = Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression GetPropertyExpression(Expression parameter, string propertyName)
    {
        string[] parts = propertyName.Split('.');
        Expression property = parameter;
        foreach (var part in parts)
        {
            property = Expression.Property(property, part);
        }
        return property;
    }
}

