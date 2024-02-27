<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackend";

$userID = $_POST["userID"];
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname );

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully";

$sql = "SELECT itemID FROM usersitems WHERE userID = '". $userID . "'";

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