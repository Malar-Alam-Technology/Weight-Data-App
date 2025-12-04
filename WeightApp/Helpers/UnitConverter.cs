using System.Text;
using WeightApp.Constants;

namespace WeightApp.Helpers
{
    /// <summary>
    /// Helper class for unit conversion operations
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Convert weight from one unit to another
        /// </summary>
        public static double Convert(double weight, string fromUnit, string toUnit)
        {
            // First convert to kg (base unit)
            double weightInKg = fromUnit.ToLower() switch
            {
                AppConstants.UnitKilogram => weight,
                AppConstants.UnitGram => weight / 1000,
                AppConstants.UnitTon => weight * 1000,
                _ => weight
            };

            // Then convert from kg to target unit
            return toUnit.ToLower() switch
            {
                AppConstants.UnitKilogram => weightInKg,
                AppConstants.UnitGram => weightInKg * 1000,
                AppConstants.UnitTon => weightInKg / 1000,
                _ => weightInKg
            };
        }

        /// <summary>
        /// Get conversion text showing weight in other units
        /// </summary>
        public static string GetConversionText(double weight, string currentUnit)
        {
            StringBuilder sb = new StringBuilder();

            switch (currentUnit.ToLower())
            {
                case AppConstants.UnitKilogram:
                    sb.Append($"{weight * 1000:F2} g | {weight / 1000:F4} ton");
                    break;
                case AppConstants.UnitGram:
                    sb.Append($"{weight / 1000:F2} kg | {weight / 1000000:F6} ton");
                    break;
                case AppConstants.UnitTon:
                    sb.Append($"{weight * 1000:F2} kg | {weight * 1000000:F2} g");
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Format weight with unit
        /// </summary>
        public static string FormatWeight(double weight, string unit, int decimalPlaces = 2)
        {
            return $"{weight.ToString($"F{decimalPlaces}")} {unit}";
        }

        /// <summary>
        /// Validate if a unit is supported
        /// </summary>
        public static bool IsValidUnit(string unit)
        {
            return AppConstants.MeasurementUnits.Contains(unit.ToLower());
        }

        /// <summary>
        /// Convert weight to kilograms (base unit for API)
        /// </summary>
        public static double ConvertToKilograms(double weight, string fromUnit)
        {
            return Convert(weight, fromUnit, AppConstants.UnitKilogram);
        }
    }
}
