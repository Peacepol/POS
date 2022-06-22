<?php
	$host = "localhost";
	$user = "root";
	$pass = "";
	$port = 3306;
	$database = "ablelicense";
	
	$mysqlcon = mysqli_connect($host, $user, $pass, $database);
	
	if ($mysqlcon->connect_error)
		die("connection error: " . $mysqlcon->connect_error);
?>