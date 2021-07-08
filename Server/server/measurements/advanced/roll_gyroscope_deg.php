 <?php
	$arg = ' 43';
	$exec_cmd = 'sudo /home/pi/IoT_scripts/get_measurements.py'.$arg;
	$exec_cmd = escapeshellcmd($exec_cmd);
	$str_out = shell_exec($exec_cmd);
	echo $str_out 
?>