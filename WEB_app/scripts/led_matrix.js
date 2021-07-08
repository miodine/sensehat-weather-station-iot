var RED = 0;
var GREEN = 0;
var BLUE = 0;
var LED_states = new Array(8);
var LED_submit=[];

const url = 'http://192.168.56.15/led_display2.php';

for (var i = 0; i < 64; i++) {
    LED_states[i] = [0, 0, 0, 0, 0];
}

function updateMonit(monit) {
    $("#monit_").html("Monit: " + monit);
}




function changeColour(id) {
    var set_value = "rgb(" + RED + "," + GREEN + "," + BLUE + ")";

    document.getElementById(id).style.backgroundColor = set_value;

    if (id != "reference") {
        var id_stringified = id.split('');

        var i = +id_stringified[0];
        var j = +id_stringified[1];

        LED_states[i + j] = [j, i, parseInt(RED), parseInt(GREEN), parseInt(BLUE)];
        LED_submit.push(LED_states[i+j]);
    }
    updateMonit("LED change applied! The LED states are out of sync now!");
}


function slider_in() {
    RED = $("#R_range").val();
    GREEN = $("#G_range").val();
    BLUE = $("#B_range").val();
    changeColour('reference');
}

function generateMatrix() {
    var i = 0;
    var j = 0;

    var table_buffer_content = "<table class = 'tbcustom'>";

    for (i = 0; i < 8; i++) { //ROW 
        table_buffer_content += "<tr>";
        for (j = 0; j < 8; j++) { //COL
            table_buffer_content += "<td><button id='" + i.toString() + j.toString() + "' onClick='changeColour(this.id)' type='button' class='led' style='background-color:#b3aeb5; border-color:#2c344e;'></button></td >"
        }
        table_buffer_content += "</tr>";
    }

    table_buffer_content += "</table>";
    $('#matrix_').append(table_buffer_content);
}

function submitMatrixState() {
   
    
    var arr = JSON.stringify(LED_submit);
    $.ajax({
        url: url,
        type: "POST",
        data: { ids: arr },
        dataType: "json",
        async: false,
        success: function (data) {
         console.log( "data sent" );
        }
    })
    
   

    updateMonit("Module initialised!");
}

function clear(){
    
    $(".led").css("background-color", "#b3aeb5");
    LED_submit=[];
     var arr = JSON.stringify(LED_submit);
    $.ajax({
        url: url,
        type: "POST",
        data: { ids: arr },
        dataType: "json",
        async: false,
        success: function (data) {
           console.log( "clear done" );
        }
    })
}


$(document).ready(function() {
    $("#clear").click(clear);
    
})