<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>
<br/>
<br/>
</div> <!-- ./container -->
<?php if(isset($_SESSION['message'])): ?>
<div class="alert alert-<?=$_SESSION['msg_type']?>">
<?php
    echo $_SESSION['message'];
    unset($_SESSION['message']);
?>
</div>
<?php endif ?>
<div class="card-body">
<form method="post" action="postprocessor.php">
	<input type = "button" class="btn btn-primary" value = "Print" onClick="print_report('table');"/>
    <button type="submit" class="btn btn-success" name="refreshReport">Refresh</button>
    <button type="submit" class="btn btn-success" name="searchReport">Search</button>
    <input type="hidden" name="license" value=1/>
    <input type="text" name="valueToSearch" placeholder="Value To Search" style="width:500px"/>
</form>
</div>
  <div class="card-body">

<div id = "table">
<h2>Registered Licenses</h2>    
<table class="table table-striped table-bordered table-hover table-responsive" style="width:100%">
    <thead>
        <tr>
            <th>Client Name</th>
            <th>Purchase Date</th>
            <th>Purchase Amount</th>
            <th>Payment Mode</th>
            <th>License Status</th>
            <th>Activation Code</th>
            <th>Expiry Date</th>
            <th>License Code</th>
            <th>Creation Date</th>
        </tr>
    </thead>
    <tbody>
<?php
    if (isset($_SESSION['searchquery'])) {
        $query = $_SESSION['searchquery'];
        unset ($_SESSION['searchquery']);
        unset ($_POST['license']);
    } else {
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
    }

    $result = filterTable($query);
    
    while($value = mysqli_fetch_array($result)): 
?>
        <tr>
            <td><?php echo $value['ClientName'];?></td>
            <td><?php echo $value['PurchaseDate'];?></td>
            <td><?php echo $value['PurchaseAmount'];?></td>
            <td><?php echo $value['PaymentMode'];?></td>
            <td><?php echo $value['LicenseStatus'];?></td>
            <td><?php echo $value['ActivationCode'];?></td>
            <td><?php echo $value['ExpiryDate'];?></td>
            <td><?php echo $value['LicenseCode'];?></td>
            <td><?php echo $value['CreationDate'];?></td>
        </tr>
<?php 
    endwhile; 
?>
    </tbody>
</table>
</div>
</div>

