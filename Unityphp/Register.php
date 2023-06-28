<?php

require 'ConnectionSetting.php';

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// check connection
if ($conn ->connect_error){
    die("Connection failed: ".$conn->connect_error);
}
echo"Connection successfully</br>";

$sql = "SELECT  password FROM account WHERE name ='" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) { 
  echo "Username is already taken.";
}
// create account to DB
else {
  echo "Creating user...";
  $sql2 ="INSERT INTO account (name, password,is_manager) VALUES('" . $loginUser . "','" . $loginPass . "',0)";
  if ($conn->query($sql2) === TRUE){
    echo "NEW record created successfully";
  }
  else{
    echo "Error: " . $sql2 . "</br>" . $conn ->error;
  }
}
$conn->close();
?>