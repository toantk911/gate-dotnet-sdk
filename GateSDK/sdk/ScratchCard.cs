using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vn.gate.sdk.exception;
using vn.gate.sdk.http;
using vn.gate.sdk.http.fields;

namespace vn.gate.sdk
{
    public class PayCard
    {
        protected SDKCoreKit sdkCoreKit;

    /**
     *
     * @return SDKCoreKit
     */
    public SDKCoreKit getSdkCoreKit() {
        return sdkCoreKit;
    }

    /**
     *
     * @param sdkCoreKit
     * @throws InvalidArgumentException
     */
    public void setSdkCoreKit(SDKCoreKit sdkCoreKit) {
        this.sdkCoreKit = assureApi(sdkCoreKit);
    }

    public PayCard() {
        this.sdkCoreKit = assureApi(null);
    }

    /**
     * @param SDKCoreKit|null instance
     * @return SDKCoreKit
     * @throws InvalidArgumentException
     */
    protected static SDKCoreKit assureApi(SDKCoreKit instance) {
        instance = instance != null ? instance : SDKCoreKit.instance();
        if (instance == null) {
            throw new InvalidArgumentException("An Api instance must be provided as argument or set as instance in the SDKCoreKit");
        }
        return instance;
    }

    /**
     * @return string
     */
    protected String getEndpoint() {
        return "scratchcard";
    }

    public IResponseInterface claimScratchCard(Dictionary<String, Object> parameters) {
        //validate data input

        foreach (KeyValuePair<String, Object> entry in parameters) {
            if (!ScratchCardFields.getEnumFields().Contains(entry.Key)) {
                throw new InvalidArgumentException(entry.Key + " is not a field of " + "ScratchCardFields");
            }            
            if (entry.Value == null) {
                throw new InvalidArgumentException(entry.Key + " field must be set");
            }
        }
        IResponseInterface response = this.getSdkCoreKit().call("/scratchcard", Request.METHOD_POST, parameters);
        return response;
    }
    }
}
