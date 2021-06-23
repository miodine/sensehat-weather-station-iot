#!/usr/bin/python3

from sense_emu import SenseHat
import json 

sense = SenseHat()

matrix_file = open('/home/pi/IoT_scripts/led_display.json')
matrix_state = json.load(matrix_file)


if not matrix_state:
	sense.clear()
	
	
else:
	for led in matrix_state:
		sense.set_pixel(led[0],led[1],led[2],led[3],led[4])

matrix_file.close()
