using MultiViewApp.Model;
using System;
using System.ComponentModel;
using System.Windows.Media;
using MultiViewApp.View;

namespace MultiViewApp.ViewModel
{
    public class View4_ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        private readonly Action<string, Color> setColorHandler;

        private LedDisplay ledDisplay;  //!< LED display model
        private IoTServer server;       //!< IoT server model


        private string ipAddress;   //!< IP address
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
        private string _monit = "Monit: "; //!< Message to be displayed
        public string Monit
        {
            get { return _monit; }
            set
            {
                if (_monit != value)
                {
                    _monit = "Monit: " + value;
                    OnPropertyChanged("Monit");
                }
            }
        }


        #endregion Fields

        #region Properties

        public int DisplaySizeX { get => ledDisplay.SizeX; }
        public int DisplaySizeY { get => ledDisplay.SizeY; }
        public Color DisplayOffColor { get => ledDisplay.OffColor; }

        public ButtonCommandWithParameter CommonButtonCommand { get; set; }
        public ButtonCommand SendRequestCommand { get; set; }
        public ButtonCommand SendClearCommand { get; set; }

        private byte _r;
        public int R
        {
            get
            {
                return _r;
            }
            set
            {
                if (_r != (byte)value)
                {
                    _r = (byte)value;
                    ledDisplay.ActiveColorR = _r;
                    SelectedColor = new SolidColorBrush(ledDisplay.ActiveColor);
                    OnPropertyChanged("R");
                }
            }
        }

        private byte _g;
        public int G
        {
            get
            {
                return _g;
            }
            set
            {
                if (_g != (byte)value)
                {
                    _g = (byte)value;
                    ledDisplay.ActiveColorG = _g;
                    SelectedColor = new SolidColorBrush(ledDisplay.ActiveColor);
                    OnPropertyChanged("G");
                }
            }
        }

        private byte _b;
        public int B
        {
            get
            {
                return _b;
            }
            set
            {
                if (_b != (byte)value)
                {
                    _b = (byte)value;
                    ledDisplay.ActiveColorB = _b;
                    SelectedColor = new SolidColorBrush(ledDisplay.ActiveColor);
                    OnPropertyChanged("B");
                }
            }
        }

        private SolidColorBrush _selectedColor;
        public SolidColorBrush SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged("SelectedColor");
                }
            }
        }

        /* serverFiles stores all paths to the files from the server*/
        private ServerFiles serverFiles;
        #endregion Properties

        public View4_ViewModel(Action<String, Color> handler)
        {
            // Initializing default values for the LED display 
            ledDisplay = new LedDisplay(0x00000000);
            SelectedColor = new SolidColorBrush(ledDisplay.ActiveColor);
            setColorHandler = handler;
            // Initialziing buttons 
            CommonButtonCommand = new ButtonCommandWithParameter(SetButtonColor);
            SendRequestCommand = new ButtonCommand(SendControlRequest);
            SendClearCommand = new ButtonCommand(ClearDisplay);
            // Creating a server and setting IP address, file path 
            serverFiles = new ServerFiles();
            ipAddress = MultiViewApp.Properties.Settings.Default.IPaddress;
            server = new IoTServer(IpAddress);
            server.setFilePath(serverFiles.ledDisplay);            
            // Message to be displayd 
            Monit = "LED display control panel initialised!";
        }

        public View4_ViewModel()
        {
            // Initializing default values for the LED display and buttons
            ledDisplay = new LedDisplay(0x00000000);
            SelectedColor = new SolidColorBrush(ledDisplay.ActiveColor);
            CommonButtonCommand = new ButtonCommandWithParameter(SetButtonColor);
            SendRequestCommand = new ButtonCommand(SendControlRequest);
            SendClearCommand = new ButtonCommand(ClearDisplay);
            // Setting IP address, file path 
            serverFiles = new ServerFiles();
            ipAddress = MultiViewApp.Properties.Settings.Default.IPaddress;
            server = new IoTServer(IpAddress);
            server.setFilePath(serverFiles.ledDisplay);
            // Message to be displayd 
            Monit = "LED display control panel initialised!";
        }

        #region Methods

        /**
         * @brief Conversion method: LED indicator Name to LED x-y position
         * @param name LED indicator Button Name propertie 
         * @return Tuple with LED x-y position (0=x, 1=y)
         */

        public (int, int) LedTagToIndex(string name)
        {
            return (int.Parse(name.Substring(3, 1)), int.Parse(name.Substring(4, 1)));
        }

        /**
         * @brief Conversion method: LED x-y position to LED indicator Name
         * @param x LED horizontal position in display
         * @param y LED vertical position in display
         * @return LED indicator Button Name property
         */
        public string LedIndexToTag(int i, int j)
        {
            return "LED" + i.ToString() + j.ToString();
        }

        /**
         * @brief LED indicator Click event handling procedure
         * @param parameter LED indicator Button Name property
         */
        private void SetButtonColor(string parameter)
        {

            /* Set active color as background */
            setColorHandler(parameter, SelectedColor.Color);
            /* Find element x-y position */
            (int x, int y) = LedTagToIndex(parameter);
            /* Update LED display data model */
            ledDisplay.UpdateModel(x, y);

            /* Message to be displayd */
            Monit = "One or more LED Colors were changed (matrix is unsynchronised).";

        }

        /**
         * @brief Clear button Click event handling procedure
         */
        private async void ClearDisplay()
        {
            /* Clear LED display GUI */
            for (int i = 0; i < ledDisplay.SizeX; i++)
                for (int j = 0; j < ledDisplay.SizeY; j++)
                    setColorHandler(LedIndexToTag(i, j), ledDisplay.OffColor);

            /* Clear LED display data model */
            ledDisplay.ClearModel();

            /* Clear physical LED display */
            await server.PostControlRequest(ledDisplay.getClearPostData());

            /* Message to be displayd */
            Monit = "LED matrix values are now set to default (matrix is synchronised).";

        }

        /**
         * @brief Send button Click event handling procedure
         */
        private async void SendControlRequest()
        {
            string error_val = "";

            /* Message to be displayed */
            Monit = "LED matrix values are now being synchronised...";

            error_val = await server.PostControlRequest(ledDisplay.getControlPostData());
            /* Update error */
            if (error_val != null)
            {
                Monit = "Update sent! (matrix is now synchronised)";
            }
            else
            {
                Monit = "Update failed! Check network configuration!";
            }

        }


        #endregion Methods

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

        #endregion PropertyChanged
    }
}
