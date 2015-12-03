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
        public const string ASTE_APIDISCOVERY_PROCESS_PREFIX = "api/1.0/activeprocess";
        public const string ASTE_APIDISCOVERY_MODULE_PREFIX = "api/1.0/activemodule";
        public const string ASTE_APIDISCOVERY_INTERNAL_USE_API_KEY = "7DAB50E3-B5D5-45DD-BBAC-C195D646507B";
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
        public const string ASTE_MODULES_LOGGER = "58965B8C-9295-43B1-8CBA-31003E565003";
        public const string ASTE_MODULES_RSSFEED = "40A41882-7095-4F2F-BCCC-01A1F5BEAC0F";
        public const string ASTE_MODULES_STORAGE_DATATEMPLATE = "0A92E10D-733B-43D1-BE92-E3F8E925B49F";
        public const string ASTE_MODULES_STORAGE_RENDERTEMPLATE = "7068D9E8-1F62-4226-873E-66652AF02909";
        public const string ASTE_MODULES_STORAGE_TEMPLATECENTER = "77ABA507-68D7-4519-8F3A-A203B697BF06";
    
        public const string ASTE_PROCESS_MIELENTERVEYSSEURA = "928245E9-90F4-4DC6-B959-A1789BD69280";
        public const string ASTE_PROCESS_SNIPPET = "";

        #endregion


        #region Web.Config keys
        public const string API_DISCOVERY_URL_KEY = "API_Discovery_Url";
        #endregion

        #region Template types
        public const int ASTE_TEMPLATE_RENDERTYPE_EDIT = 1;
        public const int ASTE_TEMPLATE_RENDERTYPE_DISPLAY = 2;
        #endregion

        #region Template Keywords

        public const string ASTE_TEMPLATE_KEYWORD_FOREACH = "<# FOREACH #>";
        public const string ASTE_TEMPLATE_KEYWORD_END = "<# END #>";
        public const string ASTE_TEMPLATE_KEYWORD_IF = "<# IF #>";
        public const string ASTE_TEMPLATE_KEYWORD_ELSE = "<# ELSE #>";
        public const string ASTE_TEMPLATE_START_PREFIX = "<# ";
        public const string ASTE_TEMPLATE_END_PREFIX = " #>";
        public const string ASTE_TEMPLATE_DATA_PREFIX = "DATA.";

        #endregion  


    }
}
