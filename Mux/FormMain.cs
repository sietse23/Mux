using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mux.Properties;
using Mux.Classes.Algemeen;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Mux
{
    public partial class FormMain : Form
    {
        #region Variables
        /********************************************** Variables ******************************************************************/

        private Settings m_Settings;
        private bool m_OK = true;

        private string m_Pad = "";

        private int m_Index;

        private Process m_Process;

        #endregion Variables

        #region Constructor, Load & Closing
        /********************************************** Constructor, Load & Closing ************************************************/

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Init();

            this.Top = 100;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 200;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
        }

        private void Init()
        {
            m_Settings = new Settings();
            m_Settings.Reload();

            if (!string.IsNullOrEmpty(m_Settings.AviDemuxExe) && File.Exists(m_Settings.AviDemuxExe))
            {
                Globals.AviDemuxExe = m_Settings.AviDemuxExe;
            }
            else
            {
                ofdDialog.Filter = "AviDemux|avidemux_cli.exe";

                if (ofdDialog.ShowDialog() == DialogResult.OK)
                {
                    Globals.AviDemuxExe = m_Settings.AviDemuxExe = ofdDialog.FileName;
                    m_Settings.Save();
                }
                else
                {
                    m_OK = false;
                }
            }

            if (!string.IsNullOrEmpty(m_Settings.LastDir))
            {
                if (Directory.Exists(m_Settings.LastDir))
                {
                    Globals.LastDir = m_Settings.LastDir;
                }
                else
                {
                    string myDir = Path.GetDirectoryName(m_Settings.LastDir);

                    while (myDir != null && !Directory.Exists(myDir))
                    {
                        myDir = Path.GetDirectoryName(myDir);
                    }

                    if (Directory.Exists(myDir))
                    {
                        Globals.LastDir = m_Settings.LastDir = myDir;
                        m_Settings.Save();
                    }
                }
            }

            Globals.TermplatePad = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Template");
            Globals.TermplatePad = Path.Combine(Globals.TermplatePad, "Script.js");
        }

        #endregion Constructor, Load & Closing


        #region Button Events
        /********************************************** Button Events **************************************************************/

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        #endregion Button Events

        #region Control Events
        /********************************************** Control Events *************************************************************/

        #endregion Control Events

        #region Functions
        /********************************************** Functions ******************************************************************/

        private void OpenFiles()
        {
            lvFiles.Clear();

            ofdDialog.Filter = "Video|*.mkv;*.vob;*.mp4;*.ts";

            if (Directory.Exists(Globals.LastDir))
            {
                ofdDialog.InitialDirectory = Globals.LastDir;
            }

            ofdDialog.InitialDirectory = @"D:\Film\Series";

            ofdDialog.Multiselect = true;

            using (new CenterWinDialog(this))
            {
                if (ofdDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && ofdDialog.FileNames.Length > 0)
                {
                    m_Pad = m_Settings.LastDir = Path.GetDirectoryName(ofdDialog.FileNames[0]);
                    m_Settings.Save();

                    foreach (string myFile in ofdDialog.FileNames)
                    {
                        AddFile(myFile);
                    }
                }
            }
        }

        private void AddFile(string FileName)
        {
            lvFiles.Items.Add(Path.GetFileName(FileName), 0);
        }

        private void Start()
        {
            m_Index = 0;

            StartFile();
        }

        private void StartFile()
        {
            lvFiles.Items[m_Index].ImageIndex = 1;

            try
            {

                string myVideoFile = Path.Combine(m_Pad, lvFiles.Items[m_Index].Text);
                string myScriptFile = Path.ChangeExtension(myVideoFile, "js");

                string myScript = File.ReadAllText(Globals.TermplatePad);
                myScript = myScript.Replace("$PAD", myVideoFile.Replace(@"\", @"/"));
                File.WriteAllText(myScriptFile, myScript);

                myVideoFile = Path.ChangeExtension(myVideoFile, "avi");

                m_Process = new Process();
                
                m_Process.StartInfo.FileName = Globals.AviDemuxExe;
                //m_Process.StartInfo.Arguments = string.Format("--autoindex --force-unpack --rebuild-index --reuse-2pass-log --run \"{0}\" --save \"{1}\"", myScriptFile, myVideoFile);
                //m_Process.StartInfo.Arguments = string.Format("--autoindex --force-unpack --run \"{0}\" --save \"{1}\"", myScriptFile, myVideoFile);
                m_Process.StartInfo.Arguments = string.Format("--run \"{0}\" --save \"{1}\"", myScriptFile, myVideoFile);

                m_Process.StartInfo.UseShellExecute = false;
                m_Process.StartInfo.CreateNoWindow = true;

                m_Process.Exited += new EventHandler(myProcess_Exited);
                m_Process.EnableRaisingEvents = true;

                m_Process.StartInfo.RedirectStandardOutput = true;
                m_Process.StartInfo.RedirectStandardError = true;

                m_Process.OutputDataReceived += new DataReceivedEventHandler(CaptureOutput);
                m_Process.ErrorDataReceived += new DataReceivedEventHandler(CaptureError);


                m_Process.Start();

                m_Process.BeginOutputReadLine();
                m_Process.BeginErrorReadLine();

                m_Process.PriorityClass = ProcessPriorityClass.High;

                //m_Process.WaitForExit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Fout();
            }
        }

        private void StartThread()
        {

        }


        private void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    LogTekst(e.Data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CaptureError(object sender, DataReceivedEventArgs e)
        {
            try
            {                
                if (e.Data != null)
                {
                    LogTekst(e.Data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            if (m_Process.ExitCode == 0)
            {
                Klaar();
            }
            else
            {
                Fout();
            }
        }

        private void LogTekst(string Tekst)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.LogTekst(Tekst)));
            else
            {
                rtbLog.AppendText(Tekst + Environment.NewLine);
                rtbLog.SelectionStart = rtbLog.Text.Length;
                rtbLog.ScrollToCaret();
                Application.DoEvents();
            }
        }

        private void Klaar()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Klaar()));
            else
            {
                lvFiles.Items[m_Index].ImageIndex = 2;
                Cleanup();

                m_Index++;

                if (m_Index < lvFiles.Items.Count)
                {
                    StartFile();
                }
            }
        }

        private void Fout()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Klaar()));
            else
            {
                lvFiles.Items[m_Index].ImageIndex = 3;
                Cleanup();
            }
        }

        private void Cleanup()
        {
            string myVideoFile = Path.Combine(m_Pad, lvFiles.Items[m_Index].Text);            
            string myScriptFile = Path.ChangeExtension(myVideoFile, "js");
            myVideoFile = Path.ChangeExtension(myVideoFile, "avi");
            string myStatsFile = myVideoFile + ".stats";

            DeleteFile(myScriptFile);
            DeleteFile(myStatsFile);
        }

        public void DeleteFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                try
                {
                    File.Delete(FileName);
                }
                catch
                {
                    Application.DoEvents();
                    Thread.Sleep(500);
                    Application.DoEvents();

                    try
                    {
                        File.Delete(FileName);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        #endregion Functions



        #region Properties
        /********************************************** Properties *****************************************************************/

        #endregion Properties
    }
}
