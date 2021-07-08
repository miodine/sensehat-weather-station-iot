#!/usr/bin/python3


from sense_emu import SenseHat

import sys
import getopt
import json

class SHCH:
	_json_fmt_template_default = {'name': "", 'value': 0.0, "unit": ""}
	_cnt_c = 0
	_cnt_x = 0
	_cnt_y = 0
	_pressed_flag = False
	
	def __init__(self, server_dir = "/home/pi/server/measurements/"):
		self._server_dir = server_dir
		self._sense = SenseHat()
		self.json_placeholder = {'name': "", 'value': 0.0, "unit": ""}
		
		
	def fmt_template_clear(self):
		self.json_placeholder = self._json_fmt_template_default
		
		
	def dump_joystic_counter_x(self) -> str:
		unit = 'click_pos'
		fname = "joy_counter_x"
		self.json_placeholder['name'] = fname
		self.json_placeholder['value'] = self._cnt_x
		self.json_placeholder['unit'] = unit
		
		file = open(self._server_dir +fname + '_'+ unit+'.json','w')
		file.write(json.dumps(self.json_placeholder))
		file.close()
		self.fmt_template_clear()	
		
	def dump_joystic_counter_y(self) -> str:
		unit = 'click_pos'
		fname = "joy_counter_y"
		self.json_placeholder['name'] = fname
		self.json_placeholder['value'] = self._cnt_y
		self.json_placeholder['unit'] = unit
		
		file = open(self._server_dir +fname + '_'+ unit+'.json','w')
		file.write(json.dumps(self.json_placeholder))
		file.close()
		self.fmt_template_clear()
			
	def dump_joystic_counter_center(self)-> str:
		unit = 'click_pos'
		fname = "joy_counter_c"
		self.json_placeholder['name'] = fname
		self.json_placeholder['value'] = self._cnt_c
		self.json_placeholder['unit'] = unit
		
		file = open(self._server_dir +fname + '_'+ unit+'.json','w')
		file.write(json.dumps(self.json_placeholder))
		file.close()
		self.fmt_template_clear()	
	
	def _evaluate_joystick_event(self):
		def _evaluate_joystick_direction(self, direction : str):
			if direction == "middle":
				self._cnt_c += 1
			elif direction == "right":
				self._cnt_x += 1
			elif direction == "left":
				self._cnt_x -= 1
			elif direction == "up":
				self._cnt_y += 1
			elif direction == "down":
				self._cnt_y -= 1
		
		for event in self._sense.stick.get_events():
			if event.action == "pressed":
				self._pressed_flag = True
				_evaluate_joystick_direction(self, direction = event.direction)
	
	
	def dump_all_to_server(self):
		self._evaluate_joystick_event()
		# joystick
		if self._pressed_flag == True:
			self.dump_joystic_counter_x()
			self.dump_joystic_counter_y()
			self.dump_joystic_counter_center()
			self._pressed_flag = False
		

def main():
	cntrs = SHCH()
	
	while True:
		cntrs.dump_all_to_server()
	
	
	
if __name__ == '__main__':
	main()
	
	
	
