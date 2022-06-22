<?php 
require_once('postprocessor.php');

if(isset($_GET['edit'])) {
    $LicenseId = $_GET['edit'];
    $update = true;
    $result  = $mysqlcon->query("SELECT l.PurchaseAmount, l.PaymentMode, l.ExpiryDate, l.TerminalCount ,c.ClientId, c.ClientName, c.SerialNumber, c.RegistrationCode FROM license l 
                                 INNER JOIN client c ON l.ClientId = c.ClientId
                                 WHERE LicenseId = $LicenseId");
    $data = $result->fetch_array();
    $ClientId = $data['ClientId'];
    $ClientName = $data['ClientName'];
    $SerialNumber = $data['SerialNumber'];
    $RegistrationCode = $data['RegistrationCode'];
    $MaxUser = $data['TerminalCount'];
    $PurchaseAmount = $data['PurchaseAmount'];
    $PaymentMode = $data['PaymentMode'];
}
?>

<div class="container">
  <div class="col-md-10">
    <div class="card">
        <div class="card-header bg-primary" style="color: white">
            <h2>Update License</h2>
        </div>
        <div class="card-body">
            <form class="form-horizontal" action="postprocessor.php" method="POST">          
         
                <input type="text" placeholder="License Id" class="form-control" autofocus name="LicenseId" value="<?php echo $LicenseId; ?>" hidden />
    
                <input type="hidden" value="<?php echo $ClientId; ?>" name="ClientId"/ hidden>
  
                <input type="text" class="form-control" name="companyname" id="companyname" class="activationtextfield" value="<?php echo $ClientName; ?>" hidden />

                <input type="text" class="form-control" name="serialno" id="serialno" class="activationtextfield" value="<?php echo $SerialNumber; ?>" hidden />

                <input type="text" class="form-control" name="registrationno" id="registrationno" class="activationtextfield" value="<?php echo $RegistrationCode; ?>" hidden />
                
                <input type="text" class="form-control" name="maxuser" id="maxuser" class="activationtextfield" value="<?php echo $MaxUser; ?>" hidden />
 
                <div class="form-group row">
                    <label for="PurchaseAmount" class="col-md-4 col-form-label text-md-right">Purchase Amount : </label>
                    <div class="col-md-6">
                        <input type="text" placeholder="PurchaseAmount" class="form-control" autofocus name="PurchaseAmount" required="required" value="<?php echo $PurchaseAmount; ?>" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="PaymentMethod" class="col-md-4 col-form-label text-md-right">Payment Method : </label>
                        <div class="col-md-6">
                        <select name="PaymentMethod" class="form-control">
                            <option selected= "select"><?php echo $PaymentMode; ?></option>
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
                        <label for="registrationno" class="col-md-4 col-form-label text-md-right">License Type:</label>
                        <div class="col-md-6">
    
                        <input type="radio" name="licensetype" value="unlimited"  id="chkNo" onClick="EnableDisableTextBox()"/ checked> 
                        <label for="unlimited">Unlimited</label>    
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="radio" name="licensetype" value="limited" id="chkYes" onClick="EnableDisableTextBox()" />
                        <label for="limited">Limited</label>
                        
                        <input type ="text" id="terminalcount" class="form-control pull-right" name = "terminalcount" disabled="disabled" />
                            
                         </div>
                    </div>
                    <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Activation Code :</label>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 text-md-right">
                            <input type="button" class="btn btn-success p-auto mx-auto" id="btngenerate" value="Regenerate" class="activationbutton" onClick="document.getElementById('activationcode').innerHTML=generateActivationCode2($('#companyname').val(), $('#registrationno').val(), $('#serialno').val(), $('#terminalcount').val());" />
                        </div>
                        <div class="col-md-6">
                            <textarea name="newactivationcode" class="form-control" id="activationcode" rows="3" cols="40" class="activationtextarea" value=<?php if (isset($_POST['activationcode'])) echo $_POST['activationcode'];?>></textarea>
                        </div>
                    </div>
                <button type="submit" class="btn btn-primary btn-block mb-2" name="licenseupdate">Update License</button>
                <button type="button" class="btn btn-primary btn-block" onClick="ajaxRequest('DataTableLicense.php');">View Table</button>
            </form> <!-- /form -->
        </div> 
    </div>
  </div>
</div><!-- ./container -->
