<?php
session_start();
require('db.php');
require('function.php');

// Add Client
if(isset($_POST['save'])) {
    $ClientName = $_POST['ClientName'];
    $SerialNumber = $_POST['SerialNumber'];
    $RegistrationCode = $_POST['RegistrationCode'];
    $Address = $_POST['Address'];
    $City = $_POST['City'];
    $State = $_POST['State'];
    $PostCode = $_POST['PostCode'];
    $Country = $_POST['Country'];
    $RegistrationName = $_POST['RegistrationName'];
    $Phone = $_POST['Phone'];
    $Fax = $_POST['Fax'];
    $Email = $_POST['Email'];  
    $ContactPerson = $_POST['ContactPerson'];
    $ControlCode = $_POST['ControlCode'];

    $result = $mysqlcon->query("SELECT `idcontrolcode` FROM `controlcode` WHERE `codeclient`='$ControlCode' AND `idclient` IS NOT NULL") or die($mysqlcon->error);
    if($result != null && $result->num_rows >= 1) {
        echo "<script type='text/javascript'>
                  window.alert('Control Code is already used');
                  window.location = 'index.php';
              </script>";
    } else { 
        $mysqlcon->query("INSERT INTO `client` (`ClientName`, `SerialNumber`, `RegistrationCode`, `Address`, `City`, `State`, `PostCode`, `Country`, `RegistrationName`, `Phone`, `Fax`, `Email`, `ContactPerson`, `ControlCode`)
                          VALUES('$ClientName', '$SerialNumber', '$RegistrationCode', '$Address', '$City', '$State', '$PostCode', '$Country', '$RegistrationName', '$Phone', '$Fax', '$Email', '$ContactPerson', '$ControlCode')") or die($mysqlcon->error);    

        $insertedid = $mysqlcon->insert_id;
        
        $mysqlcon->query("UPDATE `controlcode` SET `idclient` = $insertedid WHERE codeclient='$ControlCode'") or die($mysqlcon->error);   

        $_SESSION['message']= "Client Register Successful!";
        $_SESSION['msg_type'] = "success";

        header("location: index.php?view=DataTableClient");
    }
}

//Delete Client
if(isset($_GET['delete'])) {
    $ClientId = $_GET['delete'];
    $mysqlcon->query("DELETE FROM client WHERE ClientId = $ClientId") or die($mysqlcon->error());
    
    $_SESSION['message']= "Client has been Deleted!";
    $_SESSION['msg_type'] = "danger";  
    
    header("location: index.php?view=DataTableClient");
}

//Delete License
if(isset($_GET['deletelic'])) {
    $LicenseId = $_GET['deletelic'];
    $mysqlcon->query("DELETE FROM license WHERE LicenseId = $LicenseId") or die($mysqlcon->error());
    
    $_SESSION['message']= "License has been Deleted!";
    $_SESSION['msg_type'] = "danger";  
    
    header("location: index.php?view=DataTableLicense");
}

//Fetch Data Client
if(isset($_GET['edit'])) {
    $ClientId = $_GET['edit'];
    $result  = $mysqlcon->query("SELECT * FROM client WHERE ClientId = $ClientId") or die($mysqlcon->error());
    if($result != null && $result->num_rows >= 1) {
        $data = $result->fetch_array();
        $ClientId = $data['ClientName'];
        $ClientName = $data['ClientName'];
        $SerialNumber = $data['SerialNumber'];
        $RegistrationCode = $data['RegistrationCode'];
        $Address = $data['Address'];
        $City = $data['City'];
        $State =$data['State'];
        $PostCode = $data['PostCode'];
        $Country = $data['Country'];
        $RegistrationName =$data['RegistrationName'];
        $Phone = $data['Phone'];
        $Fax = $data['Fax'];
        $Email = $data['Email'];  
        $ContactPerson = $data['ContactPerson'];
    }
}

//Update Data Client
if(isset($_POST['update'])) {
    $ClientId = $_POST['ClientId'];
    $ClientName = $_POST['ClientName'];
    $SerialNumber = $_POST['SerialNumber'];
    $RegistrationCode = $_POST['RegistrationCode'];
    $Address = $_POST['Address'];
    $City = $_POST['City'];
    $State =$_POST['State'];
    $PostCode = $_POST['PostCode'];
    $Country = $_POST['Country'];
    $RegistrationName =$_POST['RegistrationName'];
    $Phone = $_POST['Phone'];
    $Fax = $_POST['Fax'];
    $Email = $_POST['Email'];  
    $ContactPerson = $_POST['ContactPerson'];
    
    $mysqlcon->query("UPDATE `client` SET `ClientName` = '$ClientName', `SerialNumber` = '$SerialNumber', `RegistrationCode` = '$RegistrationCode', `Address` =  '$Address', `City` = '$City', `State` = '$State', `PostCode` = '$PostCode', `Country` = '$Country', `RegistrationName` = '$RegistrationName', `Phone` = '$Phone', `Fax` = '$Fax', `Email` = '$Email', `ContactPerson` = '$ContactPerson' WHERE `ClientId` = '$ClientId' ") or die ($mysqlcon->error);
    
    $_SESSION['message']= "Client has been Updated!";
    $_SESSION['msg_type'] = "success";  
    
    header("location: index.php?view=DataTableClient");
}

//Fetch Data Client on activate
if(isset($_GET['active'])) {
    $ClientId = $_GET['active'];
    $result  = $mysqlcon->query("SELECT * FROM client WHERE ClientId = $ClientId") or die($mysqlcon->error());
    if($result->num_rows > 0) {
        $data = $result->fetch_array();
        $ClientId = $data['ClientName'];
        $ClientName = $data['ClientName'];
        $SerialNumber = $data['SerialNumber'];
        $RegistrationCode = $data['RegistrationCode'];
    }
}

if (isset($_POST['activatesave'])
    && (!isset($_POST['activationcode']) 
        || $_POST['activationcode'] == "")) {
    echo "<script type='text/javascript'>
              window.alert('Activation code cannot be empty');
              window.location = 'index.php?view=DataTableClient';
         </script>";
}

if(isset($_POST['activationcode'])
    && $_POST['activationcode'] != "") {
    $actcode = $_POST['activationcode'];
    $existsql = "SELECT * FROM `license` WHERE `ActivationCode` = '$actcode'";
    $selectresult = $mysqlcon->query($existsql);
    $clientid = $_POST['ClientId'];
    $licensetype = $_POST['licensetype'];
    $terminalcount = $_POST['terminalcount'];
    
    if ($selectresult!= null 
        && $selectresult->num_rows > 0) {
        echo "<script type='text/javascript'>
                  window.alert('A similar license already exists');
                  window.location = 'index.php?view=DataTableClient';
              </script>";
    } else {
        $savelicsql = "INSERT INTO license 
                                (`ClientId`, 
                                 `InvoiceId`, 
                                 `CreationDate`, 
                                 `PurchaseAmount`, 
                                 `PaymentMode`, 
                                 `LicenseStatus`, 
                                 `TerminalCount`,
                                 `LicenseType`,
                                 `ActivationCode`)
                                VALUES 
                                ($clientid, 
                                 0, 
                                 UTC_TIMESTAMP(), 
                                 0, 
                                 'UNPAID', 
                                 'INACTIVE',
                                 '$terminalcount',
                                 '$licensetype',
                                 '".$_POST['activationcode']."')";

        $result = $mysqlcon->query($savelicsql);
        
        if ($result >= 1) {
            echo "<script type='text/javascript'>
                      window.alert('License created successfully');
                      window.location = 'index.php?view=DataTableLicense';
                  </script>";
        } else {
            echo "<script type='text/javascript'>
                      window.alert('Database entry failed');
                      window.location = 'index.php?view=DataTableClient';
                  </script>";
        }
        unset($_POST['activationcode']);
    }
}

//Update Data license
if(isset($_POST['licenseupdate'])) {
    $LicenseId = $_POST['LicenseId'];
    $PurchaseAmount = $_POST['PurchaseAmount'];
    $PaymentMode = $_POST['PaymentMethod'];
    $ActivationCode = $_POST['newactivationcode'];
    
    $existsql = "SELECT * FROM `license` WHERE `ActivationCode` = '$ActivationCode'";
    $selectresult = $mysqlcon->query($existsql);
    if ($selectresult!= null 
        && $selectresult->num_rows > 0) {
            $_SESSION['message']= "A similar license already exists";
            $_SESSION['msg_type'] = "warning";  
    } else {
    
    $mysqlcon->query("UPDATE `license` SET `PurchaseAmount` = '$PurchaseAmount', `PaymentMode` = '$PaymentMode', `ActivationCode` = '$ActivationCode' WHERE `LicenseId` = $LicenseId ") or die ($mysqlcon->error);
    
    $_SESSION['message']= "License has been Updated!";
    $_SESSION['msg_type'] = "success";  
    }
    header("location: index.php?view=DataTableLicense");
}

if (isset($_POST['payreactivation'])
    && (!isset($_POST['ReactivationCode']) 
    || $_POST['ReactivationCode'] == "")) {
    echo "<script type='text/javascript'>
              window.alert('Activation code cannot be empty');
              window.location = 'index.php?view=AnnualMaintenance';
         </script>";
}

//Reactivation
if(isset($_POST['payreactivation'])
    && isset ($_POST['ReactivationCode'])
    && $_POST['ReactivationCode'] != "") {

    $ClientName = $_POST['CompanyName'];
    $ClientId = $_POST['CompanyID'];
    $InvoiceCode = $_POST['InvoiceCode'];
    $ReactivationCode = $_POST['ReactivationCode'];
    $PaymentMethod = $_POST['PaymentMethod'];
    $PaymentDate = $_POST['PaymentDate'];
    $PaymentAmount = $_POST['PaymentAmount'];
    $LicenseId = $_POST['LicenseId'];
    $Type = $_POST['software'];
    if($Type == 'ACCPAC'){
    $LicenseCode = "ABLEACCTG-".substr($ClientName, 0, 4)."-".substr($InvoiceCode, -4, 4);
	}else{
		 $LicenseCode = "ABLERETAIL-".substr($ClientName, 0, 4)."-".substr($InvoiceCode, -4, 4);
	}

    $Status = "PAID";
    $result = $mysqlcon->query("UPDATE `invoice` SET 
                                `SoftwareType` = '$Type', 
                                `Status` = '$Status', 
                                `PaymentDate` = '$PaymentDate' 
                                WHERE `InvoiceCode` = '$InvoiceCode'") or die($mysqlcon->error);

    if ($result > 0) {       
        $resultlic = $mysqlcon->query("UPDATE `license` SET 
                                       `LicenseCode` = '$LicenseCode', 
                                       `PurchaseDate` = '$PaymentDate', 
                                       `PurchaseAmount` = $PaymentAmount, 
                                       `PaymentMode` = '$PaymentMethod', 
                                       `LicenseStatus` = 'ACTIVE', 
                                       `ActivationCode` = '$ReactivationCode' 
                                       WHERE LicenseId = $LicenseId") or die($mysqlcon->error);
        if ($resultlic > 0) {
            //$resultinvalidatelic = $mysqlcon->query("UPDATE license SET LicenseStatus = 'INACTIVE', ExpiryDate = UTC_TIMESTAMP() WHERE LicenseId = $LicenseId");
            $_SESSION['message']= "Payment and reactivation is successful";
            $_SESSION['msg_type'] = "success";
        } else {
            $_SESSION['message']= "Failed to reactivate license";
            $_SESSION['msg_type'] = "danger";
        }
    } else {
        $_SESSION['message']= "Payment failed";
        $_SESSION['msg_type'] = "danger";
    }
    header("location:index.php?view=AnnualMaintenance");
}

if(isset($_POST['reactivationinvoice'])) {
    $ClientName = $_POST['CompanyName'];
    $ClientId = $_POST['CompanyID'];
    $InvoiceCode = $_POST['InvoiceCode'];
    $Description = $_POST['Description'];
    $Year = $_POST['InvoiceYear'];
    

    $result = $mysqlcon->query("INSERT INTO `invoice` (`InvoiceCode`, `Status`, `CreationDate`, `Description`, `Year`) VALUES 
                              ('$InvoiceCode', 'UNPAID',UTC_TIMESTAMP(), '$Description', '$Year')") or die($mysqlcon->error);

    if ($result > 0) {       
        $resultlic = $mysqlcon->query("INSERT INTO `license` (`ClientId`, `InvoiceId`, `CreationDate`, `LicenseStatus`) VALUES($ClientId, (SELECT MAX(InvoiceId) FROM invoice),UTC_TIMESTAMP(), 'INACTIVE')") or die($mysqlcon->error);
        if ($resultlic > 0) {
            $_SESSION['message'] = "A new unpaid license with Invoice code '$InvoiceCode' has been created for $ClientName";
            $_SESSION['msg_type'] = "success";
        } else {
            $_SESSION['message'] = "Creation of license failed";
            $_SESSION['msg_type'] = "danger";
        }
    } else {
        $_SESSION['message'] = "Creation of invoice failed";
        $_SESSION['msg_type'] = "danger";
    }
    
    header("location: index.php?view=AnnualMaintenance");
}

//table refresh
if(isset($_POST['refresh'])) {
    if (isset($_POST['client'])) {
        $query = "SELECT * FROM `client`";        
    } else if (isset($_POST['license'])) {
        $query = "SELECT 
                    l.LicenseId, 
                    c.ClientName, 
                    l.PurchaseDate, 
                    l.PurchaseAmount, 
                    l.PaymentMode, 
                    l.LicenseStatus, 
                    l.ActivationCode, 
                    l.ExpiryDate, 
                    l.LicenseCode, 
                    l.CreationDate 
                  FROM license l 
                  INNER JOIN client c ON l.ClientId=c.ClientId";
    } else if (isset($_POST['annualmaintenance'])) {
        $query = "SELECT 
                    c.ClientId, 
                    c.ClientName, 
                    i.Year, 
                    i.InvoiceCode, 
                    l.LicenseStatus, 
                    l.ExpiryDate, 
                    l.LicenseCode, 
                    l.PurchaseAmount, 
                    l.LicenseId,
                    i.SoftwareType         
                  FROM client c 
                  INNER JOIN license l ON l.ClientId=c.ClientId 
                  INNER JOIN invoice i ON l.InvoiceId=i.InvoiceId";
    } else if (isset($_POST['controlcode'])) {
        $query = "SELECT 
                    cc.codeclient AS ControlCode,
                    c.ClientName,
                    c.SerialNumber,
                    c.RegistrationCode
                  FROM controlcode cc 
                  LEFT JOIN client c ON c.ClientId=cc.idclient";
    }else if (isset($_POST['usermanager'])) {
        $query = "SELECT 
                    UserId,
                    UserName,
					 FullName,
                    CanAdd,
                    CanEdit, CanDelete 
                  FROM user ";
				  
    }

    $_SESSION['searchquery'] = $query;
    
    if (isset($_POST['client'])) {
        header("location: index.php?view=DataTableClient");
    } else if (isset($_POST['license'])) {
        header("location: index.php?view=DataTableLicense");
    } else if (isset($_POST['annualmaintenance'])) {
        header("location: index.php?view=AnnualMaintenance");
    } else if (isset($_POST['controlcode'])) {
        header("location: index.php?view=ControlCode");
    } else if (isset($_POST['usermanager'])) {
        header("location: index.php?view=UserManager");
    }
}

//search table
if(isset($_POST['search'])) {
    $valueToSearch = $_POST['valueToSearch'];
    
    if (isset($_POST['client'])) {
        $query = "SELECT * FROM `client` WHERE CONCAT(`ClientName`,`SerialNumber`,`RegistrationCode`,`Address`,`City`,`State`,`PostCode`,`Country`,`RegistrationName`,`Phone`,`Fax`,`Email`,`ContactPerson`) LIKE '%".$valueToSearch."%'";
    } else if (isset($_POST['license'])) {
        $query = "SELECT 
                    l.LicenseId, 
                    c.ClientName, 
                    l.PurchaseDate, 
                    l.PurchaseAmount, 
                    l.PaymentMode, 
                    l.LicenseStatus, 
                    l.ActivationCode, 
                    l.ExpiryDate, 
                    l.LicenseCode, 
                    l.CreationDate 
                  FROM license l 
                  INNER JOIN client c on l.ClientId=c.ClientId
                  WHERE CONCAT(IFNULL(c.ClientName, ''), IFNULL(l.PurchaseAmount, ''), IFNULL(l.PurchaseAmount, ''), IFNULL(l.PaymentMode, ''), IFNULL(l.LicenseStatus, ''), IFNULL(l.LicenseCode, '')) LIKE '%".$valueToSearch."%'";
    } else if (isset($_POST['annualmaintenance'])) {
        $query = "SELECT 
                    c.ClientId, 
                    c.ClientName, 
                    i.Year, 
                    i.InvoiceCode,
                    l.LicenseStatus,
                    l.ExpiryDate,
                    l.LicenseCode, 
                    l.PurchaseAmount,
                    l.LicenseId,
                    i.SoftwareType
                  FROM license l 
                  INNER JOIN client c ON l.ClientId=c.ClientId
                  INNER JOIN invoice i ON l.InvoiceId=i.InvoiceId
                  WHERE CONCAT(IFNULL(c.ClientName, ''), IFNULL(c.SerialNumber, ''), IFNULL(c.RegistrationCode, ''), IFNULL(l.LicenseCode, ''), IFNULL(l.PurchaseAmount, '')) LIKE '%".$valueToSearch."%'";
    } else if (isset($_POST['controlcode'])) {
        $searchfilter = "";
        if (isset($_POST['unusedcode'])) {
            $searchfilter = "AND c.ClientName IS NULL";
        }
        
        $query = "SELECT 
                    cc.codeclient AS ControlCode,
                    c.ClientName,
                    c.SerialNumber,
                    c.RegistrationCode
                  FROM controlcode cc 
                  LEFT JOIN client c ON c.ClientId=cc.idclient
                  WHERE CONCAT(
                    IFNULL(c.ClientName, ''), 
                    IFNULL(c.SerialNumber, ''), 
                    IFNULL(c.RegistrationCode, ''), 
                    IFNULL(cc.codeclient, '')
                  )
                  LIKE '%".$valueToSearch."%'".$searchfilter;
				  
    }
	else if (isset($_POST['usermanager'])) {
        $query = "SELECT 
                    UserId,
                    UserName,
					FullName,
                    CanAdd,
                    CanEdit, CanDelete 
                  FROM user 
                  WHERE CONCAT(
                    IFNULL(UserName, ''), 
                    IFNULL(FullName, '')
                  )
                  LIKE '%".$valueToSearch."%'";
    }

    $_SESSION['searchquery'] = $query;

    if (isset($_POST['client'])) {
        header("location: index.php?view=DataTableClient");   
    } else if (isset($_POST['license'])) {
        header("location: index.php?view=DataTableLicense");  
    } else if (isset($_POST['annualmaintenance'])) {
        header("location: index.php?view=AnnualMaintenance");  
    } else if (isset($_POST['controlcode'])) {
        header("location: index.php?view=ControlCode");
    }else if (isset($_POST['usermanager'])) {
        header("location: index.php?view=UserManager");
    }
}

if(isset($_POST['paid'])) {
    $ClientName = $_POST['CompanyName'];
    $LicenseId = $_POST['LicenseId'];
    $PurchaseAmount = $_POST['PaymentAmount'];
    $InvoiceCode = $_POST['InvoiceCode'];
    $PaymentMethod = $_POST['PaymentMethod'];
    $PaymentDate = $_POST['PaymentDate'];
    $Description = $_POST['Description'];
    $Year = $_POST['CurrentYear'];
    $Type = $_POST['software'];
    $Status = "PAID";
	
	if($Type == 'ACCPAC'){
    $LicenseCode = "ABLEACCTG-".substr($ClientName, 0, 4)."-".substr($InvoiceCode, -4, 4);
	 }else{
		 $LicenseCode = "ABLERETAIL-".substr($ClientName, 0, 4)."-".substr($InvoiceCode, -4, 4);
	}
   // $LicenseCode = "ABLEACCTG-".substr($ClientName, 0, 4)."-".substr($InvoiceCode, -4, 4);
    
    $result = $mysqlcon->query("SELECT * FROM invoice WHERE InvoiceCode = '$InvoiceCode' AND Status = 'PAID'");
    if ($result != null && $result->num_rows > 0) {
        echo "<script type='text/javascript'>
                  window.alert('This license have an existing invoice already');
                  window.location = 'index.php?view=DataTableLicense';
              </script>";
    } else {   
        $resultinsert = $mysqlcon->query("INSERT INTO `invoice` (`InvoiceCode`, `Status`, `CreationDate`, `PaymentDate`, `Description`, `Year`, `SoftwareType`) VALUES 
                                        ('$InvoiceCode', '$Status', UTC_TIMESTAMP(),'$PaymentDate', '$Description', '$Year', '$Type')") or die($mysqlcon->error);
        if ($resultinsert > 0) {
            $resultupdate = $mysqlcon->query("UPDATE license SET `PurchaseAmount` = $PurchaseAmount, `PurchaseDate` = UTC_TIMESTAMP(), `PurchaseAmount` = $PurchaseAmount, `InvoiceId` = (SELECT MAX(InvoiceId) FROM invoice), `LicenseCode` = '$LicenseCode', `PaymentMode` = '$PaymentMethod', `LicenseStatus` = 'ACTIVE' WHERE `LicenseId` = $LicenseId") or die ($mysqlcon->error);
            if($resultupdate) {
                $_SESSION['message'] = "Payment Successful";
                $_SESSION['msg_type'] = "success";
            } else {
                $_SESSION['message'] = "Payment of license failed";
                $_SESSION['msg_type'] = "danger";
            }
        } else {
            $_SESSION['message'] = "Invoice creation failed";
            $_SESSION['msg_type'] = "danger";
        }
        
        header("location: index.php?view=DataTableLicense");
    }
}

if(isset($_GET['expirelic'])) {
    $LicenseId = $_GET['expirelic'];
    $result = $mysqlcon->query("UPDATE license SET ExpiryDate = UTC_TIMESTAMP(), LicenseStatus = 'EXPIRED' WHERE LicenseId = $LicenseId");
    if ($result > 0) {
        $_SESSION['message'] = "License with ID $LicenseId expired";
        $_SESSION['msg_type'] = "success";
    }
    header("location: index.php?view=AnnualMaintenance");
}

if(isset($_POST['controlcodesave'])
    && $_POST['controlcodesave'] != "") {
    $clientid = $_POST['clientid'];
    $controlcode = $_POST['controlcode'];
    $saveccode = "INSERT INTO controlcode 
                            (`idclient`, 
                             `codeclient`)
                            VALUES 
                            ($clientid, 
                             '$controlcode')";

    $result = $mysqlcon->query($saveccode);
    
    if ($result >= 1) {
        echo "<script type='text/javascript'>
                  window.alert('Control code saved successfully');
                  window.location = 'index.php?view=ControlCode';
              </script>";
    } else {
        echo "<script type='text/javascript'>
                  window.alert('Database entry failed');
                  //window.location = 'index.php?view=DataTableClient';
              </script>";
    }
    unset($_POST['controlcodesave']);
}

if(isset($_GET['expireccode'])) {
    $ControlCode = ltrim(rtrim($_GET['expireccode']));
    $result = $mysqlcon->query("DELETE FROM controlcode WHERE codeclient = '$ControlCode'");
    if ($result > 0) {
        $_SESSION['message'] = "Control code=$ControlCode deleted";
        $_SESSION['msg_type'] = "success";
    }
    header("location: index.php?view=ControlCode");
}

if(isset($_POST['btngenerate']) && isset($_POST['controlcodecount'])) {
    $codecount = $_POST['controlcodecount'];
    for ($iterate = 0; $iterate < $codecount; ++$iterate)
    {
        $newcode = "";
        $newcodeprefix = generateRandomNumber(6);
        $result = $mysqlcon->query("SELECT codeclient FROM controlcode ORDER BY idcontrolcode DESC LIMIT 1");
        if ($result != null && $result->num_rows > 0) {
            $data = $result->fetch_array();
            $lastcode = $data['codeclient'];
            $codeparts = explode("-", $lastcode);
            $lastnumber = $codeparts[1];
            ++$lastnumber;
            $newcode = $newcodeprefix."-".$lastnumber;
        } else {
            $newcode = $newcodeprefix."-".($iterate + 1);
        }
        
        if($newcode != "")
            $mysqlcon->query("INSERT INTO controlcode(codeclient) VALUES('$newcode')");
    }
    $_SESSION['message'] = "New control codes generated";
    $_SESSION['msg_type'] = "success";
    header("location: index.php?view=ControlCode");
}
//Add new user
if(isset($_POST['saveUser'])) {
    $UserName = $_POST['UserName'];
    $FullName = $_POST['FullName'];
	$Password = $_POST['Password'];
	$hashedPassword = hash('sha512', $Password);
    $CanAdd = $_POST['canAdd'];
    $CanEdit = $_POST['canEdit'];
    $CanDelete = $_POST['canDelete'];

   $result =$mysqlcon->query("INSERT INTO `user` (`UserName`,`FullName`,`Password`, `CanAdd`, `CanEdit`, `CanDelete`)
                      VALUES('$UserName', '$FullName', '$hashedPassword', '$CanAdd', '$CanEdit', '$CanDelete')") or die($mysqlcon->error);    
if ($result > 0) {
    $_SESSION['message']= "User Successful Added!";
    $_SESSION['msg_type'] = "success";
   header("location: index.php?view=UserManager");
}
}

//Get User Data
if(isset($_GET['editUser'])) {
    $UserId = $_GET['editUser'];
    $result  = $mysqlcon->query("SELECT * FROM user WHERE UserId = $UserId") or die($mysqlcon->error());
    if($result != null && $result->num_rows >= 1) {
        $data = $result->fetch_array();
        $UserId = $data['UserId'];
        $UserName = $data['UserName'];
        $Password = $data['Password'];
		$hashedPassword = hash('sha512', $Password);
        $FullName = $data['FullName'];
        $CanAdd = $data['CanAdd'];
        $CanEdit = $data['CanEdit'];
        $CanDelete =$data['CanDelete'];
    }
}
//Update user
if(isset($_POST['updateUser'])) {
        $UserId = $_POST['UserId'];
        $UserName = $_POST['UserName'];
        $FullName = $_POST['FullName'];
    $CanAdd = $_POST['canAdd'];
    $CanEdit = $_POST['canEdit'];
    $CanDelete = $_POST['canDelete'];
    
	$result = $mysqlcon->query("UPDATE `user` SET `UserName` = '$UserName',`FullName` = '$FullName', `CanAdd` =  '$CanAdd', `CanEdit` = '$CanEdit', `CanDelete` = '$CanDelete' WHERE `UserId` = '$UserId' ") or die ($mysqlcon->error);  
	if ($result > 0) {
   $_SESSION['message']= "User has been Updated!";
    $_SESSION['msg_type'] = "success";  
	   }
	     header("location: index.php?view=UserManager");
}
//Change Password
if(isset($_POST['changePassword'])) {
        $UserId = $_POST['UserId'];
        $Password = $_POST['Password'];
		$hashedPassword = hash('sha512', $Password);

  $result = $mysqlcon->query("UPDATE `user` SET `Password` = '$hashedPassword' WHERE `UserId` = '$UserId' ") or die ($mysqlcon->error);  
if ($result > 0) {
   $_SESSION['message']= "Password Change Successfully!";
    $_SESSION['msg_type'] = "success";  
}
    header("location: index.php?view=UserManager");
}

if(isset($_GET['deleteUser'])) {
    $UserId = $_GET['deleteUser'];
    $mysqlcon->query("DELETE FROM user WHERE UserId = $UserId") or die($mysqlcon->error());
    
    $_SESSION['message']= "User has been Deleted!";
    $_SESSION['msg_type'] = "danger";  
    
    header("location: index.php?view=UserManager");
}
//$mysqlcon->close();

?>