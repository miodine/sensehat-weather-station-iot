#!/usr/bin/python3

from sense_emu import SenseHat

import sys
import getopt
import json



BASIC_MEASUREMENTS_LIST = [
'Temperature', # C idx 0
'Temperature', # F idx 1
'Pressure',    # hPa idx 2
'Pressure',    # mmHg idx 3
'Humidity',	   # % idx 4
'Humidity',    # - idx 5
'Roll',		   # rad idx 6
'Roll',		   # deg idx 7
'Pitch',       # rad idx 8 
'Pitch',	   # deg idx 9
'Yaw',		   # rad idx 10
'Yaw'          # deg idx 11 
]


ADVANCED_MEASUREMENTS_LIST = [
'Roll_acc',					# idx 40
'Pitch_acc',  				# idx 41
'Yaw_acc',    				# idx 42

'Roll_gyro',       			# idx 43
'Pitch_gyro',      			# idx 44
'Yaw_gyro',        			# idx 45

'compass',              	# idx 46
'X_compass',				# idx 47	
'Y_compass',				# idx 48
'Z_compass',            	# idx 49

'X_Acc',        			# idx 410
'Y_Acc',        			# idx 411
'Z_Acc',        			# idx 412

'Temperature_humid', 		# idx 413
'Temperature_press',  		# idx 414
'Induction_x',				# idx 415
'Induction_y',				# idx 416
'Induction_z'				# idx 417
]



sense = SenseHat() 								 
json_fmt = {"name": "", "value": 0.0, "unit": ""}
midx = int(sys.argv[1])





# temperature in celsius
if midx == 0:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_temperature()	
	json_fmt["unit"] = 'C'
	
# temperature in Fahrenheit
elif midx == 1:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_temperature()*1.8 + 32	
	json_fmt["unit"] = 'F'

# pressure in hPa
elif midx == 2:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_pressure()	
	json_fmt["unit"] = 'hPa'
	
# pressure in mmHg
elif midx == 3:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_pressure()*0.75006	
	json_fmt["unit"] = 'mmHg'

# humidity as percentage
elif midx == 4:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_humidity()	
	json_fmt["unit"] = '%'
	
# humidity as fraction
elif midx == 5:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_humidity()/100
	json_fmt["unit"] = '-'

# roll in radians
elif midx == 6:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_radians()['roll']	
	json_fmt["unit"] = 'rad'
	
# roll in degrees
elif midx == 7:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_degrees()['roll']	
	json_fmt["unit"] = 'deg'
	
# pitch in radians
elif midx == 8:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_radians()['pitch']	
	json_fmt["unit"] = 'rad'

# pitch in degrees 
elif midx == 9:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_degrees()['pitch']
	json_fmt["unit"] = 'deg'
	
# yaw in radians
elif midx == 10:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_radians()['yaw']	
	json_fmt["unit"] = 'rad'
	
# yaw in degrees
elif midx == 11:
	json_fmt["name"] = BASIC_MEASUREMENTS_LIST[midx]
	json_fmt["value"] = sense.get_orientation_degrees()['yaw']
	json_fmt["unit"] = 'deg'
	

########################################################

# roll from accelerometer
elif midx == 40:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_accelerometer()['roll']
	json_fmt["unit"] = 'deg'


# pitch from accelerometer degrees
elif midx == 41:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_accelerometer()['pitch']
	json_fmt["unit"] = 'deg'

	
# yaw from accelerometer degrees
elif midx == 42:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_accelerometer()['yaw']
	json_fmt["unit"] = 'deg'

	
# roll from gyroscope degrees
elif midx == 43:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_gyroscope()['roll']
	json_fmt["unit"] = 'deg'


# pitch from gyroscope degrees
elif midx == 44:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_gyroscope()['pitch']
	json_fmt["unit"] = 'deg'

	
	
# yaw from gyroscope degrees
elif midx == 45:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_gyroscope()['yaw']
	json_fmt["unit"] = 'deg'


# compass - dev. from North in degrees
elif midx == 46:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_compass()
	json_fmt["unit"] = 'deg'


# compass - x dir, radians per second
elif midx == 47:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_compass_raw()['x']
	json_fmt["unit"] = 'rps'
	

# compass - y dir, radians per second
elif midx == 48:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_compass_raw()['y']
	json_fmt["unit"] = 'rps'
	
	
# compass - z dir, radians per second
elif midx == 49:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-40] #xD
	json_fmt["value"] = sense.get_compass_raw()['z']
	json_fmt["unit"] = 'rps'

# accelerometer - x dir, Gs
elif midx == 410:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_accelerometer_raw()['x']
	json_fmt["unit"] = 'G'

# accelerometer - y dir, Gs
elif midx == 411:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_accelerometer_raw()['y']
	json_fmt["unit"] = 'G'

# accelerometer - z dir, Gs
elif midx == 412:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_accelerometer_raw()['z']
	json_fmt["unit"] = 'G'

# temperature from humidity sensor, Celsius
elif midx == 413:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_temperature_from_humidity()
	json_fmt["unit"] = 'C'

# temperature from pressure sensor, Celsius
elif midx == 414:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_temperature_from_pressure()
	json_fmt["unit"] = 'C'

# Magnetometer readout - x, uT
elif midx == 415:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_compass_raw()['x']
	json_fmt["unit"] = 'uT'
	
# Magnetometer readout - y, uT
elif midx == 416:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_compass_raw()['y']
	json_fmt["unit"] = 'uT'
	
# Magnetometer readout - z, uT
elif midx == 417:
	json_fmt["name"] = ADVANCED_MEASUREMENTS_LIST[midx-400] #xD
	json_fmt["value"] = sense.get_compass_raw()['z']
	json_fmt["unit"] = 'uT'






print(json.dumps(json_fmt))
	
	
	
