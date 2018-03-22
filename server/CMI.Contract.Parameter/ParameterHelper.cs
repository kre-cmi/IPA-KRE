using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using CMI.Contract.Parameter.Attributes;

namespace CMI.Contract.Parameter
{
    public static class ParameterHelper
    {
        public static string GetJsonStringOfType(IParameter parameter)
        {
            var paramList = new List<Parameter>();
            foreach (var propertyInfo in parameter.GetType().GetProperties())
            {
                var mandatory = false;
                string description = null;
                string validationString = null;
                string defaultValue = null;

                var attributes = propertyInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    var defaultAttribute = attribute as DefaultAttribute;
                    var mandatoryAttribute = attribute as MandatoryAttribute;
                    var validationAttribute = attribute as ValidationAttribute;
                    var descriptionAttribute = attribute as DescriptionAttribute;
                    if (mandatoryAttribute != null)
                    {
                        mandatory = true;
                    }
                    if (defaultAttribute != null)
                    {
                        defaultValue = defaultAttribute.Default;
                    }
                    if (validationAttribute != null)
                    {
                        validationString = validationAttribute.Regex;
                    }
                    if (descriptionAttribute != null)
                    {
                        description = descriptionAttribute.Description;
                    }
                }
                
                var param = new Parameter
                {
                    Default = defaultValue,
                    Description = description,
                    Mandatory = mandatory,
                    Name = propertyInfo.Name,
                    RegexValidation = validationString,
                    Type = GetType(propertyInfo.PropertyType),
                    Value = GetValue(propertyInfo, parameter)
                };
                if (param.Name != null)
                {
                    paramList.Add(param);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(paramList);

            if (jsonString == null)
            {
                throw new NullReferenceException();
            }
            return jsonString;

        }

        private static string GetValue(PropertyInfo propertyInfo, IParameter parameter)
        {
            return propertyInfo.GetValue(parameter).ToString();
        }

        private static string GetType(Type type)
        {
            if (type.Name == "bool")
            {
                return "bool";
            }
            if (type.Name == "int" || type.Name == "double" || type.Name == "float")
            {
                return "number";
            }
            return "text";
        }
    }
}
