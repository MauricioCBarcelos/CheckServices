
try {

    if (Get-Service -Name CheckServices) {
		
		Stop-Process -Name 'CheckServices'
		
        cmd /c sc delete "CheckServices" 
        
    }

}
catch {

}


New-Service -Name "CheckServices" -DisplayName "Check Services" -BinaryPathName (([System.IO.Directory]::GetParent($MyInvocation.MyCommand.Path)).FullName+"\CheckServices.exe")  -StartupType Manual

Read-Host "Deploy Finished"