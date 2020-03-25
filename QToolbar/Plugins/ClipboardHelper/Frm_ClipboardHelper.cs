using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System.IO;
using System.Diagnostics;

namespace QToolbar.Plugins.ClipboardHelper
{
   public partial class Frm_ClipboardHelper : DevExpress.XtraEditors.XtraForm
   {

      public delegate Tuple<bool, string> RunAction(string branchPath);

      private List<string> _Memory = new List<string>();

      private List<SimpleButton> _Buttons;

      private string _BtnPrefix = "btnClip";      
      private string _BtnDefaultText = "<Empty>";
      private int _BtnIndexSize = 2; // numbers of digits button's name contains at its end

      public Frm_ClipboardHelper()
      {
         InitializeComponent();

         GetButtons();
         InitButtons();
      }

      private void GetButtons()
      {
         // get buttons
         var ctls = from c in layoutControl2.Controls.OfType<SimpleButton>()
                    where c.Name.StartsWith(_BtnPrefix)
                    select c;

         _Buttons = ctls.ToList();
      }
      private void InitButtons()
      {
         // prepare buttons
         _Buttons.ForEach(b => { b.Click += btnClip_Click; b.Text = _BtnDefaultText; });
      }

      private void btnClip_Click(object sender, EventArgs e)
      {
         SimpleButton btn = (SimpleButton)sender;
         int index = GetButtonIndex(btn.Name);
         if (index < _Memory.Count)
         {
            Clipboard.SetText(_Memory[index]);
         }
      }

      private string GetButtonName(int i)
      {
         return $"{_BtnPrefix}{i.ToString().PadLeft(2, '0')}";
      }

      private int GetButtonIndex(string name)
      {
         return Convert.ToInt32(name.Substring(name.Length - _BtnIndexSize, _BtnIndexSize));
      }
      private void btnApply_Click(object sender, EventArgs e)
      {
         Clear();
         if (!string.IsNullOrWhiteSpace(memTokens.Text))
         {
            _Memory = memTokens.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            SetButtonsText();
         }
      }

      private void Clear()
      {
         _Memory.Clear();

         _Buttons.ForEach(b => { b.ForeColor = SystemColors.ActiveCaptionText; b.Text = _BtnDefaultText; });
      }

      private void SetButtonsText()
      {         
         for(int i = 0; i < _Memory.Count; i++)
         {
            Control[] controls = Controls.Find(GetButtonName(i), true);
            if(controls.Length > 0)
            {
               ((SimpleButton)controls[0]).Text = _Memory[i];
               ((SimpleButton)controls[0]).ForeColor = Color.Red;
            }
         }
      }
   }

}