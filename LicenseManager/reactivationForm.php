<?php 
    require_once('postprocessor.php');

if(isset($_GET['active'])) {
    $LicenseId = $_GET['active'];
    $result1 = $mysqlcon ->query("SELECT 
                                    c.ClientId,
                                    c.ClientName,
                                    i.Description 
                                FROM license l 
                                INNER JOIN invoice i on l.InvoiceId=i.InvoiceId 
                                INNER JOIN client c on l.ClientId=c.ClientId 
                                WHERE l.LicenseId = '$LicenseId'");
    $data = $result1->fetch_array();
    $ClientName = $data['ClientName'];
    $Description = $data['Description'];
    $ClientId = $data['ClientId'];
}
?>
<br/>
<div class="row justify-content-center">
   <div class="col-md-8">
     <div class="card">
        <div class="card-header bg-primary" style="color:white">
            <h4>Reactivation invoice</h4>
        </div>
        <div class="card-body">
            <form action="postprocessor.php" method="POST" >
                <?php  
                if(isset($_GET['msg']))
                { 
                ?>
                    <div  class="alert alert-light alert-success text-center py-3">
                        <?php echo $_GET['msg']; ?>
                    </div>
                <?php
                }
                ?>
                <div class="form-group row">
                    <div class="col-md-6">
                        <input type="hidden" name="CompanyID" value="<?php echo $ClientId; ?>" />
                        <input type="hidden" name="LicenseId" value="<?php echo $LicenseId; ?>" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Company Name:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" id="CompanyName" name="CompanyName" value="<?php echo $ClientName; ?>" readonly />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Year:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="InvoiceYear" id="InvoiceYear" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Invoice Code:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="InvoiceCode" id="InvoiceCode" />
                    </div>
                </div>
                <!-- 
                <div class="form-group row">
                    <label for="LicenseExpire" class="col-md-4 col-form-label text-md-right" required>License Expiration:</label>
                    <div class="col-md-6">
                        <input id="LicenseExpire" type="date" class="form-control" name="LicenseExpire"  placeholder="Expiration"/>
                    </div>
                </div>  -->
                <div class="form-group row">
                    <label for="invoiceDescrip" class="col-md-4 col-form-label text-md-right">Description:</label>
                    <div class="col-md-6">
                        <textarea name="Description" rows="3" class="form-control"><?php echo $Description;?></textarea>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-4 offset-md-8 text-md-right">
                        <input type="button" onClick="ajaxRequest('AnnualMaintenance.php');" class="btn btn-primary p-auto mx-auto" value="Back to table" />
                        <input type="submit" class="btn btn-primary mx-auto" name="reactivationinvoice" value="Save" />
                    </div>
                </div>
           </form>
        </div>
     </div>
    </div>
 </div>