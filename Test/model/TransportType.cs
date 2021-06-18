using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumGoogleMapsExample.Test.model
{
    public enum TransportType
    {
        Walking,
        Cycling,
        Bus,
        Transit
    }

    class TransportTypeConverter
    {
        public static string ToPolish(TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.Walking:
                    return "Pieszo";
                case TransportType.Cycling:
                    return "Na rowerze";
                default:
                    throw new Exception("Invalid parameter given");
            }
        }
    }
}
