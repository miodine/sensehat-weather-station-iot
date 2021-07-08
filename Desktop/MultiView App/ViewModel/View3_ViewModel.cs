#define CLIENT
#define GET
//#define DYNAMIC

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace MultiViewApp.ViewModel
{
    using Model;
    

    /** 
      * @brief View model for MainWindow.xaml 
      */
    public class View3_ViewModel : BaseViewModel
    {
        #region Properties
        private string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }
        private int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public PlotModel DataPlotModel { get; set; }
        public ButtonCommand StartButton { get; set; }
        public ButtonCommand StopButton { get; set; }
        #endregion

        #region Fields
        private Timer RequestTimer;
        private IoTServer Server;
        // list nr of sample -> its time
        private List<double> sampleCounter = new List<double>();
        // max value on x axis
        private int xAxisMax = 20;
        private int yAxisMax = 1;
        private int yAxisMin = 0;
        private int maxNoDataSeries = 12;
        // for storing data to be showed on the graph
        private ServerFiles serverFiles;
        List<MeasurementModel> chartData;
        bool isButtonStop = false;
        #endregion

        #region Checkboxes fields
        /* variables storing info about checkboxes states */
        private bool rollDeg;
        private bool rollRad;
        private bool pitchDeg;
        private bool pitchRad;
        private bool yawDeg;
        private bool yawRad;
        private bool temperatureC;
        private bool temperatureF;
        private bool humidityPercent;
        private bool humidityNum;
        private bool pressureHpa;
        private bool pressureMmhg;
        #endregion



        public View3_ViewModel()
        {

            xAxisMax = Int32.Parse(Properties.Settings.Default.xAxisMax);
            DataPlotModel = new PlotModel { Title = "Data visualization" };
            
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = xAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = yAxisMin,
                Maximum = yAxisMax,
                Key = "Vertical",
                Unit = "-",
                Title = "value"
            });
            // data series max 12:
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF00FF00")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF005500")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF0000FF")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF000055")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FFFF0000")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF550000")});
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FFff9900") });
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF3366cc") });
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF6611cc") });
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FF669900") });
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FFff4d4d") });
            DataPlotModel.Series.Add(new LineSeries() { Color = OxyColor.Parse("#FFcc33ff") });

            StartButton = new ButtonCommand(StartTimer);
            StopButton = new ButtonCommand(StopTimer);
            serverFiles = new ServerFiles();
            ipAddress = MultiViewApp.Properties.Settings.Default.IPaddress;
            sampleTime = Int32.Parse(MultiViewApp.Properties.Settings.Default.SampleTime);

            Server = new IoTServer(IpAddress);
            // list storing data from server
            chartData = new List<MeasurementModel>();
            CreateEmptyList(ref chartData);



        }


        /**
          *
          *
          */
        private void UpdateSampleList()
        {
            if (sampleCounter.Count == 0)
            {
                // first element on the chart [index] = 0.0 [s]
                // index == 0
                sampleCounter.Add(0.0);
            }
            else
            {
                int lastIndex = sampleCounter.Count - 1; //nr of elements -1 (0 begin)
                double elapsedTime = sampleCounter[lastIndex] + sampleTime / 1000.0;  // stored values in [s]   /1000.0
                sampleCounter.Add(elapsedTime);
            }
        }

        


        /**
          * @brief Time series plot update procedure.
          * @param t X axis data: Time stamp [ms].
          * @param d Y axis data: Real-time measurement [-].
          */
        private void UpdatePlot(double t)
        {
            // data series for RPY values
            //LineSeries rollSeries = DataPlotModel.Series[0] as LineSeries ;
            //LineSeries pitchSeries = DataPlotModel.Series[1] as LineSeries;
            //LineSeries yawSeries = DataPlotModel.Series[2] as LineSeries;
            //DataPlotModel.Series.Add(new LineSeries() { Title = "roll", Color = OxyColor.Parse("#FFFF0000") });

            for (int i = 0; i < maxNoDataSeries; i++)
            {
                
                try
                {
                    
                    if (chartData[i].name != null && !isButtonStop)
                    {
                        (DataPlotModel.Series[i] as LineSeries).Points.Add(new DataPoint(t, chartData[i].value));
                        (DataPlotModel.Series[i] as LineSeries).Title = chartData[i].name + " [" + chartData[i].unit + "]";
                        if (DataPlotModel.Axes[1].Maximum <= chartData[i].value)
                        {
                            DataPlotModel.Axes[1].Maximum = chartData[i].value + chartData[i].value * 0.3;
                        }
                        else if (DataPlotModel.Axes[1].Minimum >= chartData[i].value)
                        {
                            DataPlotModel.Axes[1].Minimum =- DataPlotModel.Axes[1].Maximum * 0.3;
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ADD DATA PLOT ERROR");
                    Debug.WriteLine(e);

                }
                                                
            }

            // xAxisMax = 20
            if (t > xAxisMax && !isButtonStop)
            {
                try
                {
                    // number of data points that need to be deleted 
                    int indexDelete = sampleCounter.FindIndex(x => x >= sampleTime / 1000.0);
                    // new min value of x axis
                    DataPlotModel.Axes[0].Minimum = t - xAxisMax;
                    // new max value of x axis
                    DataPlotModel.Axes[0].Maximum = t;

                    // RemoveRange( start, count )
                    for (int i = 0; i < maxNoDataSeries; i++)
                    {
                        if ((DataPlotModel.Series[i] as LineSeries).Points.Count > 0 && chartData[i].name != null)
                        {
                            (DataPlotModel.Series[i] as LineSeries).Points.RemoveRange(0, indexDelete);
                        }
                    }
                    // update list too
                    sampleCounter.RemoveRange(0, indexDelete);


                }
                catch (Exception e)
                {
                    Debug.WriteLine("ADD DATA PLOT 222 ERROR");
                    Debug.WriteLine(e);
                }


                

            }

            DataPlotModel.InvalidatePlot(true);
        }

        private MeasurementModel Deserialize(string _responseText)
        {
            try
            {
#if DYNAMIC
                dynamic resposneJson = JObject.Parse(responseText);
                UpdatePlot(timeStamp / 1000.0, (double)resposneJson.data);
#else
                MeasurementModel responseJson = JsonConvert.DeserializeObject<MeasurementModel>(_responseText);
                return responseJson;
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine("JSON DATA ERROR");
                Debug.WriteLine(_responseText);
                Debug.WriteLine(e);
                return null;
            }

        }
        private void CreateEmptyList(ref List<MeasurementModel> _list)
        {
            MeasurementModel _model = new MeasurementModel();
            for (int i = 0; i < maxNoDataSeries; i++)
            {
                _list.Add(_model);
            }
        }


        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */
        private async void UpdatePlotWithServerResponse()
        {
#if CLIENT
#if GET
            if (RollDegChecked)
            {
                Server.setFilePath(serverFiles.rollDeg);
                string responseText = await Server.GETwithClient();
                chartData[0] = Deserialize(responseText);
                DataPlotModel.Series[0].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[0] as LineSeries).Points.Clear();
                DataPlotModel.Series[0].IsVisible = false;
            }

            if (RollRadChecked)
            {
                Server.setFilePath(serverFiles.rollRad);
                string responseText = await Server.GETwithClient();
                chartData[1] = Deserialize(responseText);
                DataPlotModel.Series[1].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[1] as LineSeries).Points.Clear();
                DataPlotModel.Series[1].IsVisible = false;
            }

            if (PitchDegChecked)
            {
                Server.setFilePath(serverFiles.pitchDeg);
                string responseText = await Server.GETwithClient();
                chartData[2] = Deserialize(responseText);
                DataPlotModel.Series[2].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[2] as LineSeries).Points.Clear();
                DataPlotModel.Series[2].IsVisible = false;
            }

            if (PitchRadChecked)
            {
                Server.setFilePath(serverFiles.pitchRad);
                string responseText = await Server.GETwithClient();
                chartData[3] = Deserialize(responseText);
                DataPlotModel.Series[3].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[3] as LineSeries).Points.Clear();
                DataPlotModel.Series[3].IsVisible = false;
            }

            if (YawDegChecked)
            {
                Server.setFilePath(serverFiles.yawDeg);
                string responseText = await Server.GETwithClient();
                chartData[4] = Deserialize(responseText);
                DataPlotModel.Series[4].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[4] as LineSeries).Points.Clear();
                DataPlotModel.Series[4].IsVisible = false;
            }

            if (YawRadChecked)
            {
                Server.setFilePath(serverFiles.yawRad);
                string responseText = await Server.GETwithClient();
                chartData[5] = Deserialize(responseText);
                DataPlotModel.Series[5].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[5] as LineSeries).Points.Clear();
                DataPlotModel.Series[5].IsVisible = false;
            }

            if (TemperatureCChecked)
            {
                Server.setFilePath(serverFiles.tempC);
                string responseText = await Server.GETwithClient();
                chartData[6] = Deserialize(responseText);
                DataPlotModel.Series[6].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[6] as LineSeries).Points.Clear();
                DataPlotModel.Series[6].IsVisible = false;
            }

            if (TemperatureFChecked)
            {
                Server.setFilePath(serverFiles.tempF);
                string responseText = await Server.GETwithClient();
                chartData[7] = Deserialize(responseText);
                DataPlotModel.Series[7].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[7] as LineSeries).Points.Clear();
                DataPlotModel.Series[7].IsVisible = false;
            }

            if (HumidityNumChecked)
            {
                Server.setFilePath(serverFiles.humidNum);
                string responseText = await Server.GETwithClient();
                chartData[8] = Deserialize(responseText);
                DataPlotModel.Series[8].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[8] as LineSeries).Points.Clear();
                DataPlotModel.Series[8].IsVisible = false;
            }

            if (HumidityPercentChecked)
            {
                Server.setFilePath(serverFiles.humidPer);
                string responseText = await Server.GETwithClient();
                chartData[9] = Deserialize(responseText);
                DataPlotModel.Series[9].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[9] as LineSeries).Points.Clear();
                DataPlotModel.Series[9].IsVisible = false;
            }

            if (PressureHpaChecked)
            {
                Server.setFilePath(serverFiles.pressHpa);
                string responseText = await Server.GETwithClient();
                chartData[10] = Deserialize(responseText);
                DataPlotModel.Series[10].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[10] as LineSeries).Points.Clear();
                DataPlotModel.Series[10].IsVisible = false;
            }

            if (PressureMmhgChecked)
            {
                Server.setFilePath(serverFiles.pressMmhg);
                string responseText = await Server.GETwithClient();
                chartData[11] = Deserialize(responseText);
                DataPlotModel.Series[11].IsVisible = true;
            }
            else
            {
                (DataPlotModel.Series[11] as LineSeries).Points.Clear();
                DataPlotModel.Series[11].IsVisible = false;
            }

