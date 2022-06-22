const MAGICNO = 603777;

function generateRandomNumber(pCompanyName) {
  return Math.floor((Math.random() * 900000) + 10000);
}

function generateActivationCode(pCompanyName, pRegNo, pSerialNo) {
  var lCompName = pCompanyName.toUpperCase();
  var lRegNo = pRegNo.toUpperCase();
  var lSerialNo = pSerialNo.toUpperCase();
  
  var compnameasciicode = (lCompName.length > 4) ? lCompName.substring(0, 4).split(""): lCompName.split("");
  var regnoasciicode = (lRegNo.length > 4) ? lRegNo.substring(0, 4).split(""): lRegNo.split("");
  var serialnoasciicode = (lSerialNo.length > 4) ? lSerialNo.substring(0, 4).split(""): lSerialNo.split("");
  
  var compnamestr = "";
  var regnostr = "";
  var serialnostr = "";
  
  for (var iterate = 0; iterate < compnameasciicode.length; ++iterate) {
    compnamestr += compnameasciicode[iterate].charCodeAt(0);
  } 
  for (var iterate = 0; iterate < regnoasciicode.length; ++iterate) {
    regnostr += regnoasciicode[iterate].charCodeAt(0);
  }
  for (var iterate = 0; iterate < serialnoasciicode.length; ++iterate) {
    serialnostr += serialnoasciicode[iterate].charCodeAt(0);
  }
  
  var compnameinteger = Number(compnamestr);
  var regnointeger = Number(regnostr);
  var serialnointeger = Number(serialnostr);
  
  compnameinteger = compnameinteger ^ MAGICNO;
  var regnointeger2 = regnointeger ^ MAGICNO;
  serialnointeger = serialnointeger ^ MAGICNO;
  
  var compnamehexa = compnameinteger.toString(16).toUpperCase();
  var regnohexa = regnointeger2.toString(16).toUpperCase();
  var serialnohexa = serialnointeger.toString(16).toUpperCase();
  
  var activationkey = btoa(compnamehexa + "," + regnohexa + "," + serialnohexa);
  return activationkey;
}

function generateActivationCode2(pCompanyName, pRegNo, pSerialNo, pMaxUser) {
    var compnamestr = "";
    var regnostr = "";
    var serialnostr = "";
    var maxuserstr = "";

    for (var iterate = 0; iterate < pCompanyName.length; ++iterate) {
        compnamestr += pCompanyName[iterate].charCodeAt(0).toString(16) ;
    }
    for (var iterate = 0; iterate < pRegNo.length; ++iterate) {
        regnostr += pRegNo[iterate].charCodeAt(0).toString(16) ;
    }
    for (var iterate = 0; iterate < pSerialNo.length; ++iterate) {
        serialnostr += pSerialNo[iterate].charCodeAt(0).toString(16);
    }
    for (var iterate = 0; iterate < pMaxUser.length; ++iterate) {
        maxuserstr += pMaxUser[iterate].charCodeAt(0).toString(16);
    }

    var activationkey = btoa(compnamestr + "," + regnostr + "," + serialnostr + "," + maxuserstr);

    return activationkey;
}

function decodeActivation2(pActivationKey) {
    var rawkey = atob(pActivationKey);
    var rawkeycomponents = rawkey.split(",");
    var returnobj = {};
    if (rawkeycomponents.length == 4) {
        var decodedcompname = "";
        var decodedserialno = "";
        var decodedregno = "";
        var decodedmaxuser = "";

        var compname = rawkeycomponents[0];
        var serialno = rawkeycomponents[1];
        var regno = rawkeycomponents[2];
        var maxuser = rawkeycomponents[3];

        for (var iterate = 0; iterate < compname.length; iterate+=2) {
            var hexaunit = compname.substring(iterate, iterate + 2);
            var decimalunit = parseInt(hexaunit, 16);
            decodedcompname += (String.fromCharCode(decimalunit));
        }
        for (var iterate = 0; iterate < serialno.length; iterate+=2) {
            var hexaunit = serialno.substring(iterate, iterate + 2);
            var decimalunit = parseInt(hexaunit, 16);
            decodedserialno += (String.fromCharCode(decimalunit));
        }
        for (var iterate = 0; iterate < regno.length; iterate+=2) {
            var hexaunit = regno.substring(iterate, iterate + 2);
            var decimalunit = parseInt(hexaunit, 16);
            decodedregno += (String.fromCharCode(decimalunit));
        }
        for (var iterate = 0; iterate < maxuser.length; iterate+=2) {
            var hexaunit = maxuser.substring(iterate, iterate + 2);
            var decimalunit = parseInt(hexaunit, 16);
            decodedmaxuser += (String.fromCharCode(decimalunit));
        }

        returnobj = {
            "company": decodedcompname,
            "serial": decodedserialno,
            "registration": decodedregno,
            "maxuser": decodedmaxuser
        }
    }
    return returnobj;
}

