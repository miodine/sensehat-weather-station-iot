var sampleTime=1000;					// Sample time [ms]
var sampleTimeSec = sampleTime / 1000;	// Sample time [s]
var maxStoredSamples=1000;				// Maximum number of stored samples
var inputSampleTime;



var serverIP='';
var timeVec;	
var lastTimeStamp;	




var Vec1;		
var Vec2;	
var Vec3;	


	

		
var chart;	
var chartContext;

var timer;				// Request timer

var chart_name;
var unit;
var chartYLabel='Nan';

var label1 ='Nan';
var label2;
var label3;


var url;
var url1;
var url2;
var url3;

var numberOfValue=1; //default         // for determining how many parameters in one graph

const urlget='http://192.168.56.15/web_app/config.json';

function selectChart(){
    
   
    
    chart_name=document.getElementById("chartName").value;
    unit=document.getElementById("unit").value;
    
    //temperature
    if(chart_name==='1')
    { numberOfValue=3
         console.log( "temp" );
         label1='Temperature';
         label2='Temperature from Humidity sensor';
         label3='Temperature from Pressure sensor';
        if(unit==='0') // C
        { console.log( "c" );
          url1 = 'http://192.168.56.15/measurements/temperature_C.php';	
          url2 = 'http://192.168.56.15/measurements/advanced/temperature_from_humidity_C.php';		
          url3 = 'http://192.168.56.15/measurements/advanced/temperature_from_pressure_C.php';	
            
            
            chartYLabel='Temperature[C]';
        }
       
        if(unit==='1') // F
        {
          url1 = 'http://192.168.56.15/measurements/temperature_F.php';	
          url2 = 'http://192.168.56.15/measurements/temperature_F.php';		
          url3 = 'http://192.168.56.15/measurements/temperature_F.php';	
          chartYLabel='Temperature[F]';
            
        }
    }
    
    
    
    //pressure
    if(chart_name==='2')
    { numberOfValue=1
        label1='Pressure';
         
        if(unit==='0') // hPa
        {
          url = 'http://192.168.56.15/measurements/pressure_hPa.php';	
         
            chartYLabel='Pressure[hPa]';
        }
       
        if(unit==='1') // mmHg
        {
          url = 'http://192.168.56.15/measurements/pressure_mmHg.php';	
          chartYLabel='Pressure[mmHg]';
        }
    }
    
    
    
    //humidity
    if(chart_name==='3')
    { numberOfValue=1
         label1='Humdity';
        if(unit==='0') // %
        {
          url = 'http://192.168.56.15/measurements/humidity_%.php';	
          chartYLabel='Humidity[%]';
            
        }
       
        if(unit==='1') // -
        {
          url = 'http://192.168.56.15/measurements/humidity_-.php';
          chartYLabel='Humidity[%]';
        }
    }
    
    // Orientation
    if(chart_name==='4')
    { numberOfValue=3
          label1='ROLL';
          label2='PITCH';
          label3='YAW';
          
          if(unit==='0') // DEG
        {
            url1 = 'http://192.168.56.15/measurements/roll_deg.php';
            url2 = 'http://192.168.56.15/measurements/pitch_deg.php';
            url3 = 'http://192.168.56.15/measurements/yaw_deg.php';
          chartYLabel='oientation[Deg]';
            
        }
           if(unit==='1') // rad
        {
            url1 = 'http://192.168.56.15/measurements/roll_rad.php';
            url2 = 'http://192.168.56.15/measurements/pitch_rad.php';
            url3 = 'http://192.168.56.15/measurements/yaw_rad.php';
            chartYLabel='Orientation[Rad]';
            
        }
         
       
    }
    
    
    
    // Accelerometer
    if(chart_name==='5')
    { numberOfValue=3
         //Deg
          label1='ROLL';
          label2='PITCH';
          label3='YAW';
         url1 = 'http://192.168.56.15/measurements/advanced/roll_accelerometer_deg.php';
         url2 = 'http://192.168.56.15/measurements/advanced/pitch_accelerometer_deg.php';	
         url3 = 'http://192.168.56.15/measurements/advanced/yaw_accelerometer_deg.php';	
            
            chartYLabel='Accelerometer[Deg]';
        
    }
    
     // Gyroscope
    if(chart_name==='6')
    { numberOfValue=3
         //Deg
          label1='ROLL';
          label2='PITCH';
          label3='YAW';
         url1 ='http://192.168.56.15/measurements/advanced/roll_gyroscope_deg.php';
         url2 ='http://192.168.56.15/measurements/advanced/pitch_gyroscope_deg.php';
         url3 ='http://192.168.56.15/measurements/advanced/yaw_gyroscope_deg.php';
            
            chartYLabel='Gyroscope[Deg]';
        
    }
     stopTimer();
    chart.clear();
    chartInit();
    chart.update();
    console.log( "graph generate" );
}











