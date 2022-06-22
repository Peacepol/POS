<?php
	$host = "localhost";
	$user = "c1able23_ableapi";
	$pass = "9;h1nZ-#Ee[O";
	$port = 3306;
	$database = "c1able23_posdb";
	
	$mysqlcon = new mysqli($host, $user, $pass, $database, $port);
	
	if ($mysqlcon->connect_error)
		die("connection error: " . $mysqlcon->connect_error);
?>