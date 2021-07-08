from datetime import datetime as dt



class NonBlockingTimer:
	_t1 = 0
	_t2 = 1
	_dt = 1
	_current_dt_time = 0 
	_current_dt_int = 0 	 
	
	def __init__(self, period : int):
		self._dt = period
		self._t1 = dt.now()
		
	
	def evaluate_elapsed(self, callback): 
		self._t2 = dt.now()
		self._current_dt_time = self._t2 - self._t1
		
		self._current_dt_int = int(self._current_dt_time.total_seconds()*1000) 
		if(self._current_dt_int < self._dt):
			return
		else:
			self._t1 = dt.now()
			callback()
			
			
		
		
		
		
		
