<?php 
require_once('db.php');
require_once('function.php');

if(isset($_GET['active'])) {
    $LicenseId = $_GET['active'];
    $result1 = $mysqlcon ->query("SELECT 
                                    c.ClientId,
                                    c.ClientName, 
                                    l.PaymentMode,
                                    l.PurchaseAmount,
                                    i.InvoiceCode, 
                                    i.Description 
                                FROM license l 
                                INNER JOIN invoice i on l.InvoiceId=i.InvoiceId 
                                INNER JOIN client c on l.ClientId=c.ClientId 
                                WHERE l.LicenseId = '$LicenseId'");
    $data = $result1->fetch_array();
    $ClientName = $data['ClientName'];
    $PaymentMode = $data['PaymentMode'];
    $InvoiceCode = $data['InvoiceCode'];
    $PaymentAmount = $data['PurchaseAmount'];
    $ClientId = $data['ClientId'];
}
?>

<br/>
<div class="row justify-content-center">
   <div class="col-md-8">
     <div class="card">
         <div class="card-header bg-primary" style="color:white">
             <h4>Pay Reactivation</h4>
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
                        <input type="hidden" name="CompanyName" value="<?php echo $ClientName; ?>" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Invoice Code:</label>
                    <div class="col-md-6">
                        <input type="text" class="form-control" id="InvoiceCode" name="InvoiceCode" value="<?php echo $InvoiceCode; ?>" readonly />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label text-md-right">Software Type:</label>
                    <div class="col-md-6">
                      <select name="software" class="form-control" >
                        <option>AACCPAC</option>
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
                        <label for="registrationno" class="col-md-4 col-form-label text-md-right">License Type:</label>
                        <div class="col-md-6">
    
                        <input type="radio" name="licensetype" value="unlimited"  id="chkNo" onClick="EnableDisableTextBox()"/ checked> 
                        <label for="unlimited">Unlimited</label>    
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="radio" name="licensetype" value="limited" id="chkYes" onClick="EnableDisableTextBox()" />
                        <label for="limited">Limited</label>
                        
                        <input type ="text" id="terminalcount" value="1" class="form-control pull-right" name = "terminalcount" disabled="disabled" />
                            
                         </div>
                    </div>

                <!--<div class="form-group row">
                    <div class="col-md-4 offset-md-8 text-md-right">
                        <button type="button" class="btn btn-primary mx-auto" data-toggle="collapse" data-target="#Re-Activation">PAY</button>
                    </div>
                </div>-->

                <div id="Re-Activation">
                    <div class="form-group row">
                        <label class="col-md-4 col-form-label text-md-right">Activation Code:</label>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 text-md-right">
                            <input type="button" class="btn btn-success p-auto mx-auto" id="btngenerate" value="Generate" class="activationbutton" onClick="document.getElementById('ReactivationCode').innerHTML=generateReActivationCode('<?php echo $ClientName;?>', $('#InvoiceCode').val(), $('#terminalcount').val());" />
                        </div>
                        <div class="col-md-6">
                            <textarea name="ReactivationCode" class="form-control" id="ReactivationCode" rows="3" cols="40" class="activationtextarea" value = <?php if(isset($_POST['ReactivationCode'])) echo $_POST['ReactivationCode'];?>></textarea>
                        </div>
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col-md-4 offset-md-8 text-md-right">
                        <input type="button" onClick="ajaxRequest('AnnualMaintenance.php');" class="btn btn-primary p-auto mx-auto" value="Back to table" />
                        <input type="submit" class="btn btn-primary mx-auto" name="payreactivation" value="Pay" />
                    </div>
                </div>
           </form>
       </div>
     </div>
    </div>
 </div>