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
        public ButtonCommand SaveButtonCommand { get; set; }
        public ButtonCommand LoadButtonCommand { get; set; }
        
        private string _setting_1 = "192.168.56.15"; // Server IP
        private string _setting_2 = "500"; // Sample Time for the chart 
        private string _setting_3 = "5"; // xAxisMax max no samples seen on the chart
        private string _setting_4 = "1000"; // Dynamic List refreshment time period ListTime

        private readonly int minSampleTime = 100;


        /**
         * @brief Load user setting to default setting (modified in each session)
         * 
         */
        public void loadDefaultSettings() 
        {
            Setting_1 = Properties.Settings.Default.IPaddress;
            Setting_2 = Properties.Settings.Default.SampleTime;
            Setting_3 = Properties.Settings.Default.xAxisMax;
            Setting_4 = Properties.Settings.Default.ListTime;
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
            Properties.Settings.Default.ListTime = Setting_4;
        }
        

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

        public string Setting_4
        {
            get { return _setting_4; }
            set
            {
                if (_setting_4 != value)
                {
                    _setting_4 = value;
                    OnPropertyChanged("Setting_4");
                }
            }
        }

        public View1_ViewModel()
        {
            SaveButtonCommand = new ButtonCommand(SaveButtonClickHandler);
            LoadButtonCommand = new ButtonCommand(LoadButtonClickHandler);
        }

        public void SaveButtonClickHandler()
        {
            //saveToFile(filePath);
            saveDefaultSettings();


            if (1 != 2) return;
        }
        public void LoadButtonClickHandler()
        {
            //readFromFile(filePath);
            loadDefaultSettings();
            if (1 != 2) return;
        }
    }
}
