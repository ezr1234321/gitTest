<?php
require 'ConnectionSetting.php';
$filepath = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\TotalName.txt";
$sql = "SELECT name FROM account WHERE is_manager=0";
$result = $conn->query($sql);
$data="-";
if ($result->num_rows > 0) 
{
  // output data of each row
  while($row = $result->fetch_assoc()) {

    $data = $data.$row["name"]."-";
  }
}
echo $data;
$file = file_put_contents($filepath,$data);
$conn->close();
?>