using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.EnvManager
{
    internal class QEnvPropertiesProcessor
    {
        public void Process(QEnv objEnv)
        {
            // first of all deactivate properties
            DeactivateProperties(objEnv);

            // ResolveUNCDirs
            ResolveUNCDirs(objEnv);
        }

        #region deactivate properties
        private void DeactivateProperties(QEnv objEnv)
        {
            DeactivateIfEmptyValue(objEnv.QBC.AT_SYSTEM_PARAMS.ARCHIVE_SERVER);
            DeactivateIfEmptyValue(objEnv.QBC.AT_SYSTEM_PARAMS.ARCHIVE_DATABASE);

        }
        private void DeactivateIfEmptyValue(QEnvProperty prop)
        {
            if (prop.EmptyValue)
            {
                prop.Active = false;
            }

        }
        #endregion

        #region resolve UNC Dirs
        private void ResolveUNCDirs(QEnv objEnv)
        {

        }

        
        #endregion
    }
}
