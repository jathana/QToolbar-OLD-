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
using DevExpress.XtraGrid.Views.Grid;

namespace QToolbar
{
   public partial class uc_SQLQueriesEdit : EditFormUserControl
   {
      public uc_SQLQueriesEdit()
      {
         InitializeComponent();
      }

      //public string Query_Name
      //{
      //   get { return txtName.EditValue.ToString(); }
      //   set { txtName.EditValue = value; }
      //}

      //public string Query_SQL
      //{
      //   get { return txtSQL.Text; }
      //   set { txtSQL.Text = value; }
      //}

      //public bool Query_RunImmediate
      //{
      //   get { return Convert.ToBoolean(chkRunImmediate.EditValue); }
      //   set { chkRunImmediate.EditValue = value; }
      //}


   }
}
