using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class IntConvertor : IFieldConvertor
    {
        public dynamic Convert(string text) => System.Convert.ToInt32(text);
    }
}