function generateReActivationCode(pCompanyName, pInvoiceNo, pMaxUser) {
  var lCompanyName = pCompanyName.toUpperCase();
  var lInvoiceNo = pInvoiceNo.toUpperCase();
  var lMaxUser = pMaxUser.toUpperCase();
  
  var compnameasciicode = (lCompanyName.length > 4) ? lCompanyName.substring(0, 4).split(""): lCompanyName.split("");
  var invoicenoasciicode = (lInvoiceNo.length > 4) ? lInvoiceNo.substring(0, 4).split(""): lInvoiceNo.split("");
  var maxuserasciicode = (lMaxUser.length > 4) ? lMaxUser.substring(0, 4).split(""): lMaxUser.split("");
  
  var compnamestr = "";
  var invoicenostr = "";
  var maxuserstr= "";  
  
  for (var iterate = 0; iterate < compnameasciicode.length; ++iterate) {
    compnamestr += compnameasciicode[iterate].charCodeAt(0);
  } 
  for (var iterate = 0; iterate < invoicenoasciicode.length; ++iterate) {
    invoicenostr += invoicenoasciicode[iterate].charCodeAt(0);
  }
  for(var iterate = 0; iterate < maxuserasciicode.length; ++iterate){
	maxuserstr += maxuserasciicode[iterate].charCodeAt(0);
  }
  
  var compnameinteger = Number(compnamestr);
  var invoicenointeger = Number(invoicenostr);
  var maxuserinteger = Number(maxuserstr);
  
  compnameinteger = compnameinteger ^ MAGICNO;
  invoicenointeger = invoicenointeger ^ MAGICNO;
  maxuserinteger = maxuserinteger ^ MAGICNO;
  
  var compnamehexa = compnameinteger.toString(16).toUpperCase();
  var invoicenohexa = invoicenointeger.toString(16).toUpperCase();
  var maxuserhexa = maxuserinteger.toString(16).toUpperCase();
  
  var activationkey = btoa(compnamehexa + "," + invoicenohexa + "," + maxuserhexa);
  return activationkey;
}

function print_report(mydiv) {
    var prtContent = document.getElementById(mydiv);
    var WinPrint = window.open();
    var str = "<link rel='stylesheet' href='./css/bootstrap.min.css'>"; //Add Your stylesheet here....!!
    str += "<style>";
    str += "body { \
               font-family:Arial; \
               font-size:10px !important; \
            } \
            table { \
               border-collapse: collapse; \
               font-size:9px !important; \
               width:100% !important; \
            } \
            table td, \
            table th { \
               border:1px solid black; \
            } \
            panel-heading { \
               font-size:14px important; \
            } \
            @media print { \
                .d-print-none { \
                   display: none !important; \
                } \
            .d-flex {\
                display: -webkit-box !important;\
                display: -ms-flexbox !important;\
                display: flex !important;\
            }\
            .flex-fill {\
              -webkit-box-flex: 1 !important;\
              -ms-flex: 1 1 auto !important;\
              flex: 1 1 auto !important;\
            }\
    } ";
    str += "</style>";
    
    WinPrint.document.writeln(str);
    WinPrint.document.writeln("");
    WinPrint.document.writeln(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();
}

function ajaxRequest(uripage, params = "") {
    $.ajax( {
        url: uripage + params,
        success: function(data) {
            document.getElementById('page-content-wrapper').innerHTML = data;
        }
    }).done(function(data) {
        $('div#page-content-wrapper script').each(function (index, element) { eval(element.innerHTML)});
    });
}

var compdict = [];
function changedomvals(selindex) {
    document.getElementById('clientid').value = compdict[selindex].ClientId;
    document.getElementById('company').value = compdict[selindex].ClientName;
    document.getElementById('clientaddress').value = compdict[selindex].Address;
    document.getElementById('clientphone').value = compdict[selindex].Phone;
    document.getElementById('clientemail').value = compdict[selindex].Email;
    document.getElementById('ccode').value = compdict[selindex].ControlCode;
}

function generateControlCode(companyname) {
    var prefix = companyname.substring(0, 3);
    var lastccode = document.getElementById('ccode').value;
    var ccodedigit = lastccode.substring(3);
    ++ccodedigit;

    return (prefix + ccodedigit);
}

function EnableDisableTextBox() {
    var chkYes = document.getElementById("chkYes");
    var txtPassportNumber = document.getElementById("terminalcount");
    txtPassportNumber.disabled = chkYes.checked ? false : true;
    if (!txtPassportNumber.disabled)
	{
        txtPassportNumber.focus();
    }
	else
	{
		document.getElementById("terminalcount").value = '-1';
		 txtPassportNumber.value ='-1';
	}
}
	var checkPassword = function() {
  if (document.getElementById('password').value ==
    document.getElementById('confirm_password').value) {
    document.getElementById('message').style.color = 'green';
    document.getElementById('message').innerHTML = 'password match';
	document.getElementById('saveUser').disabled = false;
	}
 else
	 {
    document.getElementById('message').style.color = 'red';
    document.getElementById('message').innerHTML = 'password do not match';
	document.getElementById('saveUser').disabled = true;
	}
}
	var updatecheck = function() {
		
  if (document.getElementById('CanAdd').checked == true)
	{
		document.getElementById('CanAdd').value = 1;
	}
	else
	{
		document.getElementById('CanAdd').value = 0;
	}
	
	if (document.getElementById('CanEdit').checked == true)
	{
		document.getElementById('CanEdit').value = 1;
	}
	else
	{
		document.getElementById('CanEdit').value = 0;
	}
	
	if (document.getElementById('CanDelete').checked == true)
	{
		document.getElementById('CanDelete').value = 1;
	}
	else
	{
		document.getElementById('CanDelete').value = 0;
	}

}


