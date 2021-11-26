using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PostCovidBooking.Infraestructure.Helpers
{
    public static class DatesAtributeHelper
    {
        public static void ExtractvalidationDates(object value, ValidationContext validationContext,string dependentProperty, out DateTime date, out DateTime dependentDate)
        {
            Type valueType = validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType;
            if (valueType != typeof(DateTime) && valueType != typeof(DateTime?))
            {
                throw new ValidationException("No Date value found for validation.");
            }

            // Get property value
            date = ((DateTime)value).Date;
            if (dependentProperty == "Today")
                dependentDate = DateTime.Now.Date;
            else
            {
                PropertyInfo dependentPropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(dependentProperty);
                if (dependentPropertyInfo == null ||
                    dependentPropertyInfo.PropertyType != typeof(DateTime))
                {
                    throw new ArgumentException("Invalid argument for validation attribute.");
                }
                dependentDate = ((DateTime)dependentPropertyInfo.GetValue(validationContext.ObjectInstance, null)).Date;
            }
        }
    }
}
