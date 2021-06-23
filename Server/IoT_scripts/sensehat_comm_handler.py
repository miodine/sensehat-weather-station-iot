from sense_emu import SenseHat
import json


class SHCH:
    _json_fmt_template_default = {'name': "", 'value': 0.0, "unit": ""}
    _cnt_c = 0
    _cnt_x = 0
    _cnt_y = 0

    def __init__(self, server_dir="../server/measurements/"):
        self._server_dir = server_dir
        self._sense = SenseHat()
        self.json_placeholder = {'name': "", 'value': 0.0, "unit": ""}

    def fmt_template_clear(self):
        self.json_placeholder = self._json_fmt_template_default

    def dump_temperature_json(self, unit: str) -> str:
        fname = "temperature"
        self.json_placeholder['name'] = fname

        value = self._sense.get_temperature()

        if(unit == "F"):
            value = value*1.8 + 32
            self.json_placeholder['unit'] = unit
        else:
            self.json_placeholder['unit'] = "C"

        self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_pressure_json(self, unit: str) -> str:
        fname = "pressure"
        self.json_placeholder['name'] = fname

        value = self._sense.get_pressure()

        if(unit == "mmHg"):
            value = value*0.75006
            self.json_placeholder['unit'] = unit
        else:
            self.json_placeholder['unit'] = "hPa"

        self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_humidity_json(self, unit: str) -> str:
        fname = "humidity"
        self.json_placeholder['name'] = fname

        value = self._sense.get_humidity()

        if(unit == "-"):
            value = value/100
            self.json_placeholder['unit'] = unit
        else:
            self.json_placeholder['unit'] = "%"

        self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_roll_json(self, unit: str) -> str:
        fname = "roll"

        self.json_placeholder['name'] = fname

        if(unit == "rad"):
            value = self._sense.get_orientation_radians()['roll']
            self.json_placeholder['unit'] = unit
            self.json_placeholder['value'] = value
        else:
            value = self._sense.get_orientation_degrees()['roll']
            self.json_placeholder['unit'] = "deg"
            self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_pitch_json(self, unit: str) -> str:
        fname = "pitch"
        self.json_placeholder['name'] = fname

        if(unit == "rad"):
            value = self._sense.get_orientation_radians()['pitch']
            self.json_placeholder['unit'] = unit
            self.json_placeholder['value'] = value
        else:
            value = self._sense.get_orientation_degrees()['pitch']
            self.json_placeholder['unit'] = "deg"
            self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_yaw_json(self, unit: str) -> str:
        fname = "yaw"
        self.json_placeholder['name'] = fname

        if(unit == "rad"):
            value = self._sense.get_orientation_radians()['yaw']
            self.json_placeholder['unit'] = unit
            self.json_placeholder['value'] = value
        else:
            value = self._sense.get_orientation_degrees()['yaw']
            self.json_placeholder['unit'] = "deg"
            self.json_placeholder['value'] = value

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_joystic_counter_x(self) -> str:
        unit = 'click_pos'
        fname = "joy_counter_x"
        self.json_placeholder['name'] = fname
        self.json_placeholder['value'] = self._cnt_x
        self.json_placeholder['unit'] = unit

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_joystic_counter_y(self) -> str:
        unit = 'click_pos'
        fname = "joy_counter_y"
        self.json_placeholder['name'] = fname
        self.json_placeholder['value'] = self._cnt_y
        self.json_placeholder['unit'] = unit

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def dump_joystic_counter_center(self) -> str:
        unit = 'click_pos'
        fname = "joy_counter_c"
        self.json_placeholder['name'] = fname
        self.json_placeholder['value'] = self._cnt_c
        self.json_placeholder['unit'] = unit

        file = open(self._server_dir + fname + '_' + unit+'.json', 'w')
        file.write(json.dumps(self.json_placeholder))
        file.close()
        self.fmt_template_clear()

    def _evaluate_joystick_event(self):
        def _evaluate_joystick_direction(self, direction: str):
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
                _evaluate_joystick_direction(self, direction=event.direction)

    def dump_all_to_server(self):
        self._evaluate_joystick_event()
        # joystick
        self.dump_joystic_counter_x()
        self.dump_joystic_counter_y()
        self.dump_joystic_counter_center()

        # first units
        self.dump_roll_json("rad")
        self.dump_pitch_json("rad")
        self.dump_yaw_json("rad")
        self.dump_temperature_json("C")
        self.dump_humidity_json("-")
        self.dump_pressure_json("mmHg")

        # second units
        self.dump_roll_json("deg")
        self.dump_pitch_json("deg")
        self.dump_yaw_json("deg")
        self.dump_temperature_json("F")
        self.dump_humidity_json("%")
        self.dump_pressure_json("hPa")
