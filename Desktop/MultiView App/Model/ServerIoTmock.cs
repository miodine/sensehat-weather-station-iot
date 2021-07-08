using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace MultiViewApp.Model
{
    public class ServerIoTmock
    {
        Random rand = new Random();

        public JArray getMeasurements()
        {
            string jsonText = "[";

            jsonText += "{\"name\":\"Temperature\",\"value\":" + (23.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"C\"},";
            jsonText += "{\"name\":\"Pressure\",\"value\":" + (1023.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"hPa\"},";
            jsonText += "{\"name\":\"Humidity\",\"value\":" + (43.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"%\"},";

            jsonText += "{\"name\":\"Roll\",\"value\":" + (180.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"Deg\"},";
            jsonText += "{\"name\":\"Pitch\",\"value\":" + (0.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"Deg\"},";
            jsonText += "{\"name\":\"Yaw\",\"value\":" + (270.0 + rand.NextDouble()).ToString(CultureInfo.InvariantCulture) + ",\"unit\":\"Deg\"}";

            jsonText += "]";

            return JArray.Parse(jsonText);
        }
    }
}
