using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class TimeConvertor : IFieldConvertor
    {
        public dynamic Convert(string text) => DateTimeOffset.ParseExact(text, "HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
    }
}
