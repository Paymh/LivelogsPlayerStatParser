using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LivelogsPlayerStatParser
{
    public class Statistics
    {
        public String IdName, FriendlyName;
        public dynamic Value;
        public DataTypes ValueType;
        public enum DataTypes
        {
            INT,DOUBLE,FLOAT,STRING
        }

        public Statistics(String friendlyName, String idName, DataTypes valueType)
        {
            this.IdName = idName;
            this.FriendlyName = friendlyName;
            this.ValueType = valueType;
        }

        public void ChangeValue(dynamic value, bool add)
        {
            dynamic convertedValue = null;
            if (this.ValueType == DataTypes.DOUBLE)
                convertedValue = Math.Round(Convert.ToDouble(value),2);
            else if (this.ValueType == DataTypes.FLOAT)
                convertedValue = Math.Round(Convert.ToSingle(value),2);
            else if (this.ValueType == DataTypes.INT)
                convertedValue = Convert.ToInt32(value);
            else if (this.ValueType == DataTypes.STRING)
                convertedValue = value.ToString();

            if (add && this.ValueType != DataTypes.STRING)                
            {
                if (this.Value == null)
                    this.Value = 0;
                this.Value += convertedValue;
                return;
            }
            else if (this.ValueType != DataTypes.STRING || !add)
            {
                this.Value = convertedValue;
            }
        }
    }
}
