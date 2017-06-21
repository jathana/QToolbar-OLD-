﻿using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QToolbar.Buttons
{
   public class DesignersLocalButton : ButtonBase
   {
      public DesignersLocalButton(BarManager barManager, BarSubItem menu):base("",barManager, menu, LocalDesigner_ItemClick)
      {
      }

      public void CreateItems()
      {
         _Menu.ClearLinks();

         // load custom folders
         try
         {
            foreach (DataRow row in OptionsInstance.Checkouts.Data.Rows)
            {
               AddLocalDesignerItem(row);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve folders settings");
         }
      }

      private void AddLocalDesignerItem(DataRow row)
      {
         BarButtonItem nextBuildItem = new BarButtonItem(_BarManager, row["Name"].ToString(), 0);
         nextBuildItem.ItemClick += LocalDesigner_ItemClick;
         nextBuildItem.Tag = row;
         _Menu.AddItem(nextBuildItem);
      }

      private static void LocalDesigner_ItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            DataRow row = (DataRow)e.Item.Tag;
            // create batch content
            StringBuilder builder = new StringBuilder();

            // rem designer 7.0
            // e:
            // cd "e:\QCS Projects\7.0\VS Projects\SCToolkitDesigners\SCToolkitDesignersClient\bin\Debug"
            // SCToolkit.Designers.Client.exe
            string checkoutName = row["Name"].ToString();
            string checkoutPath = row["Path"].ToString();
            string checkoutDrive = Path.GetPathRoot(checkoutPath);
            string designerPath = Path.Combine(checkoutPath, @"VS Projects\SCToolkitDesigners\SCToolkitDesignersClient\bin\Debug");
            string designerFileName = Path.Combine(designerPath, "SCToolkit.Designers.Client.exe");
            if (File.Exists(designerFileName))
            {
               if (checkoutDrive.EndsWith(@"\"))
               {
                  checkoutDrive = checkoutDrive.Substring(0, checkoutDrive.Length - 1);
               }
               builder.AppendLine($"rem Run designer {checkoutName} at {checkoutPath}.");
               builder.AppendLine($"{checkoutDrive}");
               builder.AppendLine($"cd {designerPath}");
               builder.AppendLine($"SCToolkit.Designers.Client.exe");
               string FILE_NAME = Path.Combine(AppInstance.CacheDirectory, "run_designer.bat");
               FileStream fs = null;
               try
               {
                  fs = new FileStream(FILE_NAME, FileMode.Create);
                  using (StreamWriter writer = new StreamWriter(fs))
                  {
                     writer.Write(builder.ToString());
                     writer.Close();
                  }

                  System.Diagnostics.Process process = new System.Diagnostics.Process();
                  process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                  process.StartInfo.FileName = FILE_NAME;
                  process.StartInfo.UseShellExecute = true;
                  process.Start();
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show($"Failed to open designer {checkoutName}, {ex.Message}");
               }
               finally
               {
                  if (fs != null)
                     fs.Dispose();
               }
            }
            else
            {
               XtraMessageBox.Show($"Designer's executable File not found!Please open VS and build SCToolkitDesigners.sln\r\n{designerFileName}", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }


   }
}
