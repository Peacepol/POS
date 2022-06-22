<?php
require_once('db.php');
require_once('function.php');

if(isset($_GET['viewClient'])) {
    $ClientId = $_GET['viewClient'];
    $result = $mysqlcon->query("SELECT * FROM client WHERE ClientId = $ClientId");
    $data = $result->fetch_array();
    
    $ClientId = $data['ClientId'];
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
?>

<br/>
<div class="col-md-4">
   <button type="button" class="btn btn-primary d-print-none" onClick="print_report('table1');">Print</button>
</div>
<br/>
<div id = "table1">
<div class="card border-primary mb-3 mx-2" style="max-width:100%">
    <div class="card-header"><strong><h4>Client Details</h4></strong>  </div>
    <div class="card-body table-responsive">
        <table class="table" style="width:100%">
            <tbody>
                <tr class="d-flex">
                    <th class="col-2"><h4><strong>Client Name:</strong></h4></th> <td class="flex-fill text-info"><h4><strong><?php echo $ClientName;?></strong></h4></td>
                    <th class="col-2"><h4><strong>Serial Number:</strong></h4></th> <td class="flex-fill text-info"><h4><strong><?php echo $SerialNumber;?></strong></h4></td>
                </tr>
                <tr class="d-flex">
                  <th class=" col-2"><h4><strong>Registration Code:</strong></h4></th> <td class="flex-fill text-info"><h4><strong><?php echo $RegistrationCode;?></strong></h4></td>
                  <th class=" col-2" ><h4><strong>Registration Name:</strong></h4></th><td class="flex-fill text-info"><h4><strong><?php echo $RegistrationName;?></strong></h4></td> 
                </tr>
                <tr class="d-flex">
                    <th class="d-flex" ><h5><strong>Address:</strong></h5></th><td class="flex-fill text-info"><h5><?php echo $Address;?></h5></td> 
                    <th class=" d-flex"><h5><strong>City:</strong></h5></th><td class="flex-fill text-info"><h5><?php echo $City;?></h5></td>
                    <th class=" d-flex"><h5><strong>State:</strong></h5></th><td class="flex-fill text-info"><h5><?php echo $State;?></h5></td>
                </tr>
                <tr class="d-flex">
                    <th class="d-flex">Country:</th><td class="flex-fill text-info"><h5><?php echo $Country;?></h5></td>
                    <th class="d-flex">Post Code:</th><td class="flex-fill text-info"><h5><?php echo $PostCode;?></h5></td>
                </tr>
                <tr class="d-flex">
                    <th class=" col-1">Phone:</th><td class="flex-fill text-info"><?php echo $Phone;?></td>
                    <th class=" col-1">Fax:</th><td class="flex-fill text-info"><?php echo $Fax;?></td>
                    <th class="e col-1">Email:</th><td class="flex-fill text-info"><?php echo $Email;?></td>
                    <th class="col-1">Contact Person:</th><td class="flex-fill text-info"><?php echo $ContactPerson;?></td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
    <div class="card border-primary mb-3 mx-2" style="max-width:100%">
        <div class="card-header ">
            <div class="d-flex bd-highlight">
                <div class="p-2 bd-highlight"><strong><h4>License</h4></strong></div>
                <div class="ml-auto p-2 bd-highlight">  
                    <form method="POST" action="postprocessor.php">
                        <button class="btn btn-success d-print-none "  name="search">Annual Maintenance</button>
                        <input type="hidden" name="annualmaintenance"  value=1/>
                        <input value="<?php echo $ClientName;?>" name="valueToSearch" type="hidden"/>
                    </form> 
                </div>
            </div>
        </div>
        <div class="card-body table-responsive">
            <table class="table table-striped table-bordered table-hover">
                <thead >
                    <tr class="flex-fill bd-highlight">
                        <th class="p-2 flex-fill bd-highlight">Creation Date</th>
                        <th class="p-2 flex-fill bd-highlight">Year</th>
                        <th class="p-2 flex-fill bd-highlight">License Code</th>
                        <th class="p-2 flex-fill bd-highlight">Activation Code</th>
                        <th class="p-2 flex-fill bd-highlight">License Status</th>
                        <th class="p-2 flex-fill bd-highlight">Purchase Date</th>
                        <th class="p-2 flex-fill bd-highlight">Purchase Amount</th>
                        <th class="p-2 flex-fill bd-highlight">Payment Mode</th>
                        <th class="p-2 flex-fill bd-highlight">Expiry Date</th>
                    </tr>
                </thead>
                <tbody >
    <?php 
        $query = "SELECT l.*, i.Year FROM license l
                  LEFT JOIN invoice i ON l.InvoiceId=i.InvoiceId
                  WHERE ClientId = $ClientId";
        $result = filterTable($query);
        while($LicenseValue = mysqli_fetch_array($result)): 
    ?>
                    <tr>
                        <td><?php echo $LicenseValue['CreationDate'];?></td>
                        <td><?php echo $LicenseValue['Year'];?></td>
                        <td><?php echo $LicenseValue['LicenseCode'];?></td>
                        <td style = "word-break: break-all"><?php echo $LicenseValue['ActivationCode'];?></td>
                        <td><?php echo $LicenseValue['LicenseStatus'];?></td>
                        <td><?php echo $LicenseValue['PurchaseDate'];?></td>
                        <td ><?php echo $LicenseValue['PurchaseAmount'];?></td>
                        <td ><?php echo $LicenseValue['PaymentMode'];?></td>
                        <td><?php echo $LicenseValue['ExpiryDate'];?></td>
                    </tr>
    <?php 
        endwhile; 
    ?>
                </tbody>
            </table>
        </div>
    </div>
</div> <!-- table -->
<div class="col-md-4 offset-md-8 text-md-right">
    <input type="button" onClick="ajaxRequest('DataTableClient.php');" class="btn btn-primary p-auto mx-auto" value="Back to table" />
</div>
<br/>
<br/>
