#!/usr/bin/python3

from sense_emu import SenseHat
import sys
import json
from datetime import datetime as dt
from non_blocking_timer import NonBlockingTimer 
from sensehat_comm_handler import SHCH

MEASUREMENT_REFRESH_D_TIME = 0
nbt = NonBlockingTimer(MEASUREMENT_REFRESH_D_TIME)
shch = SHCH()

t_pre = dt.now()
t_post = 1.0

while(True):
    t_pre = dt.now()
    shch.dump_all_to_server()
    t_post = dt.now() - t_pre
    print(int(t_post.total_seconds()*1000))
