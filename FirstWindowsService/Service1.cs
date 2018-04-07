using System.ServiceProcess;
using System.Timers;

namespace FirstWindowsService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service1"/> class.
        /// </summary>
        public Service1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the operating system starts (for a service that starts automatically). 
        /// Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 5000;                      // Execute every 5s
            timer.Elapsed += TimerTick;                 // Event timer_Tick run
            timer.Enabled = true;                        // Enable timer
            // Write lo to file and to Windows event log
            Utilities.WriteLogErrorToFile("Test for 1st run Windows Service");
            Utilities.WriteLogErrorToEventLog("Test for 1st run Windows Service", 1);
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void TimerTick(object sender, ElapsedEventArgs args)
        {
            Utilities.WriteLogErrorToFile("Timer has ticked");
        }

        /// <summary>
        /// When service stops running.
        /// </summary>
        protected override void OnStop()
        {
            timer.Enabled = true;
            Utilities.WriteLogErrorToFile("Windows Service has been stop");
            Utilities.WriteLogErrorToEventLog("Windows Service has been stop", 1);
        }
    }
}
