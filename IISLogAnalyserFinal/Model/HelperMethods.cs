using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class HelperMethods
    {
        public IISLogPropertiesFieldMap Parse(string line)
        {
            var fieldMap = new IISLogPropertiesFieldMap();
            var w3cFields = typeof(IISLogProperties).GetFields();
            var lineFields = line.Split(' ');
            var lineFieldsIndex = 0;

            foreach (var lineField in lineFields)
            {
                GetFieldAttributeByName(lineField, w3cFields, (fieldAttribute, fieldInfo) =>
                {
                    fieldMap.Add(lineFieldsIndex, fieldAttribute.Convertor, fieldInfo);
                });
                lineFieldsIndex += 1;
            }
            return fieldMap;
        }

        public IISLogProperties Parse(string line, IISLogPropertiesFieldMap fieldMap)
        {
            var returnValue = new IISLogProperties();
            var fieldValueIndex = 0;

            foreach (var fieldValue in line.Split(' '))
            {
                if (fieldMap.ContainsKey(fieldValueIndex))
                {
                    var fieldInfo = fieldMap[fieldValueIndex];
                    fieldInfo.FieldInfo.SetValue(returnValue, fieldInfo.Convertor.Convert(fieldValue));
                }
                fieldValueIndex += 1;
            }

            return returnValue;
        }

        void GetFieldAttributeByName(string name, FieldInfo[] w3cFields, Action<IISLogFieldAttributeBase, FieldInfo> foundBlock)
        {
            for (var fieldIndexPosition = 0; fieldIndexPosition < w3cFields.Length; fieldIndexPosition++)
            {
                var w3cField = w3cFields[fieldIndexPosition];
                var fieldAttribute = (IISLogFieldAttributeBase)w3cField.GetCustomAttributes(typeof(IISLogFieldAttributeBase), false)[0];
                if (fieldAttribute != null && fieldAttribute.FieldName == name)
                {
                    foundBlock(fieldAttribute, w3cField);
                    break;
                }
            }
        }

       
    }
}
