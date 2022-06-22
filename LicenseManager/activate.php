<?php
require_once('postprocessor.php');

if(isset($_GET['active'])) {
    $ClientId = $_GET['active'];
    $update = true;
    $result  = $mysqlcon->query("SELECT * FROM client WHERE ClientId = $ClientId");
    $data = $result->fetch_array();
    $ClientId = $data['ClientId'];
    $ClientName = $data['ClientName'];
    $SerialNumber = $data['SerialNumber'];
    $RegistrationCode = $data['RegistrationCode'];
}
?>

<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-primary" style="color: white">
                <h2>Activation Code</h2>
            </div>
       	
            <div class="card-body">
                <form action="postprocessor.php" method="POST">
                    <input type="hidden" value="<?php echo $ClientId; ?>" name="ClientId"/>
                    <div class="form-group row">
                        <label for="companyname" class="col-md-4 control-label text-md-right">Company Name:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="companyname" id="companyname" class="activationtextfield" value="<?php echo $ClientName; ?>"/>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="serialno" class="col-md-4 col-form-label text-md-right">Serial Number:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="serialno" id="serialno" class="activationtextfield" value="<?php echo $SerialNumber; ?>"/>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="registrationno" class="col-md-4 col-form-label text-md-right">Registration Code:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="registrationno" id="registrationno" class="activationtextfield" value="<?php echo $RegistrationCode; ?>"/>
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
                   
                    <div class="form-group row">
                        <label class="col-md-4 col-form-label text-md-right">Activation Code:</label>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 text-md-right">
                            <input type="button" class="btn btn-success p-auto mx-auto" id="btngenerate" value="Regenerate" class="activationbutton" onClick="document.getElementById('activationcode').innerHTML=generateActivationCode2($('#companyname').val() , $('#registrationno').val(), $('#serialno').val(), $('#terminalcount').val() );" />
                        </div>
                        <div class="col-md-6">
                            <textarea name="activationcode" class="form-control" id="activationcode" rows="3" cols="40" class="activationtextarea"  value=<?php if (isset($_POST['activationcode'])) echo $_POST['activationcode'];?>></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 offset-md-8 text-md-right">
                            <input type="submit" id="btnsave" class="btn btn-primary p-auto mx-auto" name="activatesave" value="Save"  <?php echo $_SESSION['uAdd'];?>  />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 offset-md-8 text-md-right">
                            <input type="button" onClick="ajaxRequest('DataTableClient.php');" class="btn btn-primary p-auto mx-auto" value="Back to table"/>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>