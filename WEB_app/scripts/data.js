var temp_h_unit='1';
var temp_p_unit='1';
var pres_unit='1';
var hum_unit='1';
var accelerometer_unit = '1';      // Unit of roll, pitch, yaw from accelerometer
var gyroscope_unit = '1';  
var orientation_unit = '1';  


var serverIP='';

var sampleTime = 1000;              // Sample time [ms]
var maxStoredSamples = 1000;        // Maximum number of stored samples
var timer;                          // Request timer

const urlget='http://192.168.56.15/web_app/config.json';


const urlJX='http://192.168.56.15/measurements/joy_counter_x_click_pos.json';
const urlJY='http://192.168.56.15/measurements/joy_counter_y_click_pos.json';
const urlJZ='http://192.168.56.15/measurements/joy_counter_c_click_pos.json';

const urlTH='http://192.168.56.15/measurements/advanced/temperature_from_humidity_C.php';	
const urlTP='http://192.168.56.15/measurements/advanced/temperature_from_pressure_C.php';	
const urlP='http://192.168.56.15/measurements/pressure_hPa.php';	
const urlH='http://192.168.56.15/measurements/humidity_%.php';	

const urlAX='http://192.168.56.15/measurements/advanced/roll_accelerometer_deg.php';
const urlAY='http://192.168.56.15/measurements/advanced/pitch_accelerometer_deg.php';
const urlAZ='http://192.168.56.15/measurements/advanced/yaw_accelerometer_deg.php';	

const urlGX='http://192.168.56.15/measurements/advanced/roll_gyroscope_deg.php';
const urlGY='http://192.168.56.15/measurements/advanced/pitch_gyroscope_deg.php';
const urlGZ='http://192.168.56.15/measurements/advanced/yaw_gyroscope_deg.php';

const urlMX='http://192.168.56.15/measurements/advanced/induction_x_uT.php';
const urlMY='http://192.168.56.15/measurements/advanced/induction_y_uT.php';
const urlMZ='http://192.168.56.15/measurements/advanced/induction_z_uT.php';

const urlOX='http://192.168.56.15/measurements/roll_deg.php';
const urlOY='http://192.168.56.15/measurements/pitch_deg.php';
const urlOZ='http://192.168.56.15/measurements/yaw_deg.php';



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


$(document).ready(function() {
    
     getRequest();
    displayData();
  

  

     $('#Temp_H').change(function(){
     temp_h_unit= $(this).val();
     });

     $('#Temp_P').change(function(){
     temp_p_unit= $(this).val();
     });
     
     
     $('#press').change(function(){
     pres_unit= $(this).val();
     });

     $('#hum').change(function(){
     hum_unit= $(this).val();
     });
     
     
     $('#orientation').change(function(){
     orientation_unit= $(this).val();
     });
     
     $('#acc').change(function(){
     accelerometer_unit= $(this).val();
     });
     
     $('#gyro').change(function(){
     gyroscope_unit= $(this).val();
     });

    // Sample time change listener
    $("#sampleTime").change(function(){
        var inputValue = Number($(this).val());
        if (inputValue != NaN){
            sampleTime = inputValue;
        }
    });

    // Maximum number of stored samples change listener
    $("#storedSamples").change(function(){
        var inputValue = Number($(this).val());
        if (inputValue != NaN){
            maxStoredSamples = inputValue;
        }
    });

})

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
	var data=[];
    

$.when(
    $.ajax({ 
        url: urlJX, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[0] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlJY, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[1] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlJZ, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[2] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlTH, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[3] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlTP, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[4] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlP, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[5] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlH, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[6] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlOX, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[7] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlOY, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[8] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlOZ, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
               data[9] = +responseJSON.value;                  
        }           
    }),
    
    $.ajax({ 
        url: urlAX, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[10] = +responseJSON.value;                  
        }           
    }),
    
    $.ajax({ 
        url: urlAY, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
               data[11] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlAZ, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[12] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlMX, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[13] = +responseJSON.value;                  
        }           
    }),
    
    $.ajax({ 
        url: urlMY, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[14] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlMZ, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[15] = +responseJSON.value;     
        }           
    }),
    
     $.ajax({ 
        url: urlGX, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[16] = +responseJSON.value;                  
        }           
    }),

    $.ajax({ 
        url: urlGY, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {                           
            data[17] = +responseJSON.value;     
        }           
    }),
    
    $.ajax({ 
        url: urlGZ, 
        type: 'GET', dataType: 'json',
    success: function(responseJSON, status, xhr) {   
                data[18] = +responseJSON.value;                  
        }           
    })

).then(function() {
    updateTable(data);
});
}





// 0,1,2=JOYstick; 3,4=Temp; 5=Pressure; 6=Humidity; 7,8,9=Orientation; 10,11,12=Accelerometer; 13,14,15= MAG; and 16,17,18=Gyro;
function updateTable(data,data1){

    if(temp_h_unit ==='2'){
        data[3]=CelsiusToFahrenheit(data[3]);
    }
    
    
    if(temp_p_unit ==='2'){ 
        data[4]=CelsiusToFahrenheit(data[4]);
    }
    
    if(pres_unit ==='2'){ 
        data[5]=hPaTommHg(data[5]);
    }
    
    
    if(hum_unit ==='2'){ 
        data[6]=PercentToDecimal(data[6]);
    }
    
    
    if(orientation_unit ==='2'){ 
        data[7] = DegreesToRadians(data[7]); 
        data[8] = DegreesToRadians(data[8]); 
        data[9] = DegreesToRadians(data[9]); 
    }
    
    
    if(accelerometer_unit ==='2'){ 
        data[10] = DegreesToRadians(data[10]); 
        data[11] = DegreesToRadians(data[11]); 
        data[12] = DegreesToRadians(data[12]); 
    }
        
    if(gyroscope_unit === '2'){ 
        data[16] = DegreesToRadians(data[16]); 
        data[17] = DegreesToRadians(data[17]); 
        data[18] = DegreesToRadians(data[18]);  
    }
    
    $("#JXValue").html((data[0]).toFixed(3));
    $("#JYValue").html((data[1]).toFixed(3));
    $("#JZValue").html((data[2]).toFixed(3));
    
    
    $("#tempFromHumValue").html((data[3]).toFixed(3));
    $("#tempFromPresValue").html((data[4]).toFixed(3));
    $("#presValue").html((data[5]).toFixed(3));
    $("#humValue").html((data[6]).toFixed(3));
    
    
    $("#OXValue").html((data[7]).toFixed(3));
    $("#OYValue").html((data[8]).toFixed(3));
    $("#OZValue").html((data[9]).toFixed(3));
    
    $("#AXValue").html((data[10]).toFixed(3));
    $("#AYValue").html((data[11]).toFixed(3));
    $("#AZValue").html((data[12]).toFixed(3));
    $("#MXValue").html((data[13]).toFixed(3));
    $("#MYValue").html((data[14]).toFixed(3));
    $("#MZValue").html((data[15]).toFixed(3));
    $("#GXValue").html((data[16]).toFixed(3));
    $("#GYValue").html((data[17]).toFixed(3));
    $("#GZValue").html((data[18]).toFixed(3));
}


 
 function DegreesToRadians(value){
    return value * Math.PI / 180;
}

function CelsiusToFahrenheit(val){
    return 1.8 * val + 32;
}





function hPaTommHg(val){
    return val * 0.7500616827;
}


function PercentToDecimal(val){
    return val / 100;
}