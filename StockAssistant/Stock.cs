using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAssistant
{
    public enum StockType
    {
        Observed = 0,
        PartialHold,
        FullyHold,
    }

    public class Stock
    {
        #region Member
        private string m_Name;
        private string m_Number;
        private StockType m_Type;
        private float m_BuyPrice;
        private float m_SellPrice;
        private float m_ObservedPrice;
        private float m_CurrentPrice;
        private float m_ExpectBuyPrice;
        private float m_ExpectSellPrice;
        private AlgorithmType m_AlgType;
        #endregion

        #region Constructor
        public Stock()
        {
            m_Name = string.Empty;
            m_Number = string.Empty;
            m_Type = StockType.Observed;
            m_BuyPrice = 0;
            m_SellPrice = 0;
            m_ObservedPrice = 0;
            m_CurrentPrice = 0;
            m_ExpectBuyPrice = 0;
            m_ExpectSellPrice = 0;
            m_AlgType = AlgorithmType.DefinedPrice;
        }
        #endregion

        #region Property
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }
        public StockType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        public float BuyPrice
        {
            get { return m_BuyPrice; }
            set { m_BuyPrice = value; }
        }
        public float SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }
        public float CurrentPrice
        {
            get { return m_CurrentPrice; }
            set { m_CurrentPrice = value; }
        }
        public float ExpectBuyPrice
        {
            get { return m_ExpectBuyPrice; }
            set { m_ExpectBuyPrice = value; }
        }
        public float ExpectSellPrice
        {
            get { return m_ExpectSellPrice; }
            set { m_ExpectSellPrice = value; }
        }
        public AlgorithmType AlgorithmType
        {
            get { return m_AlgType; }
            set { m_AlgType = value; }
        }
        public float ObservedPrice
        {
            get { return m_ObservedPrice; }
            set { m_ObservedPrice = value; }
        }
        #endregion
    }
}
