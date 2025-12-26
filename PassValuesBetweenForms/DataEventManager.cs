using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PassValuesBetweenForms
{
    // Custom EventArgs for specific data
    public class SpecificDataEventArgs : EventArgs
    {
        public DataSet Data { get; set; }
        public string TargetFormType { get; set; }
    }

    // Custom EventArgs for common data
    public class CommonDataEventArgs : EventArgs
    {
        public DataSet Data { get; set; }
    }

    // *** CENTRALIZED EVENT MANAGER - NO FORM REFERENCES NEEDED ***
    public static class DataEventManager
    {
        // Events for data distribution
        public static event EventHandler<SpecificDataEventArgs> SpecificDataSent;
        public static event EventHandler<CommonDataEventArgs> CommonDataBroadcasted;

        // Methods to raise events
        public static void RaiseSpecificDataEvent(DataSet data, string targetFormType)
        {
            SpecificDataSent?.Invoke(null, new SpecificDataEventArgs
            {
                Data = data,
                TargetFormType = targetFormType
            });
        }

        public static void RaiseCommonDataEvent(DataSet data)
        {
            CommonDataBroadcasted?.Invoke(null, new CommonDataEventArgs { Data = data });
        }

        // Get subscriber counts
        public static int GetSpecificDataSubscriberCount()
        {
            return SpecificDataSent?.GetInvocationList().Length ?? 0;
        }

        public static int GetCommonDataSubscriberCount()
        {
            return CommonDataBroadcasted?.GetInvocationList().Length ?? 0;
        }
    }
}
