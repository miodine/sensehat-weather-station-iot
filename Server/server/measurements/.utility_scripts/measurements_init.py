import json

INIT_LIST = [
'temperature_C',   # C idx 0
'temperature_F',   # F idx 1
'pressure_hPa',    # hPa idx 2
'pressure_mmHg',   # mmHg idx 3
'humidity_%',	   # % idx 4
'humidity_-',      # - idx 5
'roll_rad',		   # rad idx 6
'roll_deg',		   # deg idx 7
'pitch_rad',       # rad idx 8 
'pitch_deg',	   # deg idx 9
'yaw_rad',		   # rad idx 10
'yaw_deg'          # deg idx 11 
]

INIT_LIST_ADVANCED = [
'roll_accelerometer_deg',	# idx 40
'pitch_accelerometer_deg',  # idx 41
'yaw_accelerometer_deg',    # idx 42

'roll_gyroscope_deg',       # idx 43
'pitch_gyroscope_deg',      # idx 44
'yaw_gyroscope_deg',        # idx 45

'compass_deg',              # idx 46
'compass_x_rps',			# idx 47	
'compass_y_rps',			# idx 48
'compass_z_rps',            # idx 49

'accelerometer_x_G',        # idx 410
'accelerometer_y_G',        # idx 411
'accelerometer_z_G',        # idx 412

'temperature_from_humidity_C', # idx 413
'temperature_from_pressure_C',  # idx 414

'induction_x_uT', 			# idx 415
'induction_y_uT',			# idx 416
'induction_z_uT'			# idx 417
]

PHP_1 = ''' <?php
	$arg = ' '''
	
PHP_2 = '''';
	$exec_cmd = 'sudo /home/pi/IoT_scripts/get_measurements.py'.$arg;
	$exec_cmd = escapeshellcmd($exec_cmd);
	$str_out = shell_exec($exec_cmd);
	echo $str_out 
?>'''

fnames = []

for i in range(len(INIT_LIST)):
	fname = INIT_LIST[i]+'.php'
	fnames.append(fname)
	
	file = open(fname,'w')
	file.write(PHP_1 +str(i) +PHP_2)
	file.close()

for i in range(len(INIT_LIST_ADVANCED)):
	fname = 'advanced/'+INIT_LIST_ADVANCED[i]+'.php'
	fnames.append(fname)
	
	file = open(fname,'w')
	file.write(PHP_1 + '4'+ str(i) +PHP_2)
	file.close()

file = open('_resource_names.json','w')
file.write(json.dumps(fnames))
file.close()

print(fnames)




