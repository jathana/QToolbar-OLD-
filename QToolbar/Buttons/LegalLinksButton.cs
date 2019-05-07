using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
    public class LegalLinksButton : ButtonBase
    {

        public LegalLinksButton(BarManager barManager, BarSubItem menu) : base("", barManager, menu)
        {
        }


        public override void CreateMenuItems()
        {

            _Menu.ClearLinks();
            // load legal links
            try
            {
                string devInst = OptionsInstance.DevSQLInstances;
                if (!string.IsNullOrEmpty(devInst))
                {
                    List<string> devdbs = new List<string>();
                    SortedList<string, string> menuItems = new SortedList<string, string>();

                    string[] devInstArr = devInst.Split(',');
                    Regex reg = new Regex("^QBCollection[_]Plus[_][0-9]+[_][0-9]+[_]*[0-9]*$");
                    Regex regVer = new Regex("[0-9]+[_][0-9]+[_]*[0-9]*$");
                    foreach (string sqlInst in devInstArr)
                    {
                        //devdbs.Clear();
                        //menuItems.Clear();
                        //string connectionString = $"Server={sqlInst};Data Source=.; Integrated Security=SSPI;";
                        string connectionString = $"Server={sqlInst};Integrated Security=SSPI;";
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            try
                            {
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                                {
                                    try
                                    {
                                        using (SqlDataReader dr = cmd.ExecuteReader())
                                        {
                                            while (dr.Read())
                                            {
                                                if (reg.IsMatch(dr[0].ToString()))
                                                {
                                                    devdbs.Add(dr[0].ToString());
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                }

                                foreach (var db in devdbs)
                                {
                                    string legalUrl = "";
                                    object urlObj = null;
                                    try
                                    {
                                        using (SqlCommand cmd = new SqlCommand($"SELECT SPR_VALUE FROM [{db}].[dbo].AT_SYSTEM_PREF WHERE SPR_TYPE='LEGAL_APP_PROCESS_MAPPING_WS_URL'", con))
                                        {

                                            urlObj = cmd.ExecuteScalar();
                                            if (urlObj != null)
                                            {
                                                string url = cmd.ExecuteScalar().ToString();
                                                if (!string.IsNullOrEmpty(url))
                                                {
                                                    Uri uri = new Uri(url.Trim());
                                                    legalUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}/QCLegalApplicationApp/";
                                                }
                                            }
                                        }

                                        string dbVer = "";
                                        if (urlObj != null)
                                        {
                                            //using (SqlCommand cmd = new SqlCommand($"SELECT TOP(1)CONVERT(NVARCHAR,MAJOR)+'.'+CONVERT(NVARCHAR,MINOR) VER FROM [{db}].[dbo].TLK_DATABASE_VERSIONS ORDER BY 1 DESC", con))
                                            //{
                                            //    dbVer = cmd.ExecuteScalar().ToString().Trim();
                                            //}
                                            Match verMatchVer = regVer.Match(db);
                                            if (verMatchVer.Success)
                                            {
                                                dbVer = verMatchVer.Value.Replace("_", ".");
                                            }
                                        }


                                        if (!string.IsNullOrEmpty(dbVer) && !string.IsNullOrEmpty(legalUrl))
                                        {
                                            menuItems.Add(dbVer, legalUrl);
                                        }
                                    }
                                    catch { }
                                }
                                con.Close();
                            }
                            catch { }
                        }

                    }
                    foreach (var key in menuItems.Keys)
                    {
                        AddLegalLinksItem(new Tuple<string, string>(key, menuItems[key]));
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Failed to retrieve legal links");
            }
        }

        private void AddLegalLinksItem(Tuple<string, string> data)
        {
            BarButtonItem legalLinkItem = new BarButtonItem(_BarManager, data.Item1, 3);
            // legal links are shell commands
            legalLinkItem.ItemClick += MenuItemClick;
            legalLinkItem.Tag = data;                        
            _Menu.AddItem(legalLinkItem);
        }

        protected override void MenuItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Tuple<string, string> data = (Tuple<string, string>)e.Item.Tag;

                // only IE is suppoerted by LegalApp
                System.Diagnostics.Process.Start("IEXPLORE.EXE", data.Item2);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
