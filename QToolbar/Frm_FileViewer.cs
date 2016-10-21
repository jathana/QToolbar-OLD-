﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QToolbar
{
   public partial class Frm_FileViewer : DevExpress.XtraEditors.XtraForm
   {
      public Frm_FileViewer()
      {
         InitializeComponent();
      }


      public void ViewFile(string file)
      {
         
         if(File.Exists(file))
         {
            Text = file;
            Show();

            try
            {
               memContent.Text = File.ReadAllText(file);
            }
            catch(Exception ex)
            {
               XtraMessageBox.Show(string.Format("Cannot open file:", ex.Message));
            }
         }
         else
         {
            throw new Exception("File not found");
         }

      }
   }
}