/**
    * @brief Add new value to next data point
    * @param data object with received data
*/
function addData(data1,data2,data3){
	
    
	if(Vec1.length > maxStoredSamples){
		removeOldData();
		lastTimeStamp+= sampleTimeSec;
		timeVec.push(lastTimeStamp.toFixed(3));
    }
    
    if(numberOfValue===1){
      Vec1.push(data1);
     
    }
    
    
    if(numberOfValue===3){
     Vec1.push(data1);
	 Vec2.push(data2);
     Vec3.push(data3);
     
    }
	
    
    chart.update();
    
    
  
    
	
	
}


/**
    * @brief Call initialization of graphs if not initailized
*/


/**
    * @brief Remove oldest data point
*/
function removeOldData(){
    timeVec.splice(0,1);
    
    if(numberOfValue===1){
      Vec1.splice(0,1);
    }
    
    
    if(numberOfValue===3){
     Vec1.splice(0,1);
     Vec2.splice(0,1);
     Vec3.splice(0,1);
    }
    
	console.log( "REMOVE DONE" );
      
}

/**
    * @brief Start request timer
*/
function startTimer(){
	
        if(timer == null)
		timer = setInterval(ajaxGetJSON, sampleTime);
    
}

/**
    * @brief Stop request timer
*/
function stopTimer(){
	    
		if(timer != null) {
		clearInterval(timer);
		timer = null;
	}
	
       
    
}

/**
    * @brief Send HTTP GET request to IoT server
*/
function ajaxGetJSON(){
   
    if(numberOfValue===1){
     var data1;
    var data2;
    var data3;
    $.ajax({
     url: url, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {
       data1 = +responseJSON.value;  
      addData(data1,data2,data3);
    }
    });
    }
    
    
    if(numberOfValue===3){
     var data1;
    var data2;
    var data3;
   
    $.when(
    $.ajax({ // 1 Request
        url: url1, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data1 = +responseJSON.value;                  
        }           
    }),

    $.ajax({ //2 Request
        url: url2, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data2 = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ // 3 Request
        url: url3, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
               data3 = +responseJSON.value;                  
        }           
    })
    ).then(function() {
    addData(data1,data2,data3);
});
    
    
    }
    
    
   
}

