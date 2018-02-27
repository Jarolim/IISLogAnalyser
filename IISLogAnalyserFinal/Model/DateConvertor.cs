using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class DateConvertor : IFieldConvertor
    {
        public dynamic Convert(string text) => DateTimeOffset.ParseExact(text, "yyyy'-'MM'-'dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal); 
    }
}
