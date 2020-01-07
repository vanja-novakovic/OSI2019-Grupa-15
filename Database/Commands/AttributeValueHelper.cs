using System;

namespace Database.Commands
{
    /// <summary>
    /// Helps in translating C# values to SQL values. Expression in command looks without this class like:
    /// "INSERT INTO EVENT (name) value (Bijelo dugme)" instead of acceptable : 
    /// "INSERT INTO EVENT (name) value ("Bijelo dugme")"
    /// </summary>
    public static class AttributeValueHelper
    {
        /// <summary>
        ///  Depending on type of property it puts Quotation around its value and returns string,
        ///  so it's not : 1997-12-12 12:12:00, but it is "1997-12-12 12:12:00"
        /// </summary>
        /// <param name="entityAttribute">PropertyInfo object that is being translated</param>
        /// <returns>Attribute value as string to put in command text</returns>
        public static string IfStringDoQuotaiton(object entityAttribute)
        {
            if (entityAttribute == null)
            {
                return "null";
            }
            else if (entityAttribute.GetType() == typeof(string))
                return "\"" + entityAttribute + "\"";
            else if (entityAttribute.GetType() == typeof(DateTime))
            {
                // return "Date(" + "\"" + ((DateTime)entityAttribute).ToString("yyyy-MM-dd HH:mm:ss") + "\"" + ")";
                return "\"" + ((DateTime)entityAttribute).ToString("yyyy-MM-dd HH:mm:ss") + "\"";

            }
            else
                return entityAttribute.ToString();
        }
    }
}
