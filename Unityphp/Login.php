<?php

require 'ConnectionSetting.php';
$filepath = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\state.txt";
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];
$state = 0;
// check connection


$sql = "SELECT  password FROM account WHERE name ='" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass){
        echo "Login Success.";
        $file = file_put_contents($filepath,"success");
        $state = 1;
    }
    else{
        echo "Wrong Password.";
        $file = file_put_contents($filepath,"faile");
    }
  }
}
 else {
  echo "Username does not exists.";
}
$sql2 = "SELECT  is_manager FROM account WHERE name ='" . $loginUser . "'";
$result2 = $conn->query($sql2);
$row2 = $result2->fetch_assoc();
if($row2["is_manager"] == 1 && $state == 1 ){//管理者
  $file = file_put_contents($filepath,"manager");
  require 'GetUserInfor.php';
}
$conn->close();
?>