# Server
The server is based on the Raspberry Pi VDI provided by the instructor on the laboratory classes. The back-end is written exclusively in Python and PHP. The execution of necessary server scripts has been accomplished by creating startup routines in the /home/pi./config folder. After turning on the server, the clients can access the measurements at will, referencing the PHP resources, with names corresponding to the desired measurement and its unit. The .php files in the $/measurements$ folder call the python script upon being accessed. The called script is responsible for extracting the data from the SenseHat emulator - it is provided with numerical argument (whose value depends on the measurement type and unit). After geting the data from SenseHat, the script returns it in the form of JSON object, with the following structure:

*{"measurement_name":"name","value": 0.0, "unit":"unit"}*
    
With 0.0 being placeholder of the extracted value. To generate all the measurement-related .php files, a special python script has been used. One exception from this, was the state of the joystick - which is represented as axis/center counters (in the manner identical to the one presented in the laboratory classes) - those files are generated/updated by the script running in the background, that check whether the joystick-pressed event took place, and if so - to update/create the appropriate .json files. 

As can be observed in the figure \ref{fig:server_1}, the client apps can access all the measurement data provided by the SenseHat emulator. The update of the LED matrix on the emulator is realise through the 'led\_display.php' and 'led\_display2.php' (second one is exclusive for the WebApp). Their principle of operation is that they dump the data received and call the python script to update the matrix. The data are of the following type: JSON array of arrays in the format of:

*[[x,y,R,G,B], [x,y,R,G,B]...]*

Where x,y are numerical values denoting index of the LED in the matrix (row, column), and R,G,B are numerical values denoting the contents of the Red, Green and Blue components, consituting the colour of the LED to be set. 
