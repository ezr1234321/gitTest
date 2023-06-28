<?php
$servername = "localhost";
$username = "root";
$password ="qazxcdews";
$dbname = "UnityDB";
// create connection
$conn = new mysqli("localhost", "root", "qazxcdews", "UnityDB");
if ($conn ->connect_error){
    die("Connection failed: ".$conn->connect_error);
    echo"Connection fail \n";
}
?>