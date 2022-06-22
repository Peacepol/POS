<?php    
    function filterTable($query) {
        require('db.php');
        $filter_result = mysqli_query($mysqlcon, $query);
        return $filter_result;
    }

    function generateKey() {
        $keylength = 6;
        $str = "1234567890abcdefghijklmnopqrstuvwxyz";
        $rndstr = substr(str_shuffle($str), 0, $keylength);
        return $rndstr;
    }
    
    function generateRandomNumber($keylength = 6) {
        $str = "1234567890";
        $rndstr = substr(str_shuffle($str), 0, $keylength);
        return $rndstr;
    }
    
    class CompanyInfo {
        public $ClientId = 0;
        public $ClientName = "";
        public $Address = "";
        public $Phone = "";
        public $Email = "";
        public $CCode = "";
    }
    
    $compdictionary = array();
?>