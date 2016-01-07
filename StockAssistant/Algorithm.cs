using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAssistant
{
    public enum AlgorithmType
    {
        DefinedPrice,
        FiveDayCost,
    }
    public abstract class Algorithm
    {
        public abstract bool IsMatch();
    }
}
