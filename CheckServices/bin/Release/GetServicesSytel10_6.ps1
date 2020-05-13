$prefix = "<service Name=`""
$posfix = "`" autoStart=`"true`"/>"  
$item = ""
$c = 0
Get-Content -Path (([System.IO.Directory]::GetParent($MyInvocation.MyCommand.Path)).FullName+'\Lista-De-Servicos.txt') | foreach{
$c++
$obj = Get-Service -DisplayName $_ | Where-Object {$_.Status -eq "Running"}
#$obj.count
for ($i = 0; $i -lt $obj.Count; $i++) {
    $item +="`n" + $prefix + $obj[$i].Name + $posfix + "`n"
}
}

$content = (Get-Content (([System.IO.Directory]::GetParent($MyInvocation.MyCommand.Path)).FullName+'\CheckServices.exe.config')).Replace('<!--Add services-->','<!--Add services--> '+ "`n"+'   '+$item)

Set-Content  (([System.IO.Directory]::GetParent($MyInvocation.MyCommand.Path)).FullName+'\CheckServices.exe.config') -Value $content

 
 
 Get-Content (([System.IO.Directory]::GetParent($MyInvocation.MyCommand.Path)).FullName+'\CheckServices.exe.config')

Read-Host 'Arquivo atualizado'   