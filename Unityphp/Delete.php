<?php

require 'ConnectionSetting.php';

$User = $_POST["User"];

$state = 0;
// check connection
$sql = "DELETE FROM account WHERE name ='" . $User . "'";
$result = $conn->query($sql);

$conn->close();
?>