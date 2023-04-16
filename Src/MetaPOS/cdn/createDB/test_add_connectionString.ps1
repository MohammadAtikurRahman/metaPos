Param(

[parameter(Mandatory=$true)] [string]$NDB, 
[Parameter(Mandatory=$True)] [string]$NUSR, 
[Parameter(Mandatory=$True)] [string]$NUSRPASS,
[Parameter(Mandatory=$True)] [string]$URL

)



$newString = -join("server","=","CQJW-A71767F9F\SQLEXPRESS",";","database","=",$NDB,";","uid","=",$NUSR,";","password","=",$NUSRPASS,";"); 

$webConfig ="..\..\web.config";

$xml = [xml](Get-Content $webConfig);
$node = $xml.SelectSingleNode('//connectionStrings')
$child = $xml.CreateElement('add')
$child.SetAttribute('name', $NDB)
$child.SetAttribute('connectionString', $newString)
$node.AppendChild($child)
$xml.Save($webConfig)

New-WebBinding -Name allRobiamarhishab -IPAddress "*" -Port 80 -HostHeader $URL;

wacs --target iis --host $URL --installation iis;
iisreset /restart; 