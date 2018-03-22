using System;

namespace CMI.Contract.Parameter.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidationAttribute : Attribute
    {
        public string Regex { get; set; }
        public ValidationAttribute(string regex, string errorMessage)
        {
            Regex = regex;
        }
    }
}
