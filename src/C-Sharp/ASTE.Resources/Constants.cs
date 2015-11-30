using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTE.Resources
{
    public class Constants
    {
        #region API Discovery
        public const string ASTE_APIDISCOVERY_PROCESS_PREFIX = "ConfigApi/1.0/getActiveProcesses";
        public const string ASTE_APIDISCOVERY_MODULE_PREFIX = "ConfigApi/1.0/getActiveModules";
        #endregion

        #region Ping
        public const string ASTE_PINGSTATUS_SUCCESS = "success";
        public const string ASTE_PING_STATUS_ERROR = "error";
        #endregion

        #region Module type
        public const string ASTE_MODULETYPE_MODULE = "module";
        public const string ASTE_MODULETYPE_PROCESS = "process";
        #endregion

        #region module / process GUID's
        public const string ASTE_MODULES_FORM_MODULE_GUID = "7080C368-82F0-4BA5-849F-F6E246F3211B";
        public const string ASTE_MODULES_API_DISCOVERY_GUID = "946B0E33-0A24-441F-A681-D2924245E7A2";
        public const string ASTE_PROCESS_MIELENTERVEYSSEURA = "928245E9-90F4-4DC6-B959-A1789BD69280";
        public const string ASTE_MODULES_LOGGER = "58965B8C-9295-43B1-8CBA-31003E565003";
        #endregion


        #region Web.Config keys
        public const string API_DISCOVERY_URL_KEY = "API_Discovery_Url";
        #endregion


    }
}
