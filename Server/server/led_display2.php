<?php

/**
 ******************************************************************************
 * @file    LED update handling
 * @author  Adrian Wojcik
 * @code thief Keshav sharma
 * @version V1.1
 * @date    09-May-2020
 * @brief   
 ******************************************************************************
 */

include 'led_display_model.php';
$dispControlFile = '../IoT_scripts/led_display.json';

switch($_SERVER['REQUEST_METHOD']) {
  
  case "GET": 
  // Parse input data and save as JSON array to text file
    $disp = new LedDisplay();
    file_put_contents($dispControlFile, $disp->postDataToJsonArray($_GET));
    
    echo file_get_contents($dispControlFile);
    break;
  
  case "POST": 
  // Parse input data and save as JSON array to text file
    $var = $_POST['ids'];
    file_put_contents($dispControlFile,$var);
    echo json_encode($var);
    break;

  case "PUT": 
  // Save input JSON array directly to text file
    file_put_contents($dispControlFile, file_get_contents('php://input'));
    echo '["ACK"]';
    break;
}

$exec_cmd = 'sudo /home/pi/IoT_scripts/update_matrix.py';
$cmd_in = escapeshellcmd($exec_cmd);
$resp = exec($cmd_in);
echo $resp
?>
