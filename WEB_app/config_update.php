<?php
$var = $_POST['settings'];
file_put_contents('config.json', $var);
echo json_encode($var);
?>