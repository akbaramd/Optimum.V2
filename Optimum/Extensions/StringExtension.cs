using Newtonsoft.Json;

namespace Optimum.Extensions;

public static class StringExtension
{
    public static object ConvertToType(this string value, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return value;
        }
        else if (targetType == typeof(int))
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
        }
        else if (targetType == typeof(bool))
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                return boolValue;
            }
        }
        else if (targetType == typeof(DateTime))
        {
            if (DateTime.TryParse(value, out DateTime dateTimeValue))
            {
                return dateTimeValue;
            }
        }

        // Add more custom type conversions as needed

        return null; // or throw an exception if conversion fails
    }
    
    public static bool IsJson(this string input)
    {
        try
        {
            JsonConvert.DeserializeObject(input);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}