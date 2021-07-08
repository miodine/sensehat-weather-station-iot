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
        #region Properties
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
        #endregion
        #region Fields
        private IoTServer Server;
        private ServerFiles serverFiles;

        private Timer RequestTimer;
        private int sampleTime;

        private string responseText;
        private MeasurementModel responseJson;

        private List<MeasurementModel> measurementList;
        #endregion

        public View2_ViewModel()
        {
            // Create new collection for measurements data
            Measurements = new ObservableCollection<MeasurementViewModel>();
            // Bind button with action
            Refresh = new ButtonCommand(GetServerResponse);
            // Create a member with all file paths for obtaing data
            serverFiles = new ServerFiles();
            // Create a server and set specified IP address
            ipAddress = MultiViewApp.Properties.Settings.Default.IPaddress;
            Server = new IoTServer(IpAddress);
        }
        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */
        private async void GetServerResponse()
        {
            // Clear previous measurements 
            Measurements.Clear();
            // Disable Refresh button until the data is obtained
            Refresh.IsEnabled = false;

            // The data is being obtained one by one (from independent files)
            // The process: set new file path -> wait for server reponse -> deserialize the response -> add data to Measurements list
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
            // Enable again Refresh button
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