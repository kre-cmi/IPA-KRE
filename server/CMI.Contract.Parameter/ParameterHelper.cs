using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMI.Contract.Parameter.Attributes;

namespace CMI.Contract.Parameter
{
    public static class ParameterHelper
    {
        public static Parameter[] GetParameterListFromParameter(IParameter parameter)
        {
            var paramList = new List<Parameter>();
            var nameSufix = parameter.GetType().Namespace;
            foreach (var fieldInfo in parameter.GetType().GetFields())
            {
                var param = CreateParameter(fieldInfo, parameter, nameSufix);
                if (param.Name != null)
                {
                    paramList.Add(param);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            return paramList.ToArray();
        }

        public static bool SaveParameter(IParameter parameter, Parameter[] parameters = null)
        {
            var path = GetParameterPath(parameter);
            var jsonString = string.Empty;

            if (parameters == null)
            {
                jsonString = GetJsonStringOfParameter(parameter);
            }
            if (parameters != null)
            {
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
            }
            try
            {
                System.IO.File.WriteAllText(path, jsonString);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static IParameter GetParameters(IParameter parameter)
        {
            var path = GetParameterPath(parameter);
            if (!System.IO.File.Exists(path))
            {
                InitialSaveParameter(parameter);   
            }
            var jsonString = System.IO.File.ReadAllText(path);
            var paramList = Newtonsoft.Json.JsonConvert.DeserializeObject<Parameter[]>(jsonString);
            var nameSufix = parameter.GetType().Namespace;

            foreach (var fieldInfo in parameter.GetType().GetFields())
            {
                var value = paramList.First(p => p.Name == nameSufix + "." + fieldInfo.Name)?.Value;
                if (value != null)
                {
                    fieldInfo.SetValue(parameter, Convert.ChangeType(value, fieldInfo.FieldType));
                }
            }

            return parameter;
        }

        private static void InitialSaveParameter(IParameter parameter)
        {
            var path = GetParameterPath(parameter);
            var jsonString = GetJsonStringOfParameter(parameter);
            System.IO.File.WriteAllText(path, jsonString);
        }

        private static string GetJsonStringOfParameter(IParameter parameter)
        {
            var paramList = GetParameterListFromParameter(parameter);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(paramList);

            if (jsonString == null)
            {
                throw new NullReferenceException();
            }
            return jsonString;
        }

        private static string GetParameterPath(IParameter p)
        {
            var fullPath = p.GetType().Assembly.CodeBase;
            var path = fullPath.Replace(fullPath.Split('/').Last(), "parameters.json");
            var uri = new UriBuilder(path);
            return Uri.UnescapeDataString(uri.Path);
        }

        private static Parameter CreateParameter(FieldInfo fieldInfo, IParameter parameter, string sufix)
        {
            var param = new Parameter
            {
                Name = sufix + "." + fieldInfo.Name,
                Value = fieldInfo.GetValue(parameter)?.ToString(),
                Type = GetType(fieldInfo.FieldType)
            };

            if (param.Name == null || param.Type == null)
            {
                return null;
            }

            var attributes = fieldInfo.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                var mandatoryAttribute = attribute as MandatoryAttribute;
                var defaultAttribute = attribute as DefaultAttribute;
                var validationAttribute = attribute as ValidationAttribute;
                var descriptionAttribute = attribute as DescriptionAttribute;
                if (mandatoryAttribute != null)
                {
                    param.Mandatory = true;
                }
                if (defaultAttribute != null)
                {
                    param.Default = defaultAttribute.Default;
                }
                if (validationAttribute != null)
                {
                    param.RegexValidation = validationAttribute.Regex;
                }
                if (descriptionAttribute != null)
                {
                    param.Description = descriptionAttribute.Description;
                }
            }
            return param;
        }

        private static string GetType(Type type)
        {
            if (type.Name == "Boolean")
            {
                return "checkbox";
            }
            if (type.Name == "int32" || type.Name == "Double" || type.Name == "Float" || type.Name == "int64" || type.Name == "Long")
            {
                return "number";
            }
            return "text";
        }
    }
}
