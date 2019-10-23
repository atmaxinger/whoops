using System;

namespace whoop
{
    class PropertyTypeAttribute : Attribute
    {
        public string PropertyType { get; }

        public PropertyTypeAttribute(string propertyType)
        {
            PropertyType = propertyType;
        }
    }
}