<?php
require_once('db.php');
ob_start();
session_start();
ob_end_clean();
if(isset($_POST['Login']))
{
    if(empty($_POST['username'])|| empty($_POST['password']))
    {
        header("location:login.php");
    }
    else {
        $username = $_POST['username'];
        $plainpassword = $_POST['password'];
        $hashedPassword = hash('sha512', $plainpassword);
        //$mysqlcon->query("UPDATE user SET `Password` = '$hashedPassword' WHERE  UserName = '".$_POST['username']."' and Password = '".$_POST['password']."'") or die ($mysqlcon->error);
        //$result = mysqli_query($mysqlcon, "SELECT * FROM user WHERE UserName = '$username' and Password = '$hashedPassword'");
		 $result = $mysqlcon->query("SELECT * FROM user WHERE UserName = '$username' and Password = '$hashedPassword'") or die($mysqlcon->error());
        if($result != null
           && $result->num_rows > 0)
        {
			$data = $result->fetch_array();
			  $_SESSION['uEdit'] ="";
			  $_SESSION['uAdd'] ="";
			  $_SESSION['uDelete'] ="";
			  if($data['CanEdit'] == 0){
				   $_SESSION['uEdit'] = "disabled";
			  }
			   if($data['CanAdd'] == 0){
				   $_SESSION['uAdd'] = "disabled";
			  }
			   if($data['CanDelete'] == 0){
				   $_SESSION['uDelete'] = "disabled";
			  }
                 $query = "SELECT PurchaseDate, LicenseId FROM license";
                 $result  = $mysqlcon->query($query);
                 $exp_date = date("Y/12/31 23:59:59");
                 $curr_date = date("Y/d/m");
                 if($curr_date == $exp_date){
                    while($rows = $result->fetch_array()){
                        if($rows['PurchaseDate'] < $exp_date){   
                        $LicID = array($rows['LicenseId']);
                        $resultUpdate = $mysqlcon->query("UPDATE license SET ExpiryDate = UTC_TIMESTAMP(), LicenseStatus = 'EXPIRED' WHERE LicenseId = '".$rows['LicenseId']."'");
                            if ($resultUpdate > 0) {
                            $_SESSION['message'] = "License with ID $LicID expired";
                            $_SESSION['msg_type'] = "success";
                            }                        
                        }
                    } 
                 }
            $_SESSION['user']= $_POST['username'];
            header("location:index.php");
        }
        else
        {
            ob_start();
            header("location:login.php?Invalid= Please enter a correct username and password");
            ob_end_clean(); 
        }
    }
}
else {
    echo 'not working';
}
?>