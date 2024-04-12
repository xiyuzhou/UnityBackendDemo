<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$itemID = $_POST["itemID"];

$path = "https://appendicular-conjun.000webhostapp.com/ItemsIcons/" . $itemID . ".png";

$image = file_get_contents($path);

echo $image;
$conn->close();
?>