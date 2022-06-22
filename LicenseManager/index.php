<?php
session_start();
if(!isset($_SESSION["user"])) {
    header("location:login.php");
}
?>
<!doctype html>
<html class="no-js" lang="">
<head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title> Able License Manager</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--link rel="stylesheet" href="./css/bootstrap.css"-->
    <!--link rel="stylesheet" media="print" href="./css/bootstrap.css"-->
    <link rel="stylesheet" href="./css/bootstrap.min.css">
    <link rel="stylesheet" href="./css/simple-sidebar.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <!--script src="js/bootstrap.bundle.min.js"></script-->
    <script src="./js/functions.js"></script>
	<script src="./js/bootstrap.js"></script>
</head>

<body>
    <div class="d-flex" id="wrapper">
    <!-- Sidebar -->
    <div class="bg-primary border-right" id="sidebar-wrapper">
      <div class="sidebar-heading"style="color:white">LICENSE MANAGER</div>
      <div class="list-group list-group-flush" id="navbarColor01"> 
        <a href="index.php" class="list-group-item list-group-item-action bg-primary">Dashboard</a>
        <a href="#" onClick="ajaxRequest('RegisterClient.php');" class="list-group-item list-group-item-action bg-primary">Register Client</a>
        <a href="#" onClick="ajaxRequest('DataTableClient.php');" class="list-group-item list-group-item-action bg-primary">Client List</a>
        <a href="#" onClick="ajaxRequest('DataTableLicense.php');" class="list-group-item list-group-item-action bg-primary">License List</a>
        <a href="#" onClick="ajaxRequest('AnnualMaintenance.php');" class="list-group-item list-group-item-action bg-primary">Annual Maintenance</a>
        <!--a href="#" onClick="ajaxRequest('GenerateControlCode.php');" class="list-group-item list-group-item-action bg-primary">Generate Control Code</a-->
        <a href="#" onClick="ajaxRequest('ControlCode.php');" class="list-group-item list-group-item-action bg-primary">Control Code List</a>
		<a href="#" onClick="ajaxRequest('UserManager.php');" class="list-group-item list-group-item-action bg-primary">User Manager</a>

        <br/>
        <div class="container-fluid">
            <div class="row"  class="list-group-item list-group-item-action bg-primary">
                <div style="position:fixed; float:none; width:inherit;">
                    <footer style="position:fixed; bottom: 0; width:239px;">
                        <a href="login.php" class="list-group-item list-group-item-action bg-primary">
                            <font size = "4">Logout</font>
                        </a>
                    </footer>
                </div>
            </div>    
        </div>
    </div>
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">
    <!-- / PAGE CONTENT --> 
      <div class="container-fluid">
        <h1 class="mt-4">Welcome <?php echo $_SESSION['user'] ?></h1>
      </div>  
    </div>
    <!-- /#page-content-wrapper -->
    </div>
  <!-- /#wrapper -->

  <!-- Menu Toggle Script -->
  <script>
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    }); 
  </script>
  
<?php
    if (isset($_GET['view'])) {
        switch($_GET['view']) {
            case "DataTableClient":
?>
            <script type="text/javascript">
                ajaxRequest('DataTableClient.php');
            </script>
<?php       break;
            case "DataTableLicense":
?>
            <script type="text/javascript">
                ajaxRequest('DataTableLicense.php');
            </script>    
<?php       break;
            case "RegisterClient":
?>
            <script type="text/javascript">
                ajaxRequest('RegisterClient.php');
            </script>  
<?php       break;
            case "AnnualMaintenance":
?>
            <script type="text/javascript">
                ajaxRequest('AnnualMaintenance.php');
            </script>
<?php       break;
            case "GenerateControlCode":
?>
            <script type="text/javascript">
                ajaxRequest('GenerateControlCode.php');
            </script>
<?php       break;
            case "ControlCode":
?>
            <script type="text/javascript">
                ajaxRequest('ControlCode.php');
            </script>
<?php       break;
			case "UserManager":
?>
            <script type="text/javascript">
                ajaxRequest('UserManager.php');
            </script>
<?php       break;
        }
    }
?>
</body>
</html>