#else
            string responseText = await Server.POSTwithClient();
#endif
#else
#if GET
            string responseText = await Server.GETwithRequest();
#else
            string responseText = await Server.POSTwithRequest();
#endif
#endif

            // first call creates sampleCounter[0] = 0.0
            UpdateSampleList();
            // sampleCunter[lastIndex] =  time stamp
            int lastIndex = sampleCounter.Count - 1;
            UpdatePlot(sampleCounter[lastIndex]);

        }

        /**
          * @brief Synchronous procedure for request queries to the IoT server.
          * @param sender Source of the event: RequestTimer.
          * @param e An System.Timers.ElapsedEventArgs object that contains the event data.
          */
        private void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlotWithServerResponse();
            
            //MessageBox.Show(string.Format("State:{0}",checkChange));
            
        }


        #region ButtonCommands

        /**
         * @brief RequestTimer start procedure.
         */
        private void StartTimer()
        {
            if (RequestTimer == null)
            {
                isButtonStop = false;
                RequestTimer = new Timer(sampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;
                
                DataPlotModel.ResetAllAxes();
            }
        }

        /**
         * @brief RequestTimer stop procedure.
         */
        private void StopTimer()
        {
            if (RequestTimer != null)
            {
                isButtonStop = true;
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
        }



        #endregion

        #region Checkboxes methods
        /**
         * @brief Checkboxes region: public methods to access private fields from region Checkboxes fields
         */
        public bool RollDegChecked
        {
            get { return rollDeg; }
            set
            {
                if (rollDeg != value)
                {
                    StopTimer();
                    rollDeg = value;
                    OnPropertyChanged("RollDegChecked");
                }
            }
        }

        public bool RollRadChecked
        {
            get { return rollRad; }
            set
            {
                if (rollRad != value)
                {                   
                    StopTimer();
                    rollRad = value;
                    OnPropertyChanged("RollRadChecked");
                }
            }
        }

        public bool PitchDegChecked
        {
            get { return pitchDeg; }
            set
            {
                if (pitchDeg != value)
                {                  
                    StopTimer();
                    pitchDeg = value;
                    OnPropertyChanged("PitchDegChecked");
                }
            }
        }

        public bool PitchRadChecked
        {
            get { return pitchRad; }
            set
            {
                if (pitchRad != value)
                {
                    StopTimer();
                    pitchRad = value;
                    OnPropertyChanged("PitchRadChecked");
                }
            }
        }

        public bool YawDegChecked
        {
            get { return yawDeg; }
            set
            {
                if (yawDeg != value)
                {
                    StopTimer();
                    yawDeg = value;
                    OnPropertyChanged("YawDegChecked");
                }
            }
        }

        public bool YawRadChecked
        {
            get { return yawRad; }
            set
            {
                if (yawRad != value)
                {
                    StopTimer();
                    yawRad = value;
                    OnPropertyChanged("YawRadChecked");
                }
            }
        }
        public bool PressureMmhgChecked
        {
            get { return pressureMmhg; }
            set
            {
                if (pressureMmhg != value)
                {
                    StopTimer();
                    pressureMmhg = value;
                    OnPropertyChanged("PressureMmhgChecked");
                }
            }
        }
        public bool PressureHpaChecked
        {
            get { return pressureHpa; }
            set
            {
                if (pressureHpa != value)
                {
                    StopTimer();
                    pressureHpa = value;
                    OnPropertyChanged("PressureHpaChecked");
                }
            }
        }
        public bool HumidityNumChecked
        {
            get { return humidityNum; }
            set
            {
                if (humidityNum != value)
                {
                    StopTimer();
                    humidityNum = value;
                    OnPropertyChanged("HumidityNumChecked");
                }
            }
        }
        public bool HumidityPercentChecked
        {
            get { return humidityPercent; }
            set
            {
                if (humidityPercent != value)
                {
                    StopTimer();
                    humidityPercent = value;
                    OnPropertyChanged("HumidityPercentChecked");
                }
            }
        }
        public bool TemperatureFChecked
        {
            get { return temperatureF; }
            set
            {
                if(temperatureF != value)
                {
                    StopTimer();
                    temperatureF = value;
                    OnPropertyChanged("TemperatureFChecked");
                }
            }      
        }

        public bool TemperatureCChecked
        {
            get { return temperatureC; }
            set
            {
                if (temperatureC != value)
                {
                    StopTimer();
                    temperatureC = value;
                    OnPropertyChanged("TemperatureCChecked");
                }
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * @brief Simple function to trigger event handler
         * @params propertyName Name of ViewModel property as string
         */
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

