<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>

</div> <!-- ./container -->
<div class="card border-primary my-3 mx-2" style="max-width:100%">
   <div id="table1">
   <div class="card-header">
        <h3>Licenses List</h3>
    </div>
    <?php if(isset($_SESSION['message'])): ?>
    <div class="alert alert-<?=$_SESSION['msg_type']?>">
    <?php
        echo $_SESSION['message'];
        unset($_SESSION['message']);
    ?>
    </div>
    <?php endif ?>
    <div class="card-body">
        <form method="POST" action="postprocessor.php">
            <button type="button" class="btn btn-primary d-print-none" onClick="print_report('table1');">Print</button>
            <button type="submit" class="btn btn-success d-print-none" name="refresh">Refresh</button>
            <button type="submit" class="btn btn-success d-print-none" name="search">Search</button>
            <input type="hidden" name="license" value=1/>    
            <input type="text" name="valueToSearch" placeholder="Value To Search" class="d-print-none"style="width:500px"/>
        </form>
			<div class= "table-responsive ">
				<table class="table table-striped table-bordered mt-3" style="width:100%">
					<thead>
						<tr>
                        	<th class="d-print-none">Update</th>
							<th class="d-print-none">Delete</th>
							<th class="d-print-none">Invoice</th>
							<th>License Code</th>
							<th>Client Name</th>
							<th>Max Terminal</th>
							<th>Purchase Date</th>
							<th>Purchase Amount</th>
							<th>Payment Mode</th>
							<th>License Status</th>
							<th>Activation Code</th>
							<th>Expiry Date</th>
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
									l.CreationDate,
									l.TerminalCount									
								  FROM license l 
								  INNER JOIN client c on l.ClientId=c.ClientId";
								  //WHERE l.LicenseCode <> NULL OR l.LicenseCode <> ''";
					}

					$result = filterTable($query);
					if ($result != null && $result->num_rows > 0) {
						while($value = mysqli_fetch_array($result)): 
				?>
						<tr>
                        	<td class="d-print-none">
								<button class="btn btn-success" onClick="ajaxRequest('UpdateLicense.php?edit=', <?php echo $value['LicenseId'];?>);" <?php echo $_SESSION['uEdit'];?> >Edit</button>
							</td>
							<td class="d-print-none">
								<a href="postprocessor.php?deletelic=<?php echo $value['LicenseId'];?>">
									<button class="btn btn-danger"  <?php echo $_SESSION['uDelete'];?> >Delete</button>
								</a>
							</td>
							<td class="d-print-none">
								<button class="btn btn-primary" onClick="ajaxRequest('invoice.php?invoice=',<?php echo $value['LicenseId'];?>);">Invoice</button>
							</td>
							<td><?php echo $value['LicenseCode'];?></td>
							<td><?php echo $value['ClientName'];?></td>
							<td><?php echo $value['TerminalCount'];?></td>
							<td><?php echo $value['PurchaseDate'];?></td>
							<td><?php echo $value['PurchaseAmount'];?></td>
							<td><?php echo $value['PaymentMode'];?></td>
							<td><?php echo $value['LicenseStatus'];?></td>
							<td style = "word-break: break-all"><?php echo $value['ActivationCode'];?></td>
							<td><?php echo $value['ExpiryDate'];?></td>
							<td><?php echo $value['CreationDate'];?></td>
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