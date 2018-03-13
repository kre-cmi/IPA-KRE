using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace CMI.Contract.Parameter
{
    public class Parameter
    {
        public List<string> GetParameter()
        {
            var settings = new List<string>();
            return this.GetType().GetMembers().Select(m => m.Name).ToList();
        }
    }
}