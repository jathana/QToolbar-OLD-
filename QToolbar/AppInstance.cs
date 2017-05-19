using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QToolbar
{
   public static class AppInstance
   {


      /// <summary>
      /// Local directory to save app data.
      /// </summary>
      public static string UserAppDataPath
      {
         get
         {

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.AppDomain.CurrentDomain.FriendlyName);
         }
      }

      /// <summary>
      /// Application Cache dir.
      /// </summary>
      public static string CacheDirectory
      {
         get
         {
            string dir= Path.Combine(UserAppDataPath, "Cache");
            Utils.EnsureFolder(dir);
            return dir;
         }
      }

      /// <summary>
      /// Cache file for cf admin files information.
      /// </summary>
      public static string CFsTreeCacheFile
      {
         get
         {
            return Path.Combine(CacheDirectory, "AdminCFsDBs.xml");
         }
      }


   }
}
