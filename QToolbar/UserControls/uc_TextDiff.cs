using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DiffPlex;
using FastColoredTextBoxNS;

namespace QToolbar.UserControls
{
   public partial class uc_TextDiff : DevExpress.XtraEditors.XtraUserControl
   {
      public uc_TextDiff()
      {
         InitializeComponent();
      }

      public string LeftText
      {
         get
         {
            return txtLeft.Text;
         }
         set
         {
            txtLeft.Text = value;
         }
      }

      public string RightText
      {
         get
         {
            return txtRight.Text;
         }
         set
         {
            txtRight.Text = value;
         }
      }

      public FastColoredTextBoxNS.Language Language
      {
         get
         {
            return txtLeft.Language;
         }
         set
         {
            txtLeft.Language = value;
            txtRight.Language = value;
         }
      }


      public void Compare(bool ignoreWhiteSpace)
      {
         var differ = new Differ();
         var charDiffs = differ.CreateCharacterDiffs(txtLeft.Text, txtRight.Text, true);
         foreach(var block in charDiffs.DiffBlocks)
         {
            //Place p=new Place()
            //txtLeft.Selection.Start.Range..Start.iChar = block.DeleteCountA;
         }
      }

   }
}
