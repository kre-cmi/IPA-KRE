using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace CMI.Contract.Parameter
{
    public abstract class AbstractParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class SimpleParameter : AbstractParameter
    {
        public string Value { get; set; }
        public string Default { get; set; }
        public string RegexValidation { get; set; }
        public bool? Mandatory { get; set; }
    }

    public class ComplecParameter : AbstractParameter
    {
        public List<SimpleParameter> Parameters { get; set; } = new List<SimpleParameter>();
    }
}