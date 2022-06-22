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
    <input type="hidden" name="annualmaintenance" value=1/>
    <input type="text" name="valueToSearch" placeholder="Value To Search" style="width:500px"/>
</form>
</div>
<div class="card-body">
<div id = "table">
<h2>License Reactivation</h2>
<table class="table table-striped table-bordered table-hover table-responsive" style="width:100%">

    <thead>
        <tr>
            <th>Client Name</th>
            <th>Year</th>
            <th>Invoice Code</th>
            <th>License Code</th>
            <th>Paid Amount</th>
            <th>Status</th>
            <th>Expiration</th>
        </tr>
    </thead>
    <tbody>
<?php 
    if (isset($_SESSION['searchquery'])) {
        $query = $_SESSION['searchquery'];
        unset ($_SESSION['searchquery']);
        unset ($_POST['annualmaintenance']);
    } else {
        $query = "SELECT c.ClientId, 
                         c.ClientName,  
                         i.Year,
                         i.InvoiceCode, 
                         l.LicenseCode, 
                         l.LicenseStatus,
                         l.ExpiryDate,
                         l.PurchaseAmount,
                         l.LicenseId 
                  FROM client c 
                  INNER JOIN license l ON l.ClientId=c.ClientId
                  INNER JOIN invoice i ON i.InvoiceId=l.InvoiceId";
    }

    $result = filterTable($query);

    while($value = mysqli_fetch_array($result)): 
?>
        <tr>
            <td><?php echo $value['ClientName'];?></td>
            <td><?php echo $value['Year'];?></td>
            <td><?php echo $value['InvoiceCode'];?></td>
            <td><?php echo $value['LicenseCode'];?></td>
            <td><?php echo $value['PurchaseAmount'];?></td>
            <td><?php echo $value['LicenseStatus'];?></td>
            <td><?php echo $value['ExpiryDate'];?></td>
        </tr>
<?php 
    endwhile; 
?>
    </tbody>
</table>
</div>
</div>
<br/>
<br/>
