using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace MultiViewApp.ViewModel
{
    using Model;
    class View1_ViewModel : BaseViewModel
    {
        #region Properties
        public ButtonCommand SaveButtonCommand { get; set; }
        public ButtonCommand LoadButtonCommand { get; set; }
        #endregion
        #region Fields
        private string _setting_1 = "192.168.56.15"; //<- Server IP
        private string _setting_2 = "500";           //<- Sample Time for the chart 
        private string _setting_3 = "5";             //<- xAxisMax max time seen on the chart

        private readonly int minSampleTime = 100;    //<- min sample time 
        #endregion

        /**
         * @brief Load user setting to default setting (modified in each session)
         * 
         */
        public void loadDefaultSettings() 
        {
            Setting_1 = Properties.Settings.Default.IPaddress;
            Setting_2 = Properties.Settings.Default.SampleTime;
            Setting_3 = Properties.Settings.Default.xAxisMax;
        }
        /**
         * @brief Save user setting from default setting (modified in each session)
         * 
         */
        public void saveDefaultSettings()
        {
            if (Int32.Parse(Setting_2) < minSampleTime)
            {
                MessageBox.Show("It is advisable to display at most one quantity when sample time < 100 ms.","Warning!");
                Properties.Settings.Default.SampleTime = Setting_2;
            }
            Properties.Settings.Default.IPaddress = Setting_1;           
            Properties.Settings.Default.xAxisMax = Setting_3;
        }

        #region Public methods 
        public string Setting_1
        {
            get { return _setting_1; }
            set
            {
                if (_setting_1 != value)
                {
                    _setting_1 = value;
                    OnPropertyChanged("Setting_1");
                }
            }
        }

        public string Setting_2
        {
            get { return _setting_2; }
            set
            {
                if (_setting_2 != value)
                {
                    _setting_2 = value;
                    OnPropertyChanged("Setting_2");
                }
            }
        }

        public string Setting_3
        {
            get { return _setting_3; }
            set
            {
                if (_setting_3 != value)
                {
                    _setting_3 = value;
                    OnPropertyChanged("Setting_3");
                }
            }
        }
        #endregion

        public View1_ViewModel()
        {
            SaveButtonCommand = new ButtonCommand(SaveButtonClickHandler);
            LoadButtonCommand = new ButtonCommand(LoadButtonClickHandler);
        }

        #region Button Click Handlers
        public void SaveButtonClickHandler()
        {
            saveDefaultSettings();
            if (1 != 2) return;
        }
        public void LoadButtonClickHandler()
        {
            loadDefaultSettings();
            if (1 != 2) return;
        }
        #endregion
    }
}
