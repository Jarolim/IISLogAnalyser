using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class IISLogPropertiesFieldMap
    {
        public class IISLogPropertiesFieldMapInfo
        {
            readonly public IFieldConvertor Convertor;
            readonly public FieldInfo FieldInfo;

            public IISLogPropertiesFieldMapInfo(IFieldConvertor convertorType, FieldInfo fieldInfo)
            {
                Convertor = convertorType;
                FieldInfo = fieldInfo;
            }
        }

        readonly Dictionary<int, IISLogPropertiesFieldMapInfo> FieldDictionary = new Dictionary<int, IISLogPropertiesFieldMapInfo>(16);
        
        public bool ContainsKey(int key)
        {
            return FieldDictionary.ContainsKey(key);
        }

        public IISLogPropertiesFieldMapInfo this[int key]
        {
            get
            {
                return FieldDictionary[key];
            }
        }

        public void Add(int fieldIndex, IFieldConvertor convertorType, FieldInfo fieldInfo)
        {
            FieldDictionary.Add(fieldIndex, new IISLogPropertiesFieldMapInfo(convertorType, fieldInfo));
        }
    }
}
