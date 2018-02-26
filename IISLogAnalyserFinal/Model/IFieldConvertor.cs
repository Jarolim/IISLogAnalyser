using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISLogAnalyserFinal.Model
{
    public interface IFieldConvertor
    {
        dynamic Convert(string text);
    }
}
