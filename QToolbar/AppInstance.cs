using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar
{
   public static class AppInstance
   {


      public static string TmpDirectory
      {
         get
         {
            return Path.Combine(Path.GetTempPath(), "QToolbar");
         }
      }

      public static string CacheDirectory
      {
         get
         {
            return Path.Combine(TmpDirectory, "Cache");
         }
      }

      public static string CFsTreeCacheFile
      {
         get
         {
            return Path.Combine(CacheDirectory, "AdminCFsDBs.xml");
         }
      }


   }
}
