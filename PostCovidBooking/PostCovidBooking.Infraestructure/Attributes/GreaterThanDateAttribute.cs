using PostCovidBooking.Infraestructure.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace PostCovidBooking.Infraestructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    sealed public class GreaterThanDateAttribute : ValidationAttribute
    {
        #region Fields

        readonly int days;
        readonly string dependentProperty;

        #endregion

        #region Properties

        public int Days
        {
            get { return days; }
        }

        public string DependentProperty
        {
            get { return dependentProperty; }
        }

        #endregion

        #region Constructors

        public GreaterThanDateAttribute(string field, int days)
        {
            // assign fields
            dependentProperty = field;
            this.days = days;
        }

        #endregion

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Validate property data type
            DateTime date, dependentDate;
            DatesAtributeHelper.ExtractvalidationDates(value, validationContext, DependentProperty, out date, out dependentDate);

            // Perform validation operation
            if (date >= dependentDate.AddDays(days)) { return ValidationResult.Success; }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }

        #endregion
    }
}