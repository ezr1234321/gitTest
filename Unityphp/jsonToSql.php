<?php

require 'ConnectionSetting.php';

$filename = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContent.json";
$filename2 = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContentWall.json";

$data = file_get_contents($filename);
$data2 = file_get_contents($filename2);

$array = json_decode($data,true);
$array2 = json_decode($data2,true);

$loginUser = $_POST["loginUser"];

$sql = "UPDATE account SET json_f ='".$data."' WHERE name = '".$loginUser."'";
$sql2 = "UPDATE account SET json_w ='".$data2."' WHERE name = '".$loginUser."'";

$result = $conn->query($sql);
$result2 = $conn->query($sql2);

$conn->close();

?>