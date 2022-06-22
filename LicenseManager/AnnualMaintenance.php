<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>
   
</div> <!-- ./container -->
 
<div class="card border-primary my-3 mx-2" style="max-width:100%">
   <div id="table1">
    <div class="card-header">
         <h3>Annual Maintenance List</h3>
    </div>
    <?php if(isset($_SESSION['message'])): ?>
    <div class="alert alert-<?=$_SESSION['msg_type']?>">
    <?php
        echo $_SESSION['message'];
        unset($_SESSION['message']);
    ?>
    </div>
    <?php endif ?>

    <div class="card-body ">
        <form method="post" action="postprocessor.php">
            <button type="button" class="btn btn-primary d-print-none" onClick="print_report('table1');">Print</button>
            <button type="submit" class="btn btn-success d-print-none" name="refresh">Refresh</button>
            <button type="submit" class="btn btn-success d-print-none" name="search">Search</button>
            <input type="hidden" name="annualmaintenance" value=1 />    
            <input type="text" name="valueToSearch" placeholder="Value To Search" class="d-print-none"style="width:500px" />
        </form>
		<div class="table-responsive container"style="max-width:100%">
            <table class="table table-striped table-bordered  mt-3" style="width:100%">
                <thead>
                    <tr>
                        <th>Client Name</th>
                        <th>Year</th>
                        <th>Invoice Code</th>
                        <th>License Code</th>
                        <th>Paid Amount</th>
                        <th>Status</th>
                        <th>Expiration</th>
                        <th>Software Type</th>
                        <th class="d-print-none">Options</th>
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
                                     l.LicenseId,
                                     i.SoftwareType
                              FROM client c 
                              INNER JOIN license l ON l.ClientId=c.ClientId
                              INNER JOIN invoice i ON i.InvoiceId=l.InvoiceId";
                }
                $result = filterTable($query);
                if ($result != null && $result->num_rows > 0) {
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
                        <td><?php echo $value['SoftwareType'];?></td>
                        <td class="d-print-none">
                        <?php
                            if ($value['LicenseStatus'] == 'ACTIVE') {
                        ?>
                            <button class="btn btn-primary" onClick="ajaxRequest('reactivationForm.php?active=', <?php echo $value['LicenseId'];?>);">Reactivate</button>
                            <a href="postprocessor.php?expirelic=<?php echo $value['LicenseId'];?>">
                                <button class="btn btn-danger" <?php echo $_SESSION['uDelete'];?> >Expire</button>
                            </a>
                        <?php
                            } else if ($value['LicenseStatus'] == 'INACTIVE') {
                        ?>
                            <button class="btn btn-success" onClick="ajaxRequest('PayReactivation.php?active=', <?php echo $value['LicenseId'];?>);">Pay</button>
                        <?php
                            }
                        ?>
                        </td>
                    </tr>
            <?php 
                    endwhile;
                }
            ?>
                </tbody>
            </table>
			</div>
        </div>
    </div>
</div>