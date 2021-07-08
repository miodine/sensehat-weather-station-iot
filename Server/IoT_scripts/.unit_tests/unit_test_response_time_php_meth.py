#!/usr/bin/python3

from sense_emu import SenseHat
import sys
import json
from datetime import datetime as dt

sense = SenseHat()

t_pre = dt.now()
t_post = 1.0

response_rate = [0]*1000
avg_response_rate = 0



for i in range(1000):
    t_pre = dt.now()
    
    json_fmt = {'name': "temperature", 'value': 0.0, "unit": "Celsius"}
    val = sense.get_temperature()
    json_fmt['value'] = val
    print(json.dumps(json_fmt))
    
    t_post = dt.now() - t_pre
    
    
    response_rate[i] = int(t_post.total_seconds()*1000)
    print(response_rate[i])



