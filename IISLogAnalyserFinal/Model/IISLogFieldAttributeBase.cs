using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public abstract class IISLogFieldAttributeBase : Attribute
    {
        public readonly string FieldName;
        public readonly IFieldConvertor Convertor;

        protected IISLogFieldAttributeBase(string name)
        {
            FieldName = name;
        }

        protected IISLogFieldAttributeBase(string name, IFieldConvertor convertor)
        {
            FieldName = name;
            Convertor = convertor;
        }
    }

    public class IISLogStringAttribute : IISLogFieldAttributeBase
    {
        public IISLogStringAttribute(string name) : base(name, new StringConvertor())
        {
        }
    }

    public class IISLogTimeAttribute : IISLogFieldAttributeBase
    {
        public IISLogTimeAttribute(string name) : base(name, new TimeConvertor())
        {
        }
    }

    public class IISLogDateAttribute : IISLogFieldAttributeBase
    {
        public IISLogDateAttribute(string name) : base(name, new DateConvertor())
        {
        }
    }

    public class IISLogIntAttribute : IISLogFieldAttributeBase
    {
        public IISLogIntAttribute(string name) : base(name, new IntConvertor())
        {
        }
    }

}
