using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vn.gate.sdk.http.fields
{
    public class ScratchCardFields
    {
        /**
     * Provider list is allow by GATE <br><br>
     * VIETTEL, MOBIFONE, VINAPHONE, VCOIN (VTC), GATE, ZING (VINA GAME),
     * GARENA, ONCASH
     */
        public static String PROVIDER = "provider";

        /**
         *
         */
        public static String USERNAME = "username";

        /**
         *
         */
        public static String REQUEST_ID = "request_id";

        /**
         *
         */
        public static String SERIAL = "serial";

        /**
         *
         */
        public static String PIN = "pin";

        /**
         * Includes 2 components (service_code + product_code) <br>
         * 1. Service code (service_code, granted by the gate of each partner
         * allocated a code)<br>
         * 2. Product code (product_code, depending on the business product that the
         * partner defines respectively, is a sequence of 4 characters from 0000 to
         * 9999).<br>
         * <br>
         * For example: service_code = 1001008 and product_code = 0001 inference
         * code telco_service_code = 10010080001
         */
        public static String TELCO_SERVICE_CODE = "telco_service_code";

        public static List<String> getEnumFields()
        {
            List<String> fields = new List<string>();
            fields.Add(PROVIDER);
            fields.Add(USERNAME);
            fields.Add(REQUEST_ID);
            fields.Add(SERIAL);
            fields.Add(PIN);
            fields.Add(TELCO_SERVICE_CODE);
            return fields;
        }
    }
}
