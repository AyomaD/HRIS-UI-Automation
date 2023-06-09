using System.Collections;
using System.Globalization;
using System.Resources;

namespace UI_Automation.Util
{
    class DataReader
    {
        public static Dictionary<string, string> getData(String environment)
        {
            ResourceSet resourceSet = null;

            Dictionary<string, string> dataSet = new Dictionary<string, string>();
            if (environment == "dev")
            {
                ResourceManager resourceManager = new ResourceManager(typeof(TestData.DevEnvData));
                resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            }
            if (environment == "test")
            {
                ResourceManager resourceManager = new ResourceManager(typeof(TestData.TestEnvData));
                resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            }

            foreach (DictionaryEntry entry in resourceSet)
            {
                dataSet.Add(entry.Key.ToString(), entry.Value.ToString());

            }
            return dataSet;
        }

        public static Dictionary<string, string> getAutomationConfigData()
        {
            ResourceSet resourceSet = null;
            Dictionary<string, string> dataSet = new Dictionary<string, string>();

            ResourceManager resourceManager = new ResourceManager(typeof(TestData.ConfigData));
            resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                dataSet.Add(entry.Key.ToString(), entry.Value.ToString());

            }
            return dataSet;
        }
    }
}

