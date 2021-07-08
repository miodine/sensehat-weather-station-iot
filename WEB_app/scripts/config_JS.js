const ServerIoT = new XMLHttpRequest();
// configuration file

//-> DIRECTORIES FOR YOUR FILE SYSTEM:
const url = 'http://192.168.56.15/web_app/config.json';
const url_update_settings = 'http://192.168.56.15/web_app/config_update.php';






// default user setting (should be read from the server)
var serverIP = '';

var sample_time = '';
var max_samples = '';

/**
* @brief Display date
*/
function displayData() {
	$("#ServerIP").val(serverIP);
	$("#sample_time").val(sample_time);
	$("#max_samples").val(max_samples);

}

/**
* @brief Update date
*/
function updateData(jsonObj) {
	serverIP = jsonObj.serverIP;
	sample_time = jsonObj.sample_time;
	max_samples = jsonObj.max_samples;
	displayData();

}

/**
* @brief XMLHttpRequest Load event handling
*/
ServerIoT.onload = () => {
	errorCode = ServerIoT.status.toString();
	if (errorCode[0] == '2') { /// 2xx: Success
		document.getElementById("response").value = ServerIoT.responseText;
		var jsonObj = JSON.parse(ServerIoT.responseText);
		updateData(jsonObj);
	} else {
		requestError(errorCode, ServerIoT.statusText);
	}
}

/**
* @brief XMLHttpRequest Error event handling: network-level error
*/
ServerIoT.onerror = () => {
	requestError(ServerIoT.status, "Failed to fetch");
}

/**
* @brief button click event handling
*/
function getRequest() {
	ServerIoT.open("GET", url);
	/// setRequestHeader ( header, value)
	ServerIoT.setRequestHeader("Content-Type", "application/json");
	ServerIoT.send();
}

/**
* @brief 
*/

function saveConfig() {
	var _settings = new Object();

	_settings.serverIP = $("#ServerIP").val();
	_settings.sample_time = $("#sample_time").val();
	_settings.max_samples = $("#max_samples").val();

	settings_stringified = JSON.stringify(_settings);


	$.ajax({
		url: url_update_settings,
		type: "POST",
		data: { settings: settings_stringified },
		dataType: "json",
		async: false,
		success: function (data) {
			alert(data);
		}
	});

}

$(document).ready(() => {
	displayData();
	$("#save").click(saveConfig);
	$("#load").click(getRequest);

});
