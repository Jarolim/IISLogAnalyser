using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public class StringConvertor : IFieldConvertor
    {
        public dynamic Convert(string text) => text;
    }
}
