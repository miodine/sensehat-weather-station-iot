#define CLIENT
#define GET

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System;
using Newtonsoft.Json;
namespace MultiViewApp.ViewModel
{
    using Model;
    using Newtonsoft.Json.Linq;

    public class View2_ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<MeasurementViewModel> Measurements { get; set; }
        public ButtonCommand Refresh { get; set; }
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


        private ServerIoTmock ServerMock = new ServerIoTmock();
        private IoTServer Server;
        private ServerFiles serverFiles;

        private Timer RequestTimer;
        private int sampleTime;

        private string responseText;
        private MeasurementModel responseJson;
        int counter = 0;
        bool first;

        private List<MeasurementModel> measurementList;

        public View2_ViewModel()
        {
            // Create new collection for measurements data
            Measurements = new ObservableCollection<MeasurementViewModel>();
            sampleTime = 4000;
            // Bind button with action
            Refresh = new ButtonCommand(GetServerResponse);

            serverFiles = new ServerFiles();
            ipAddress = MultiViewApp.Properties.Settings.Default.IPaddress;
            Server = new IoTServer(IpAddress);
            measurementList = new List<MeasurementModel>();
            first = true;
        }

        private void addMeasurement() 
        {
            if (!first)
            {
                Measurements[counter] = new MeasurementViewModel(responseJson);                               
            }
            else
            {
                Measurements.Add(new MeasurementViewModel(responseJson));
            }
            counter++;
        }
        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */
        private async void GetServerResponse()
        {
            Measurements.Clear();
            Refresh.IsEnabled = false;
#if CLIENT
#if GET
            Server.setFilePath(serverFiles.rollDeg);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.rollRad);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.rollAccel);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            
            Server.setFilePath(serverFiles.pitchDeg);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.pitchRad);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.pitchAccel);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.pitchGyro);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.yawDeg);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.yawRad);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.yawAccel);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.yawGyro);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.joystickC);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.joystickX);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.joystickY);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.tempC);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.tempF);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.tempHumid);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.tempPress);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.pressHpa);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.pressMmhg);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.humidPer);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.humidNum);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.accelX);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.accelY);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.accelZ);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.inducX);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.inducY);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.inducZ);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

            Server.setFilePath(serverFiles.compassDeg);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson)); Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.compassX);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.compassY);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));
            Server.setFilePath(serverFiles.compassZ);
            responseText = await Server.GETwithClient();
            responseJson = JsonConvert.DeserializeObject<MeasurementModel>(responseText);
            Measurements.Add(new MeasurementViewModel(responseJson));

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
            Refresh.IsEnabled = true;
        }


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