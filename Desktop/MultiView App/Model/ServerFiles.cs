namespace MultiViewApp.Model
{
    /**
     * class ServerFiles: stores files paths for obtaining data from the server
     */
    class ServerFiles
    {
        /**
         * @brief: Constructor initializes all the file paths
         */
        public ServerFiles()
        {
            ledDisplay = "/led_display.php";
            rollDeg = "/measurements/roll_deg.php";
            rollRad = "/measurements/roll_rad.php";
            pitchDeg = "/measurements/pitch_deg.php";
            pitchRad = "/measurements/pitch_rad.php";
            yawDeg = "/measurements/yaw_deg.php";
            yawRad = "/measurements/yaw_rad.php";
            humidNum = "/measurements/humidity_-.php";
            humidPer = "/measurements/humidity_%.php";
            pressHpa = "/measurements/pressure_hPa.php";
            pressMmhg = "/measurements/pressure_mmHg.php";
            tempC = "/measurements/temperature_C.php";
            tempF = "/measurements/temperature_F.php";
            accelX = "/measurements/advanced/accelerometer_x_G.php";
            accelY = "/measurements/advanced/accelerometer_y_G.php";
            accelZ = "/measurements/advanced/accelerometer_z_G.php";
            compassDeg = "/measurements/advanced/compass_deg.php";
            compassX = "/measurements/advanced/compass_x_rps.php";
            compassY = "/measurements/advanced/compass_y_rps.php";
            compassZ = "/measurements/advanced/compass_x_rps.php";
            inducX = "/measurements/advanced/induction_x_uT.php";
            inducY = "/measurements/advanced/induction_y_uT.php";
            inducZ = "/measurements/advanced/induction_z_uT.php";
            pitchAccel = "/measurements/advanced/pitch_accelerometer_deg.php";
            pitchGyro = "/measurements/advanced/pitch_gyroscope_deg.php";
            rollAccel = "/measurements/advanced/roll_accelerometer_deg.php";
            rollGyro = "/measurements/advanced/roll_gyroscope_deg.php";
            yawAccel = "/measurements/advanced/yaw_accelerometer_deg.php";
            yawGyro = "/measurements/advanced/yaw_gyroscope_deg.php";
            tempHumid = "/measurements/advanced/temperature_from_humidity_C.php";
            tempPress = "/measurements/advanced/temperature_from_pressure_C.php";
            joystickC = "/measurements/joy_counter_c_click_pos.json";
            joystickX = "/measurements/joy_counter_x_click_pos.json";
            joystickY = "/measurements/joy_counter_y_click_pos.json";
        }

        #region Fields File Paths
        public string ledDisplay { get; }
        public string rollDeg { get; }
        public string pitchDeg { get; }
        public string yawDeg { get; }
        public string rollRad { get; }
        public string pitchRad { get; }
        public string yawRad { get; }
        public string tempC { get; }
        public string tempF { get; }
        public string humidPer { get; }
        public string humidNum { get; }
        public string pressHpa { get; }
        public string pressMmhg { get; }
        public string accelX { get; }
        public string accelY { get; }
        public string accelZ { get; }
        public string compassDeg { get; }
        public string compassX { get; }
        public string compassY { get; }
        public string compassZ { get; }
        public string inducX { get; }
        public string inducY { get; }
        public string inducZ { get; }
        public string pitchAccel { get; }
        public string pitchGyro { get; }
        public string rollAccel { get; }
        public string rollGyro { get; }
        public string yawAccel { get; }
        public string yawGyro { get; }
        public string tempPress { get; }
        public string tempHumid { get; }
        public string joystickC { get; }
        public string joystickX { get; }
        public string joystickY { get; }
        #endregion
    }
}
