using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiViewApp.ViewModel
{
    using Model;
    /**
     * MeasurementViewModel class
     * @brief: methods used for binding MeasurementModel data with proper fields in the View2.xaml file
     */
    public class MeasurementViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged("name");
                }
            }
        }
        
        private double _value;
        public string value
        {
            get
            {
                return _value.ToString("0.0####", CultureInfo.InvariantCulture);
            }
            set
            {
                if (Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double tmp) && _value != tmp)
                {
                    _value = tmp;
                    OnPropertyChanged("value");
                }
            }
        }

        private string _unit;
        public string unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged("unit");
                }
            }
        }

        public MeasurementViewModel(MeasurementModel model)
        {
            UpdateWithModel(model);
        }

        public void UpdateWithModel(MeasurementModel model)
        {
            _name = model.name;
            OnPropertyChanged("name");
            _value = model.value;
            OnPropertyChanged("value");
            _unit = model.unit;
            OnPropertyChanged("unit");
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
