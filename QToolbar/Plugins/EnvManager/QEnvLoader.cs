using Microsoft.Web.Administration;
using QToolbar.Helpers;
using QToolbar.Options;
using QToolbar.Plugins.Environments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;

namespace QToolbar.Plugins.EnvManager
{
    public class QEnvLoader
    {
        #region fields
        private List<QEnv> _Data = new List<QEnv>();
        private List<Task> _Tasks = new List<Task>();
        private Dictionary<string, CancellationTokenSource> _CancellationTokenSourceList = new Dictionary<string, CancellationTokenSource>();
        #endregion


        #region events
        public delegate void InfoCollectedEventHandler(object sender, EnvInfoEventArgs args);
        public event InfoCollectedEventHandler InfoCollected;
        public event EventHandler AllInfoCollected;
        #endregion


        public List<QEnv> Data
        {
            get { return _Data; }
        }
        public void AddOrUpdate(string envName, CfFile qbcAdminCf, string checkoutPath, string proteusCheckoutPath)
        {
            QEnv envObj = null;
            bool adding = false;
            List<QEnv> envsFound = _Data.FindAll(e => e.Name == envName);
            if (envsFound.Count == 0)
            {
                // add 
                envObj = new QEnv();
                adding = true;
            }
            else if (envsFound.Count == 1)
            {
                // update
                envObj = envsFound[0];
            }
            else if (envsFound.Count > 1)
            {
                throw new Exception($"Found environment {envName} multiple times.");
            }
            if (envObj != null)
            {
                envObj.Status = "Loading...";
                envObj.Name = envName;
                envObj.CheckoutPath = checkoutPath;
                envObj.ProteusCheckoutPath = proteusCheckoutPath;
                envObj.QCS_CLIENT.QBC_ADMIN.QBC_SERVER.Value = qbcAdminCf.GetServer(envName);
                envObj.QCS_CLIENT.QBC_ADMIN.QBC_DB.Value = qbcAdminCf.GetDatabase(envName);
                envObj.QCS_CLIENT.QBC_ADMIN.TOOLKIT_WS_URL.Value = qbcAdminCf.GetValue("General", "ToolkitWSURL");
                envObj.QCS_CLIENT.QBC_ADMIN.APPLICATION_WS_URL.Value = qbcAdminCf.GetValue("General", "ApplicationWSURL");

                envObj.QBCAdminCfPath = qbcAdminCf.File;

                if (string.IsNullOrEmpty(checkoutPath))
                {
                    envObj.Errors.AddError($"Checkout path is empty.", "");
                }
                if (string.IsNullOrEmpty(proteusCheckoutPath))
                {
                    envObj.Errors.AddWarning($"Proteus checkout path is empty.", "");
                }

            }
            if (adding)
            {
                _Data.Add(envObj);
            }

            // collect rest info async
            CollectEnvInfoAsync(envObj);
        }

        public void Remove(string envName)
        {
            List<QEnv> rs = _Data.FindAll(e => e.Name == envName);
            if (rs.Count == 1)
            {
                CancellationTokenSource cts = _CancellationTokenSourceList.FirstOrDefault(ct => ct.Key == envName).Value;
                if (cts != null)
                    cts.Cancel();
                _CancellationTokenSourceList.Remove(envName);
                _Data.Remove(rs[0]);
            }
            else if (rs.Count > 1)
            {
                throw new Exception($"Cannot delete.Multiple environments {envName} found.");
            }

            Debug.WriteLine(_CancellationTokenSourceList.Count);
        }

        public void Refresh()
        {
            string envName = string.Empty;
            CfFile cf = new CfFile(string.Empty);
            string checkoutPath = string.Empty;
            string proteusCheckoutPath = string.Empty;
            QEnv item = null;
            for (int i = 0; i < _Data.Count; i++)
            {
                item = _Data[i];
                envName = item.Name;
                cf.File = item.QBCAdminCfPath;
                checkoutPath = item.CheckoutPath;
                proteusCheckoutPath = item.ProteusCheckoutPath;

                item.Clear();
                item.Name = envName;

                AddOrUpdate(envName, cf, checkoutPath, proteusCheckoutPath);
                Debug.WriteLine(envName);
            }

        }

        private async void CollectEnvInfoAsync(QEnv env)
        {
            CancellationTokenSource tokenSource = null;

            if (!_CancellationTokenSourceList.ContainsKey(env.Name))
            {
                tokenSource = new CancellationTokenSource();
                _CancellationTokenSourceList.Add(env.Name, tokenSource);
            }
            else
            {
                tokenSource = _CancellationTokenSourceList[env.Name];
            }

            Task<QEnv> rs = Task<QEnv>.Factory.StartNew(state =>
            {
                QEnv objEnv = (QEnv)state;

                CancellationToken cancelToken = tokenSource.Token;

                List<string> eodFlows = new List<string>();

                CfFile adminCf = new CfFile(env.QBCAdminCfPath);

                if (!cancelToken.IsCancellationRequested)
                {
                    // load parameters from databases
                    QEnvDatabasesLoader dbLoader = new QEnvDatabasesLoader();
                    dbLoader.Load(objEnv, eodFlows, cancelToken);

                    // Check environment's files & folders
                    QEnvFileSystemLoader fsLoader = new QEnvFileSystemLoader();
                    fsLoader.Load(objEnv, adminCf, eodFlows, cancelToken);

                    // load dependency values
                    QEnvDependenciesLoader depLoader = new QEnvDependenciesLoader();
                    depLoader.Load(objEnv);

                    // Deactivate properties not used
                    QEnvPropertiesProcessor processor = new QEnvPropertiesProcessor();
                    processor.Process(objEnv);

                    objEnv.Validate();
                };
                return objEnv;
            }, env, tokenSource.Token);

            _Tasks.Add(rs);

            await Task.WhenAll(rs).ContinueWith((t) =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
             {
                 OnInfoCollected(new EnvInfoEventArgs(env));
             });
            });

            // when all tasks are completed raise AllInfoCollected event
            await Task.Factory.ContinueWhenAll(_Tasks.ToArray(),
               (z) =>
               {
                   AllInfoCollected(this, new EventArgs());
               });
        }



        private void OnInfoCollected(EnvInfoEventArgs args)
        {
            if (InfoCollected != null)
            {
                InfoCollected(this, args);
            }
        }

        private void OnAllInfoCollected(EventArgs args)
        {
            if (AllInfoCollected != null)
            {
                AllInfoCollected(this, args);
            }
        }
    }

}
