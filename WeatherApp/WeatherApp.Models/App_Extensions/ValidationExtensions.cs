using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp
{
    public static class ValidationExtensions
    {
        // Regexp for recurring validation purposes
        public const string TEXT_FIELD_REGEXP = @"^[0-9a-zA-ZåäöÅÄÖéèÈÉËëáàÁÀ\-_&\.,~\^@()/%\s\!]*$";


        // If the class to be validated does not have a separate metadata class, pass
        // the same type for both typeparams.
        public static bool IsDataAnnotationValid<T, U>(this T obj, ref Dictionary<string, string> errors)
        {
            //If metadata class type has been passed in that's different from the class to be validated, register the association
            if (typeof(T) != typeof(U))
            {
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(T), typeof(U)), typeof(T));
            }

            var validationContext = new ValidationContext(obj, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (validationResults.Count > 0 && errors == null)
                errors = new Dictionary<string, string>(validationResults.Count);

            foreach (var validationResult in validationResults)
            {
                errors.Add(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            if (validationResults.Count > 0)
                return false;
            else
                return true;
        }
    }
}

