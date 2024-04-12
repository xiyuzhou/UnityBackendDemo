<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully";
//echo "<br>";
//variables by users
//$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];
$itemID = $_POST["itemID"];
// Create connection

$response = array();

$sql = "SELECT name, description, price FROM items WHERE ID = '". $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {

    $row = array();
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  echo json_encode($rows);
} 
else 
{
  echo "o result";
}


$conn->close();
?>