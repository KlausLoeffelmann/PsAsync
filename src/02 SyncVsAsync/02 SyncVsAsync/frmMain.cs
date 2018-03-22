using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncVsAsync
{
    public partial class frmMain : Form
    {
        private const int DIGITS_OF_PI = 100000;
        private const int BYTES_TO_WRITE = 200000000;
        private const string BIG_FILES_NAME = "\\demodata.bin";

        private DateTime myStartTime;
        private string myPiLabelContent = "";
        private byte[] myRandomBytes = new byte[BYTES_TO_WRITE];
        private bool mySignalEndProgram;

        private bool myUpdateElapsedTimeSuspended;

        public frmMain()
        {
            // This call is required by the designer.
            InitializeComponent();

            //We do not want to store the throttle setting
            //before InitializeComponent is done, because otherwise
            //the setting always gets overridden with the default value.
            ThrottleTrackBar.ValueChanged += ThrottleTrackBar_ValueChanged;

            //These are the bytes we want to write as demodata.bin on the memory stick.
            var randomGenerator = new Random(DateTime.Now.Millisecond);
            randomGenerator.NextBytes(myRandomBytes);

            base.Load += MainForm_Load;
            btnPickPath.Click += btnPickPath_Click;
            btnWriteFileSync.Click += btnWriteFileSync_Click;
            btnWriteFileAsync.Click += btnWriteFileAsync_Click;
            btnAwaitWriteFile.Click += btnAwaitWriteFile_Click;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //When the Form shows, we take the last folder path from the Settings.
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastPathToThumbDriveFolder))
            {
                TargetFolder = Properties.Settings.Default.LastPathToThumbDriveFolder;
                ThrottleTrackBar.Value = Properties.Settings.Default.ThrottleValue;
            }

            //Setting the titel label (don't we love string interpolation? :-)
            lblTitel.Text = $"Calculating {DIGITS_OF_PI} digits of Pi ({Math.PI}...)";

            //We schedule this for execution as soon as the message loop is idling again.
            //The remaining code for loading the form can therefore finish first.
            this.BeginInvoke(new Action(StartAfterMainFormLoaded));
        }

        private void StartAfterMainFormLoaded()
        {
            try
            {
                //So, we're immediately starting calculating PI to 100,000 digits!
                myStartTime = DateTime.Now;

                //Why this?
                this.Refresh();
                var piResult = DoPiCalc();
                txtPiResult.Visible = true;
                txtPiResult.Text = piResult;
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
        }

        //Updates the UI, so we can see some Pi calculating is going on.
        public void UpdateUI(int digitNo, string currentDigit)
        {

            //When we throttle the processor to calculate very slowly,
            //we can actually see how each next digit of pi come in.
            if (myPiLabelContent.Length < 10)
            {
                myPiLabelContent += currentDigit;
            }
            else
            {
                myPiLabelContent = myPiLabelContent.Substring(myPiLabelContent.Length - 9, 9) + currentDigit;
            }

            //Updating the pi-fragment with the newly calculated pi digit.
            lblCurrentPiFragment.Text = myPiLabelContent;

            //Updating which No of Pi digit we just calculated.
            lblDigitNo.Text = digitNo.ToString();

            //Updating the progress bar.
            piCalculationProgress.Value = Convert.ToInt32(100 / (double)DIGITS_OF_PI * digitNo);

            if (!myUpdateElapsedTimeSuspended)
            {
                //Also update the time elapsed:
                lblElapsedTime.Text = (DateTime.Now - myStartTime).ToString("h\\:mm\\:ss");
            }

            //Let the app breathe! We need the Message Loop to catch on.
            Application.DoEvents();

            //And this is how we throttle things. We're just suspending the 
            //the thread for n milliseconds. The processor does not do any work
            //in that period of time.
            Thread.Sleep(200 - ThrottleTrackBar.Value * 20);
        }

        private void btnPickPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderGetter = new FolderBrowserDialog
            {
                Description = "Pick Path to Thumb Drive:",
                ShowNewFolderButton = true
            };

            if (!string.IsNullOrWhiteSpace(txtTargetFolder.Text))
            {
                folderGetter.SelectedPath = TargetFolder;
            }

            var dResult = folderGetter.ShowDialog();
            if (dResult == DialogResult.OK)
            {
                //Setting the target folder also takes care
                //of serializing the setting.
                TargetFolder = folderGetter.SelectedPath;
            }
        }

        //If the path to the file on the thumb drive does not exist,
        //we're creating it first.
        private void EnsureFolderExists()
        {
            DirectoryInfo testFolderInfo = new DirectoryInfo(TargetFolder);
            if (!testFolderInfo.Exists)
            {
                testFolderInfo.Create();
            }
        }

        public string TargetFolder
        {
            get
            {
                return txtTargetFolder.Text;
            }
            set
            {
                txtTargetFolder.Text = value;
                Properties.Settings.Default.LastPathToThumbDriveFolder = txtTargetFolder.Text;
                Properties.Settings.Default.Save();
            }
        }

        // HOOK 1!
        //This is the sync version of putting the 100 MByte file on the memory stick.
        //Observe, how the processor workload changes in the task manager,
        //and how "responsive" the app becomes!
        private void btnWriteFileSync_Click(object sender, EventArgs e)
        {

            lblElapsedTime.Text = "Saving File...";

            //There is no active Message Loop at this time, so the
            //WM_PAINT for the label is coming in way too late!
            //That's why we have to force the refresh directly.
            lblElapsedTime.Refresh();

            try
            {
                EnsureFolderExists();

                FileStream fs = new FileStream(TargetFolder + BIG_FILES_NAME, FileMode.Create);

                fs.Write(myRandomBytes, 0, myRandomBytes.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something did not go according to plan..." + Environment.NewLine + 
                                "(did you take the memory stick out?)" + "\r" + "\r" + ex.Message, ex.GetType().ToString());
            }
        }

        // HOOK 2!
        //This is the old fashion async version.
        //We're just kicking of the "put-the-file-on-stick"-operation...
        private void btnWriteFileAsync_Click(object sender, EventArgs e)
        {
            lblElapsedTime.Text = "Saving File...";
            //This time, we need no Refresh. Since this event handler is only
            //kicking of the I/O Bound workload (on the same thread, btw!)
            //the message loop continues, and the UI gets updated automatically.

            //However: Since we want to see 'Saving File...' for as long as it takes,
            //we should not update the time elapsed display for the time being.
            myUpdateElapsedTimeSuspended = true;

            try
            {
                EnsureFolderExists();

                FileStream fs;

                //We need to force the FileStream to really be asynchronous. That means,
                //setting the flags, reserving a buffer big enough to hold the complete file
                //(otherwise already BeginWrite would be blocking!), and requesting async File-IO by
                //passing True as last parameter.
                fs = new FileStream(TargetFolder + BIG_FILES_NAME, FileMode.Create, 
                                    FileAccess.Write, FileShare.None, 1000000000, true);

                //We're only kicking of the I/O-Operation. And telling it: 
                //"Call us back. The number is E-n-d-W-r-i-t-e-F-i-l-e-P-r-o-c!"...
                var result = fs.BeginWrite(myRandomBytes, 0, myRandomBytes.Length, EndWriteFileProc, fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something did not go according to plan..." + 
                                Environment.NewLine + "(did you take the memory stick out?)" + 
                                "\r" + "\r" + ex.Message, ex.GetType().ToString());
            }
        }

        //...and it calls back here, when the component in charge of 
        //your PC (Tablet, Phone equivalent) is done with putting it there.
        //The Processor is doing absolutely nothing for this method in the meantime.
        private void EndWriteFileProc(IAsyncResult asyncResult)
        {
            FileStream fs = (FileStream)asyncResult.AsyncState;

            fs.EndWrite(asyncResult);
            fs.Flush();
            fs.Close();

            //We do not need any longer to show "Saving file..."
            myUpdateElapsedTimeSuspended = false;
        }

        // HOOK 3! - Simulating Await.
        public void WriteFileCommandProcAsyncInOneMethod(IAsyncResult asyncResult)
        {

            FileStream fs = null;

            //Test the state. If asyncResult has a content,
            //this method act as the callback part...
            if (asyncResult != null)
            {
                goto CompleteAsyncFileOperation;
            }

            //...otherweise as the kick-off part:
            fs = new FileStream(TargetFolder + BIG_FILES_NAME, FileMode.Create, FileAccess.Write, FileShare.None, 1000000000, true);
            var result = fs.BeginWrite(myRandomBytes, 0, myRandomBytes.Length, WriteFileCommandProcAsyncInOneMethod, fs);
            return;

            //AWAIT! 
            //At this point the app is awaiting the callback of the write operation.
            //And it is doing absolutely nothing!
            //(well, nothing other than cycling the Windows Message Loop).

            //And here comes the callback part:
            CompleteAsyncFileOperation:
            fs = (FileStream)asyncResult.AsyncState;
            fs.EndWrite(asyncResult);
            fs.Flush();
            fs.Close();
        }

        private async void btnAwaitWriteFile_Click(object sender, EventArgs e)
        {
             await WriteFileCommandProcAsync();
        }

        //This is the Async/Await Version.
        public async Task WriteFileCommandProcAsync()
        {

            try
            {
                EnsureFolderExists();

                FileStream fs = new FileStream(TargetFolder + BIG_FILES_NAME, FileMode.Create, FileAccess.Write, FileShare.None, 1000000000, true);

                lblElapsedTime.Text = "Saving File...";
                myUpdateElapsedTimeSuspended = true;
                await fs.WriteAsync(myRandomBytes, 0, myRandomBytes.Count());
                await fs.FlushAsync();
                fs.Close();
                myUpdateElapsedTimeSuspended = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something did not go according to plan..." + Environment.NewLine + "(did you take the memory stick out?)" + "\r" + "\r" + ex.Message, ex.GetType().ToString());
            }
        }

        //Yet another version, done by a task alone. No await, no async.
        public void WriteFileCommandProcApmTaskCoveredAsync()
        {

            EnsureFolderExists();

            FileStream fs = new FileStream(TargetFolder + BIG_FILES_NAME, FileMode.Create, FileAccess.Write, FileShare.None, 1000000000, true);


            var taskToReturn = Task.Factory.FromAsync(fs.BeginWrite, fs.EndWrite, myRandomBytes, 0, myRandomBytes.Length, null).
                ContinueWith((orgTask) =>
                    {
                        fs.Flush();
                        fs.Close();
                    });
        }

        //Simple Pi_Calc Algorithm.
        private string DoPiCalc()
        {
            PiCalc piCalc = new PiCalc();

            BigInteger TWO = 2;
            BigInteger TEN = 10;
            BigInteger k = 2;
            BigInteger a = 4;
            BigInteger b = 1;
            BigInteger a1 = 12;
            BigInteger b1 = 4;

            StringBuilder sb = new StringBuilder();

            var digitCount = -1;

            do
            {
                BigInteger p = k * k;
                var q = TWO * k + BigInteger.One;
                k = k + BigInteger.One;
                var tempa1 = a1;
                var tempb1 = b1;

                a1 = p * a + q * a1;
                b1 = p * b + q * b1;
                a = tempa1;
                b = tempb1;
                BigInteger d = a / b;
                BigInteger d1 = a1 / b1;

                while (d == d1)
                {
                    if (digitCount == -1)
                    {
                        sb.Append(" ");
                        sb.Append(d.ToString());
                        digitCount += 1;
                    }
                    else
                    {
                        sb.Append(d.ToString());
                        digitCount += 1;
                    }

                    if (digitCount % 10 == 0)
                    {
                        sb.Append(" ");
                    }

                    UpdateUI(digitCount, d.ToString());

                    a = TEN * (a % b);
                    a1 = TEN * (a1 % b1);
                    d = a / b;
                    d1 = a1 / b1;
                }
                if (digitCount == DIGITS_OF_PI || mySignalEndProgram)
                {
                    return sb.ToString();
                }
            } while (true);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mySignalEndProgram = true;
            Properties.Settings.Default.Save();
        }

        private void ThrottleTrackBar_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ThrottleValue = ThrottleTrackBar.Value;
        }

    }
}
