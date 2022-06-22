<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>
   
</div> <!-- ./container -->
 
<div class="card border-primary my-3 mx-2" style="max-width:100%">
   <div id="table1">
    <div class="card-header">
         <h3>Control Code Lists</h3>
    </div>
    <?php if(isset($_SESSION['message'])): ?>
    <div class="alert alert-<?=$_SESSION['msg_type']?>">
    <?php
        echo $_SESSION['message'];
        unset($_SESSION['message']);
    ?>
    </div>
    <?php endif ?>

    <div class="card-body table-responsive">
        <form method="post" action="postprocessor.php">
            <ol class="breadcrumb">
                <li>
                    <ul style="list-style-type:none">
                        <li>
                            <button type="button" class="btn btn-primary d-print-none mx-1" onClick="print_report('table1');">Print</button>
                            <button type="submit" class="btn btn-success d-print-none mx-1" name="refresh">Refresh</button>                 
                            <input type="hidden" name="controlcode" value= 1 />
                        </li>
                        <li>
                            <br/>
                            <input type="text" name="valueToSearch" placeholder="Value To Search" class="d-print-none mx-1" style="width:400px" />
                            <button type="submit" class="btn btn-success d-print-none mx-1" name="search">Search</button>
                            <div class="checkbox">
                                <label>
                                    &nbsp;
                                    <input type="checkbox" value="false" id="unused" name="unusedcode" onclick="if (this.value=='false')this.value=true;else this.value=false;" />
                                    &nbsp;
                                    Show only unused codes
                                </label>
                            </div>
                        </li>
                        <li>
                            <label class="col-form-label text-md-right">&nbsp; Number of codes to generate:</label>
                            <input type="number" name="controlcodecount" class="d-print-none mx-1" style="width:125px" id="controlcodecount" /> 
                            <input type="submit" class="btn btn-success mx-1" id="btngenerate" name="btngenerate" value="Generate" <?php echo $_SESSION['uAdd'];?> />
                        </li>
                    </ul>
                </li>
            </ol>			
        </form >
		<div class="d-flex justify-content-center">
            <table class="table table-striped table-bordered table-responsive" style="width:100%">
                <thead>
                    <tr>
                        <th>Control Code</th>
                        <th>Client Name</th>
                        <th>Serial Number</th>
                        <th>Registration Code</th>
                        <th class="d-print-none">Options</th>
                    </tr>
                </thead>
                <tbody>
            <?php 
                if (isset($_SESSION['searchquery'])) {
                    $query = $_SESSION['searchquery'];
                    unset ($_SESSION['searchquery']);
                    unset ($_POST['controlcode']);
                } else {
                    $query = "SELECT cc.codeclient AS ControlCode,
                                     c.ClientName,
                                     c.SerialNumber,
                                     c.RegistrationCode
                              FROM controlcode cc 
                              LEFT JOIN client c ON c.ClientId=cc.idclient";
                }
                $result = filterTable($query);
                if ($result != null && $result->num_rows > 0) {
                    while($value = mysqli_fetch_array($result)): 
            ?>
                    <tr>
                        <td><?php echo $value['ControlCode'];?></td>
                        <td><?php echo $value['ClientName'];?></td>
                        <td><?php echo $value['SerialNumber'];?></td>
                        <td><?php echo $value['RegistrationCode'];?></td>
                        <td class="d-print-none">
                            <a href="postprocessor.php?expireccode= <?php echo $value['ControlCode'];?>">
                                <button class="btn btn-danger"  <?php echo $_SESSION['uDelete'];?> >Delete</button>
                            </a>                  
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