<?php
require_once('db.php');
require_once('function.php');

$result  = $mysqlcon->query("SELECT c.ClientId,
                                 c.ClientName,
                                 CONCAT(c.Address, c.City, c.State, c.Postcode, c.Country) AS FullAddress,
                                 c.Phone,
                                 c.Email
                             FROM client c");    
?>
<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-primary" style="color: white">
                <h2>Control Code</h2>
            </div>
       
            <div class="card-body">
                <form action="postprocessor.php" method="POST">
                    <div class="form-group row">
                        <label for="companyname" class="col-md-4 control-label text-md-right">Company Name:</label>

                        <div class="col-md-6">
                            <select name="company" id="company" class="form-control" onChange="changedomvals(this.selectedIndex);">
                            <?php
                                $arrayindex = 0;
                                while($data = $result->fetch_array()):
                                    $compdictionary[$arrayindex] = new CompanyInfo();
                                    $compdictionary[$arrayindex]->ClientId = $data['ClientId'];
                                    $compdictionary[$arrayindex]->ClientName = $data['ClientName'];
                                    $compdictionary[$arrayindex]->Address = $data['FullAddress'];
                                    $compdictionary[$arrayindex]->Phone = $data['Phone'];
                                    $compdictionary[$arrayindex]->Email = $data['Email'];
                                    $clientccquery = "SELECT codeclient
                                                         FROM controlcode
                                                         WHERE idclient=".$data['ClientId']."
                                                         ORDER BY idcontrolcode DESC LIMIT 1";                                   
                                    $ccoderesult = $mysqlcon->query($clientccquery);
                                    if ($ccoderesult != null && $ccoderesult->num_rows > 0) {
                                        $ccodedata = $ccoderesult->fetch_array();
                                        $compdictionary[$arrayindex]->CCode = $ccodedata['codeclient'];
                                    } else {
                                        $compdictionary[$arrayindex]->CCode = substr($data['ClientName'], 0, 3)."0";
                                    }                                 
                            ?>
                                    <script type="text/javascript">
                                        var compinfo = {
                                            ClientId: <?php echo $data['ClientId'];?>,
                                            ClientName: "<?php echo $data['ClientName'];?>",
                                            Address: "<?php echo $data['FullAddress'];?>",
                                            Phone: "<?php echo $data['Phone'];?>",
                                            Email: "<?php echo $data['Email'];?>",
                                            ControlCode: "<?php echo $compdictionary[$arrayindex]->CCode; ?>"
                                        };
                                        compdict[<?php echo $arrayindex;?>] = compinfo;
                                    </script>
                                    <option><?php echo $data['ClientName']; ?></option>
                            <?php
                                    ++$arrayindex;
                                endwhile;
                            ?>
                            </select>
                        </div>
                    </div>
                    <input type="hidden" value="<?php echo $compdictionary[0]->ClientId; ?>" name="clientid" id="clientid"/>
                    <input type="hidden" value="<?php echo $compdictionary[0]->CCode; ?>" name="ccode" id="ccode"/>
                    <div class="form-group row">
                        <label for="clientaddress" class="col-md-4 col-form-label text-md-right">Address:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="clientaddress" id="clientaddress" value="<?php echo $compdictionary[0]->Address; ?>"/>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="clientphone" class="col-md-4 col-form-label text-md-right">Phone:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="clientphone" id="clientphone" value="<?php echo $compdictionary[0]->Phone; ?>"/>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="clientemail" class="col-md-4 col-form-label text-md-right">Email:</label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" name="clientemail" id="clientemail" value="<?php echo $compdictionary[0]->Email; ?>"/>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-4 col-form-label text-md-right">Control Code:</label>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 text-md-right">
                            <input type="button" class="btn btn-success p-auto mx-auto" id="btngenerate" value="Generate" onClick="document.getElementById('controlcode').innerHTML=generateControlCode($('#company').val());" />
                        </div>
                        <div class="col-md-6">
                            <textarea name="controlcode" class="form-control" id="controlcode" rows="3" cols="40"><?php if (isset($_POST['controlcode'])) echo $_POST['controlcode'];?></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4 offset-md-8 text-md-right">
                            <input type="submit" id="btnsave" class="btn btn-primary p-auto mx-auto" name="controlcodesave" value="Save" />
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>