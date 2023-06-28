<?php

$filepath = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContent.json";
$filepath2 = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContentWall.json";

$loginUser = $_POST["loginUser"];

require 'ConnectionSetting.php';

$sql = " SELECT json_f FROM account  WHERE name = '".$loginUser."' ";
$sql2 = " SELECT json_w FROM account  WHERE name = '".$loginUser."' ";

$result = $conn->query($sql);
$result2 = $conn->query($sql2);

$data = $result->fetch_assoc();
$data2 = $result2->fetch_assoc();

$file = file_put_contents($filepath,$data);
$file2 = file_put_contents($filepath2,$data2);

$conn->close();
?>