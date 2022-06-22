<?php 
require_once('postprocessor.php');

if(isset($_GET['invoice'])) {
    $LicenseId = $_GET['invoice'];
    $InvoiceCode = generateKey();

    $result = $mysqlcon->query("SELECT * FROM license l 
                                INNER JOIN client c ON l.ClientId = c.ClientId 
                                WHERE LicenseId = $LicenseId");

    $data = $result->fetch_array();

    $ClientName = $data['ClientName'];
    $PaymentAmount = $data['PurchaseAmount'];
    $InvoiceId = $data['InvoiceId'];
    
    $result = $mysqlcon->query("SELECT * FROM invoice WHERE InvoiceId = $InvoiceId AND Status = 'PAID'");
    if ($result != null 
        && $result->num_rows > 0) {
        $data = $result->fetch_array();
        $InvoiceCode = $data['InvoiceCode'];
    }
 }
?>
<br/>
<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-primary" style="color:white">
                <h4>Invoice</h4>
            </div>
            <div class="card-body">
            <form action="postprocessor.php" method="POST" >
<?php           if(isset($_GET['msg']))
                { 
?>
                    <div class="alert alert-light alert-success text-center py-3">
                        <?php echo $_GET['msg']; ?>
                    </div>
<?php
                }
?>
                <input value="<?php echo $LicenseId ?>" name="LicenseId" type="hidden" />
                <input type="hidden" name="CurrentYear" value="<?php echo date("Y");?>" />
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Company Name:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="CompanyName" id="CompanyName" value="<?php echo $ClientName; ?>" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Invoice Code:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" name="InvoiceCode" required />
                    </div>
                </div>      
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Software Type:</label>
                    <div class="col-md-6">
                      <select name="software" class="form-control" >
                       <option>ACCPAC</option>
                        <option>Able RETAIL</option>
                      </select>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Payment Method:</label>
                    <div class="col-md-6">
                      <select name="PaymentMethod" class="form-control" >
                        <option>Visa</option>
                        <option>Paypal</option>
                        <option>MasterCard</option>
                        <option>EFTPOS</option>
                        <option>Cheque</option>
                        <option>Cash</option>
                        <option>Direct Deposit</option>
                        <option>Other</option>
                      </select>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Payment Amount:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" id="disabledInput" name="PaymentAmount" value="<?php echo $PaymentAmount; ?>" />
                    </div>
                </div>  
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Payment Date:</label>
                    <div class="col-md-6">
                        <input type="date" class="form-control" id="disabledInput" name="PaymentDate" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="invoiceDescrip" class="col-md-4 col-form-label text-md-right">Description:</label>
                    <div class="col-md-6">
                        <textarea name="Description" id="invoiceDescrip" rows="3" class="form-control"></textarea>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-4 offset-md-8 text-md-right">
                        <input type="submit"class="btn btn-primary p-auto mx-auto" name="paid" id="paid" value="Pay" <?php echo $_SESSION['uAdd'];?>/>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-4 offset-md-8 text-md-right">
                        <input type="button" onClick="ajaxRequest('DataTableLicense.php');" class="btn btn-primary p-auto mx-auto" value="Back to table" />
                    </div>
                </div>
            </form>
            </div>
        </div>
    </div>
</div>