/**
    * @brief Initialize humidity graph
*/
function chartInit(){
    
     if(numberOfValue===1){
      console.log( "1 chart" );
        // array with consecutive integers: <0, maxSamplesNumber-1>
	timeVec = [...Array(maxStoredSamples).keys()]; 
	// scaling all values ​​times the sample time 
	timeVec.forEach(function(p, i) {this[i] = (this[i]*sampleTimeSec).toFixed(4);}, timeVec);
    
	// last value of 'timeVec'
	lastTimeStamp =+timeVec[timeVec.length-1];

  // last value of 'xdata'
  

  // empty array
  Vec1 = []; 

  // get chart context from 'canvas' element
  chartContext = $("#Chart")[0].getContext('2d');
  
  chart = new Chart(chartContext, {
    // The type of chart: linear plot
    type: 'line',

    // Dataset: 'xdata' as labels, 'ydata' as dataset.data
    data: {
      labels: timeVec,
      datasets: [{
        fill: false,
        label: label1,
        backgroundColor: 'rgb(255, 0, 0)',
        borderColor: 'rgb(255, 0, 0)',
        data: Vec1,
        lineTension: 0
      }]
    },

    // Configuration options
    options: {
      responsive: true,
      maintainAspectRatio: false,
      animation: false,
      scales: {
        yAxes: [{
          scaleLabel: {
            display: true,
            labelString: chartYLabel
          }
        }],
        xAxes: [{
          scaleLabel: {
            display: true,
            labelString: 'Time [s]'
          }
        }]
      }
    }
  });
  
  Vec1= chart.data.datasets[0].data;
  timeVec = chart.data.labels;
      
    }
    
    
    
    
    if(numberOfValue===3){
       console.log( "3 chart" );
       // array with consecutive integers: <0, maxSamplesNumber-1>
	timeVec = [...Array(maxStoredSamples).keys()]; 
	// scaling all values ​​times the sample time 
	timeVec.forEach(function(p, i) {this[i] = (this[i]*sampleTimeSec).toFixed(4);}, timeVec);
    
	// last value of 'timeVec'
	lastTimeStamp =+timeVec[timeVec.length-1]; 
     
    
	// empty array
	Vec1 = []; 
    Vec2 = []; 
    Vec3 = []; 
    
	// get chart context from 'canvas' element
	chartContext = $("#Chart")[0].getContext('2d');
    
	
    
	chart = new Chart(chartContext, {
		// The type of chart: linear plot
		type: 'line',
        
		// Dataset: 'timeVec' as labels, 'rollVec' as dataset.data
		data: {
			labels: timeVec,
			datasets: [
                {
                    fill: false,
                    label: label1,
                    backgroundColor: 'rgb(0, 255, 0)',
                    borderColor: 'rgb(0, 255, 0)',
                    data: Vec1 ,
                    lineTension: 0
                },
                {
                    fill: false,
                    label: label2,
                    backgroundColor: 'rgb(255, 0, 0)',
                    borderColor: 'rgb(255, 0, 0)',
                    data: Vec2,
                    lineTension: 0
                },
                {
                    fill: false,
                    label: label3,
                    backgroundColor: 'rgb(0,0 , 255)',
                    borderColor: 'rgb(0, 0, 255)',
                    data: Vec3,
                    lineTension: 0
                }
                
            ]
        },
        
		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: chartYLabel
                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Time [s]'
                    }
                }]
            }
        }
    });
    
	Vec1 = chart.data.datasets[0].data;
    Vec2 = chart.data.datasets[1].data;
    Vec3 = chart.data.datasets[2].data;
    timeVec = chart.data.labels;
      
    }
    
    
    
   
}


/**
    * @brief Initialize temperature graph
*/


$(document).ready(function() {
   
    timer = null;
   
    getRequest();
    displayData();
    
    chartInit();
   
    console.log( "window ready" );
    
     $('#chartName').change(function(){
    var value = $(this).val();
    console.log( value );
    if(value==='1')
    { 
         
         $("#unit1").text("C"); 
         $("#unit2").text("F"); 
    }
    
    
    //pressure
    if(value==='2')
    { 
        $("#unit1").text("hPa"); 
         $("#unit2").text("mmHg"); 
    }
    
    
    //humidity
    if(value==='3')
    { 
        $("#unit1").text("%"); 
         $("#unit2").text("[-]"); 
    }
    
    
    if(value==='4' || value==='5' || value==='6' )
    { 
          $("#unit1").text("Deg"); 
         $("#unit2").text("Rad");
       
    }
   
  });
    
    
    

    $.ajaxSetup({ cache: false }); // Web browser cache control
    
    $("#start").click(startTimer);
    $("#stop").click(stopTimer);
       
})




///////////////////////updating parameters


/**
* @brief Display date
*/
function displayData() {
	$("#ServerIP").text(serverIP);
	$("#sampletime").text(sampleTime.toString());
	$("#samplenumber").text(maxStoredSamples.toString());

}

/**
* @brief Update date
*/
function updateData(jsonObj) {
    serverIP = jsonObj.serverIP;
	sampleTime =parseInt(jsonObj.sample_time);
	maxStoredSamples =parseInt(jsonObj.max_samples);
    console.log(sampleTime);
	displayData();

}





/**
* @brief button click event handling
*/
function getRequest() {
	$.ajax({ // 1 Request
        url: urlget, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                updateData(responseJSON);                 
        }           
    });
    console.log("success");
}